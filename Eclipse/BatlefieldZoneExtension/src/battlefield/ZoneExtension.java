package battlefield;

import java.sql.SQLException;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.Arrays;

import com.smartfoxserver.v2.components.login.ILoginAssistantPlugin;
import com.smartfoxserver.v2.components.login.LoginAssistantComponent;
import com.smartfoxserver.v2.components.login.LoginData;
import com.smartfoxserver.v2.components.signup.ISignUpAssistantPlugin;
import com.smartfoxserver.v2.components.signup.PasswordMode;
import com.smartfoxserver.v2.components.signup.SignUpAssistantComponent;
import com.smartfoxserver.v2.components.signup.SignUpConfiguration;
import com.smartfoxserver.v2.components.signup.SignUpErrorCodes;
import com.smartfoxserver.v2.components.signup.SignUpValidationException;
import com.smartfoxserver.v2.core.SFSEventType;
import com.smartfoxserver.v2.db.IDBManager;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSArray;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.extensions.ExtensionLogLevel;
import com.smartfoxserver.v2.extensions.SFSExtension;
import com.smartfoxserver.v2.persistence.room.DBRoomStorageConfig;
import com.smartfoxserver.v2.persistence.room.RoomStorageMode;
import com.smartfoxserver.v2.persistence.room.SFSStorageException;

public class ZoneExtension extends SFSExtension  
{
	IDBManager sfs2xDBManager;
	private SignUpAssistantComponent suac;
	private LoginAssistantComponent lac;	
	
	String ValidateUserName(String uname)
	{
		Pattern pattern = Pattern.compile("^([a-zA-Z0-9_/-])*$",Pattern.CASE_INSENSITIVE);
    	Matcher matcher = pattern.matcher(uname);
    	if(matcher.find())
    	{
    		return "";
    	}
    	return "Please enter a valid user name";
	}
	
	String ValidateEmail(String email)
	{
		Pattern pattern = Pattern.compile("^[\\w!#$%&'*+/=?`{|}~^-]+(?:\\.[\\w!#$%&'*+/=?`{|}~^-]+)*@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$",Pattern.CASE_INSENSITIVE);
    	Matcher matcher = pattern.matcher(email);
    	if(matcher.find())
    	{
    		return "";
    	}
    	return "Please enter a valid email address";
	}
	
	String ValidatePassword(String password)
	{
		Pattern hasNumber = Pattern.compile("[0-9]+");
		Pattern hasUpperChar = Pattern.compile("[A-Z]+");
		Pattern hasLowerChar = Pattern.compile("[a-z]+");
		Pattern hasMiniMaxChars = Pattern.compile(".{6,30}");
		Pattern hasSymbols = Pattern.compile("[!@#$%^&*()_+=/[{/]};:<>|./?,-]");
		
		Matcher hasNumMatcher = hasNumber.matcher(password);
		Matcher hasUpperCharMatcher = hasUpperChar.matcher(password);
		Matcher hasLowerCharMatcher = hasLowerChar.matcher(password);
		Matcher hasMiniMaxCharsMatcher = hasMiniMaxChars.matcher(password);
		Matcher hasSymbolsMatcher = hasSymbols.matcher(password);
		
		if(!hasNumMatcher.find())
		{
			return "Your password must contain a number.";
		}
		else if(!hasUpperCharMatcher.find())
		{
			return "Your password must contain an uppercase letter.";
		}
		else if(!hasLowerCharMatcher.find())
		{
			return "Your password must contain a lowercase letter.";
		}
		else if(!hasMiniMaxCharsMatcher.find())
		{
			return "Your password must contain between 6 and 30 characters.";
		}
		else if(!hasSymbolsMatcher.find())
		{
			return "Your password must contain at least one special character.";
		}
		return "";
		
	}
	
	String ValidateRePassword(String password, String rePassword)
	{
		if(!password.equals(rePassword))
		{
			return "Passwords must match";
		}
		return "";
	}
	
	boolean TableExists(String dbName, String tableName)
	{
		String sql = "SELECT * FROM information_schema.tables WHERE table_schema = '" + dbName + "' AND table_name = '" + tableName + "' LIMIT 1;";
		try
        {
            ISFSArray res = sfs2xDBManager.executeQuery(sql, new Object[] {});
            if(res.size() > 0)
            {
            	return true;
            }
            else
            {
            	return false;
            }
        }
        catch (SQLException e)
        {
            trace(ExtensionLogLevel.WARN, "SQL Failed: " + e.toString());
            return false;
        }		
	}
	
	void InitRoomPersistence()
	{
		DBRoomStorageConfig dbsc = new DBRoomStorageConfig();
		dbsc.tableName = "bf_rooms";
		
		dbsc.storeInactiveRooms = true;
		dbsc.storeRoomVariables = true;
    	
    	getParentZone().initRoomPersistence(RoomStorageMode.DB_STORAGE, dbsc);   	
	}
	
	void InitSFS2XUserTable()
	{ 
		if(!TableExists("sfs2x","users"))
		{
			String sql = "CREATE TABLE users (id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY, username VARCHAR(30), password VARCHAR(32), email VARCHAR(50));";
            try
            {
                sfs2xDBManager.executeInsert(sql, new Object[] {});
                trace("sfs2x users table created");
            }
            catch (SQLException e)
            {
                trace(ExtensionLogLevel.WARN, "SQL Failed: " + e.toString());
            }
		}
	}
	
	void InitArmyInventoryTable()
	{
		if(!TableExists("sfs2x","bf_ArmyInventory"))
		{
			String sql = "CREATE TABLE bf_ArmyInventory (id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY, playerID INT UNSIGNED NOT NULL, race TINYINT UNSIGNED, attack TINYINT UNSIGNED, defense TINYINT UNSIGNED, movement TINYINT UNSIGNED, bbgroup TINYINT UNSIGNED, upgradeLevel TINYINT UNSIGNED, upgradeString VARCHAR(50), abilities VARCHAR(50), INDEX(playerID));";
	        try
	        {
	        	sfs2xDBManager.executeInsert(sql, new Object[] {});
	            
	        }
	        catch (SQLException e)
	        {
	            trace(ExtensionLogLevel.WARN, "SQL Failed: " + e.toString());
	            
	        }
		}
	}
	
	void InitCraftInventoryTable()
	{
		if(!TableExists("sfs2x","bf_CraftInventory"))
		{
			String sql = "CREATE TABLE bf_CraftInventory (id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY, playerID INT UNSIGNED NOT NULL, type VARCHAR(2) NOT NULL, quantity SMALLINT UNSIGNED NOT NULL, INDEX(playerID));";
	        try
	        {
	        	sfs2xDBManager.executeInsert(sql, new Object[] {});
	            
	        }
	        catch (SQLException e)
	        {
	            trace(ExtensionLogLevel.WARN, "SQL Failed: " + e.toString());
	            
	        }
		}
	}
	
	void InitCipherTable()
	{
		if(!TableExists("sfs2x","bf_Cipher"))
		{
			String sql = "CREATE TABLE bf_Cipher (id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY, playerID INT UNSIGNED NOT NULL, race TINYINT UNSIGNED, cipher VARCHAR(26) NOT NULL, seed VARCHAR(100), solved TINYINT UNSIGNED, INDEX(playerID));";
	        try
	        {
	        	sfs2xDBManager.executeInsert(sql, new Object[] {});
	            
	        }
	        catch (SQLException e)
	        {
	            trace(ExtensionLogLevel.WARN, "SQL Failed: " + e.toString());
	            
	        }
		}
	}
	
	public void InitSUAC()
	{
		suac = new SignUpAssistantComponent();
		
        suac.getConfig().minUserNameLength = 3;
        suac.getConfig().maxUserNameLength = 30;
        suac.getConfig().maxEmailLength = 50;
        suac.getConfig().maxPasswordLength = 33;
        suac.getConfig().checkForDuplicateEmails = true;
        suac.getConfig().checkForDuplicateUserNames = true;
        suac.getConfig().passwordMode = PasswordMode.MD5;
        
        suac.getConfig().preProcessPlugin = new ISignUpAssistantPlugin()
        {
            @Override
            public void execute(User user, ISFSObject params, SignUpConfiguration config) throws SignUpValidationException
            {
            	String uname = params.getUtfString("username");
            	String password = params.getUtfString("password");
            	String rePassword = params.getUtfString("r");
            	String email = params.getUtfString("email");
            	
            	if(ValidateUserName(uname) == "")
            	{
            		if(ValidateEmail(email) == "")
            		{
            			if(ValidatePassword(password) == "")
            			{
            				if(ValidateRePassword(password,rePassword) != "")
            				{
            					throw new SignUpValidationException(SignUpErrorCodes.CUSTOM_ERROR, ValidateRePassword(password,rePassword));
            				}
            			}
            			else
            			{
            				throw new SignUpValidationException(SignUpErrorCodes.CUSTOM_ERROR, ValidatePassword(password));
            			}
            		}
            		else
            		{
            			throw new SignUpValidationException(SignUpErrorCodes.CUSTOM_ERROR, ValidateEmail(email));
            		}
            	}
            	else
            	{
            		throw new SignUpValidationException(SignUpErrorCodes.CUSTOM_ERROR, ValidateUserName(uname));
            	}      
            }
        };
        
        suac.getConfig().postProcessPlugin = new ISignUpAssistantPlugin()
		{

			@Override
			public void execute(User user, ISFSObject arg1, SignUpConfiguration arg2) throws SignUpValidationException 
			{
				
			}
	
		};
        
        /*suac.getConfig().emailResponse.isActive = true;
        suac.getConfig().emailResponse.fromAddress = "StandstillDigitalMedia@gmail.com";
        suac.getConfig().emailResponse.subject = "Thanks for signing up!";
        suac.getConfig().emailResponse.template = "EmailTemplates/SignUpConfirmation.html";
        
        suac.getConfig().passwordRecovery.isActive = true;
        suac.getConfig().passwordRecovery.mode = RecoveryMode.GENERATE_NEW;
        suac.getConfig().passwordRecovery.email.fromAddress = "StandstillDigitalMedia@gmail.com";
        suac.getConfig().passwordRecovery.email.subject = "Password recovery service";
        suac.getConfig().passwordRecovery.email.template = "EmailTemplates/SignUpConfirmation.html";*/

	}
	
	
	public void InitLAC()
	{
		lac = new LoginAssistantComponent(this);
		lac.getConfig().userNameField = "username";
		lac.getConfig().nickNameField = "username";
		
		lac.getConfig().extraFields = Arrays.asList("id");
	     
	    lac.getConfig().postProcessPlugin = new ILoginAssistantPlugin ()
	    {	    	
	    	@Override
	        public void execute(LoginData loginData)
	        {
	            ISFSObject fields = loginData.extraFields;
	            trace("*********userid = " + fields.getInt("id"));
	            loginData.session.setProperty("dbID", fields.getInt("id"));
	             
	            //int auth = fields.getInt("auth");
	            //String uma = fields.getUtfString("uma");
	             
	            /*if(auth == 1)
	            {
	            	loginData.session.setProperty("$permission", DefaultPermissionProfile.STANDARD);
	            }
	            else if (auth == 2)
	            {
	            	loginData.session.setProperty("$permission", DefaultPermissionProfile.MODERATOR);
	            }
	            else if(auth == 3)
	            {
	            	loginData.session.setProperty("$permission", DefaultPermissionProfile.ADMINISTRATOR);
	            }
	            else if(auth == 4)
	            {
	            	//loginData.session.setProperty("$permission", UserPermission.PAIDUSER);
	            }*/
	            //loginData.clientOutGoingData.putUtfString(Values.uma, uma);
	            //loginData.clientOutGoingData.putUtfString(Values.LeaderString, GetLeadersString(getParentZone().getUserByName(lac.getConfig().nickNameField)));
	            //loginData.clientOutGoingData.putSFSArray(Values.SaveString, GetLayoutsString(getParentZone().getUserByName(lac.getConfig().nickNameField), fields.getUtfString("username")));
	            //loginData.clientOutGoingData.putInt(Values.Version, versionNum);
	        }
	    };
	}
	
	void DeleteAllRooms()
	{
		try 
    	{
			getParentZone().getRoomPersistenceApi().removeAllRooms();
		} 
    	catch (SFSStorageException e) 
    	{
			e.printStackTrace();
		}
	}
	
	@Override
	public void init() 
	{
		sfs2xDBManager = getParentZone().getDBManager();
		
    	InitRoomPersistence();    	
    	InitSFS2XUserTable();
    	InitArmyInventoryTable();
    	InitCraftInventoryTable();
    	InitCipherTable();
    	
    	InitSUAC();
    	InitLAC();
    	
    	DeleteAllRooms();
    	
    	addRequestHandler(SignUpAssistantComponent.COMMAND_PREFIX, suac); 
    	addEventHandler(SFSEventType.ROOM_REMOVED, RoomRemovedEvent.class);
    	addEventHandler(SFSEventType.USER_JOIN_ZONE, JoinZoneEvent.class);
    	//addEventHandler(SFSEventType.USER_JOIN_ROOM, ZoneEventUserJoinZone.class);
	}
}
