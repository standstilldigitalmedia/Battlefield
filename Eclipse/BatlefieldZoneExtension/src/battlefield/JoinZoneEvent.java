package battlefield;

import java.util.ArrayList;

import com.smartfoxserver.v2.api.CreateRoomSettings;
import com.smartfoxserver.v2.core.ISFSEvent;
import com.smartfoxserver.v2.core.SFSEventParam;
import com.smartfoxserver.v2.db.IDBManager;
import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.SFSRoomRemoveMode;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.variables.RoomVariable;
import com.smartfoxserver.v2.entities.variables.SFSRoomVariable;
import com.smartfoxserver.v2.entities.variables.SFSUserVariable;
import com.smartfoxserver.v2.entities.variables.UserVariable;
import com.smartfoxserver.v2.exceptions.SFSCreateRoomException;
import com.smartfoxserver.v2.exceptions.SFSException;
import com.smartfoxserver.v2.exceptions.SFSJoinRoomException;
import com.smartfoxserver.v2.extensions.BaseServerEventHandler;
import com.smartfoxserver.v2.persistence.room.SFSStorageException;

public class JoinZoneEvent extends BaseServerEventHandler 
{
	IDBManager sfs2xDBManager;
	
	@Override
	public void handleServerEvent(ISFSEvent event) throws SFSException 
	{
		sfs2xDBManager = getParentExtension().getParentZone().getDBManager();
		User user = (User) event.getParameter(SFSEventParam.USER);
		
		UserVariable dbId = new SFSUserVariable("dbid", user.getSession().getProperty("dbID"));
		dbId.setHidden(true);
		user.setVariable(dbId);
		
		
		if(sfs2xDBManager == null || user == null)
		{
			trace("User or dbmanager = null in ZoneEventUserJoinZone");
		}
		else
		{
			String ciphers = Cipher.InitUserCiphers(user, sfs2xDBManager);
			
			if(ciphers.equals("none"))
			{
				String newCipher = Cipher.CreateNewCipher(user, sfs2xDBManager, 0);
				
				if(newCipher.equals("exists"))
				{
					trace("This shouldn't be possible");
				}
				else if(!newCipher.equals("set"))
				{
					trace(newCipher);
				}
				CreateRoomSettings cfg = new CreateRoomSettings();
    			cfg.setName(user.getName() + "Campaign");
    			cfg.setMaxUsers(1);
    			cfg.setMaxVariablesAllowed(10);
    			cfg.setGame(false);
    			cfg.setExtension(new CreateRoomSettings.RoomExtensionSettings("Battlefield", "battlefield.CampaignRoomExtension"));
    			cfg.setDynamic(true);
    			cfg.setAutoRemoveMode(SFSRoomRemoveMode.WHEN_EMPTY);
    			
    			ArrayList<RoomVariable> vars = new ArrayList<RoomVariable>();
    			/*********************************************************************
    			Set Room Variables here
    			**********************************************************************/
    			RoomVariable someVar = new SFSRoomVariable("p", 0);
    			vars.add(someVar);
    			cfg.setRoomVariables(vars);
    			
    			try
    			{
    				Room myNewRoom = getApi().createRoom(getParentExtension().getParentZone(), cfg, user);
    				getApi().joinRoom(user, myNewRoom);
    			}
    			catch (SFSCreateRoomException b)
    			{
    			    trace("Exception " + b);
    			}
    			catch (SFSJoinRoomException c)
    			{
    				trace("Excemption " + c);
    			}
			}
			else if(ciphers.equals("set"))
			{
				try 
				{
					CreateRoomSettings settings = getParentExtension().getParentZone().getRoomPersistenceApi().loadRoom(user.getName() + "Campaign");
					Room newRoom = getApi().createRoom(getParentExtension().getParentZone(), settings, user);
					getApi().joinRoom(user, newRoom);
				} 
				catch (SFSStorageException e) 
				{
					e.printStackTrace();
				}
				catch (SFSCreateRoomException b)
    			{
    			    trace("Exception " + b);
    			}
    			catch (SFSJoinRoomException c)
    			{
    				trace("Excemption " + c);
    			}				
			}
			else
			{
				trace(ciphers);
			}
		}
		
		/*
		else
		{
			try
	        {
	        	String sql = "SELECT * FROM ct_rooms WHERE name='" + user.getName() + "Campaign'";
	            ISFSArray res = sfs2xDBManager.executeQuery(sql, new Object[] {});
	            if(res.size() > 0)
	            {
	            	try
	            	{
	            		CreateRoomSettings settings = getParentExtension().getParentZone().getRoomPersistenceApi().loadRoom(user.getName() + "Campaign");
	        			Room myRoom = getApi().createRoom(getParentExtension().getParentZone(), settings, null, false, null, false, false);
	        			getApi().joinRoom(user, myRoom);
	            	}
	            	catch (SFSStorageException a)
	            	{
	            		trace(a);
	            	}
	            }
	            else
	            {
	    			CreateRoomSettings cfg = new CreateRoomSettings();
	    			cfg.setName(user.getName() + "Campaign");
	    			cfg.setMaxUsers(1);
	    			cfg.setMaxVariablesAllowed(10);
	    			cfg.setGame(false);
	    			//cfg.setExtension(new CreateRoomSettings.RoomExtensionSettings("ChadtegoExtension", "TutorialRoomExtension"));
	    			cfg.setDynamic(true);
	    			
	    			ArrayList<RoomVariable> vars = new ArrayList<RoomVariable>();
	    			/*********************************************************************
	    			Set Room Variables here
	    			**********************************************************************
	    			RoomVariable someVar = new SFSRoomVariable("type", "value");
	    			vars.add(someVar);
	    			cfg.setRoomVariables(vars);
	    			
	    			try
	    			{
	    				Room myNewRoom = getApi().createRoom(getParentExtension().getParentZone(), cfg, user);
	    				//getApi().leaveRoom(user, user.getLastJoinedRoom());
	    				getApi().joinRoom(user, myNewRoom);
	    			}
	    			catch (SFSCreateRoomException b)
	    			{
	    			    trace("Exception " + b);
	    			}
	    			catch (SFSJoinRoomException c)
	    			{
	    				trace("Excemption " + c);
	    			}
	            }
	        }
	        catch(SQLException e)
	        {
	            trace(ExtensionLogLevel.WARN, "SQL Failed: " + e.toString());
	        }
		}*/        
	}
	
	/*static List<ISFSObject> GetUserCraftInv(int userID, IDBManager sfs2xDBManager)
	{
		//List<ISFSObject> currencyArray = new ArrayList<ISFSObject>();  
		//List<ISFSObject> runesArray = new ArrayList<ISFSObject>();
		//List<ISFSObject> craftingMatsArray = new ArrayList<ISFSObject>();
		List<ISFSObject> returnArray = new ArrayList<ISFSObject>();
		
		String sql = "SELECT * FROM BG_CraftInventory WHERE playerID=" + userID;
		
        try
        {
            ISFSArray res = sfs2xDBManager.executeQuery(sql, new Object[] {});
            
            for(int a = 0; a < res.size(); a++)
            {
            	ISFSObject item = res.getSFSObject(a);
            	ISFSObject returnObj = new SFSObject();
            	
            	returnObj.putInt("q", item.getInt("quantity"));
            	returnObj.putUtfString("t", item.getUtfString("type"));
            	
            	returnArray.add(returnObj);
            }
        }
        catch(SQLException e)
        {
            //trace(ExtensionLogLevel.WARN, "SQL Failed: " + e.toString());
        }
        return returnArray;
	}
	
	static List<ISFSObject> GetUserCipher(int userID, IDBManager sfs2xDBManager)
	{
		List<ISFSObject> returnArray = new ArrayList<ISFSObject>();
		
		String sql = "SELECT * FROM BG_Cipher WHERE playerID=" + userID;
		
        try
        {
            ISFSArray res = sfs2xDBManager.executeQuery(sql, new Object[] {});
            
            for(int a = 0; a < res.size(); a++)
            {
            	ISFSObject item = res.getSFSObject(a);
            	ISFSObject returnObj = new SFSObject();
            	
            	returnObj.putInt("r", item.getInt("race"));
            	returnObj.putUtfString("c", item.getUtfString("cipher"));
            	
            	returnArray.add(returnObj);
            }
        }
        catch(SQLException e)
        {
            //trace(ExtensionLogLevel.WARN, "SQL Failed: " + e.toString());
        }
        return returnArray;
	}*/

}
