package battlefield;

import java.sql.SQLException;

import com.smartfoxserver.v2.db.IDBManager;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSArray;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSArray;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.entities.variables.SFSUserVariable;
import com.smartfoxserver.v2.entities.variables.UserVariable;
import com.smartfoxserver.v2.exceptions.SFSVariableException;

public class Cipher 
{
	static char[] alpha = {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
	static char[] capitalAlpha = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
	static String[][] raceSeedStrings = {
			{
				"The Daglaresh aren't the sharpest tool in the shed",
				"A diet high in rotten fruit results in stinky poop",
				"Farm life is not always an easy life",
			},
			{
				"It is said that curiosity is what killed the cat",
				"Shin Ra have a habit of always landing on their feet",
				"A rat in the hand is worth two in the dumpster",
			},
	};
	
	static String GetSeedString(int race)
	{
		int min = 0;
		int max = raceSeedStrings[race].length -1;
	    int rando = (int)Math.floor(Math.random()*(max-min+1)+min);
	    return raceSeedStrings[race][rando];
	}
	
	static String EncodeString(char[] beta, String seed)
	{
		char[] seedCharArray = seed.toCharArray();
		String returnString = "";
		
		for(int a = 0; a < seed.length(); a++)
		{
			boolean found = false;
			for(int b = 0; b < beta.length; b++)
			{
				if(seedCharArray[a] == alpha[b] || seedCharArray[a] == capitalAlpha[b])
				{
					returnString += beta[b];
					found = true;
					break;
				}
			}
			if(!found)
			{
				returnString += seedCharArray[a];
			}
		}		
		return returnString;
	}
	
	static char[] CreateRandomCipher()
	{
		char[] beta = {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
		//ISFSObject cipherObj = new SFSObject();
		
		for(int a = 0; a < 25; a++)
		{
			int min = 0;
			int max = 25;
		    int rando = (int)Math.floor(Math.random()*(max-min+1)+min);
		    while(rando == a)
		    {
		    	rando = (int)Math.floor(Math.random()*(max-min+1)+min);
		    }
		   
		    char betaA = beta[a];
		    beta[a] = beta[rando];
		    beta[rando] = betaA;
		}
		return beta;
	}
	
	static String InsertNewCipher(User user, IDBManager sfs2xDBManager, int race, String cipher, String seed)
	{
		String sql = "INSERT INTO bf_cipher (playerID, race, cipher, seed) VALUES (?,?,?,?);";
		int userID = user.getVariable("dbid").getIntValue();
		try
		{
			sfs2xDBManager.executeUpdate(sql, new Object[] {userID, race, cipher, seed });
			return "inserted";
		}
		catch(SQLException e)
        {
            return "SQL Failed: " + e.toString();
        }
	}
	
	public static String CreateNewCipher(User user, IDBManager sfs2xDBManager,int race)
	{
		ISFSArray cipherArray;
		if(user.containsVariable("c"))
		{
			//cipherArray = user.
			cipherArray = user.getVariable("c").getSFSArrayValue();//(List<ISFSObject>)user.getVariable("c");
			for(int a = 0; a < cipherArray.size(); a++)
			{
				ISFSObject userVarObj = (ISFSObject) cipherArray.getElementAt(a);
				if(race == userVarObj.getInt("r"))
				{
					return "exists";
				}
			}
			
		}
		else
		{
			cipherArray = new SFSArray();
		}
		
		String seed = GetSeedString(race);
		char[] cipher = CreateRandomCipher();
		String cipherString = new String(cipher);
		
		ISFSObject userVarObj = new SFSObject();
    	
    	userVarObj.putInt("r", race);
    	userVarObj.putUtfString("c", cipherString);
    	userVarObj.putUtfString("s", seed);
    	
    	cipherArray.addSFSObject(userVarObj);
		try 
		{
        	UserVariable ciphers = new SFSUserVariable("c", cipherArray);
        	ciphers.setHidden(true);
			user.setVariable(ciphers);
		} 
		catch (SFSVariableException s) 
		{
			return "Uservar Failed: " + s.toString();
		}
		
		String insert = InsertNewCipher(user, sfs2xDBManager, race, cipherString, seed);
		
		if(insert.equals("inserted"))
		{
			return "set";
		}
		else
		{
			return insert;
		}
	}
	
	public static String InitUserCiphers(User user,IDBManager sfs2xDBManager)
	{
		ISFSArray cipherArray = new SFSArray();
		int userID = user.getVariable("dbid").getIntValue();
		String sql = "SELECT * FROM bf_cipher WHERE playerID=" + userID;
		
		try
        {
            ISFSArray res = sfs2xDBManager.executeQuery(sql, new Object[] {});
            
            for(int a = 0; a < res.size(); a++)
            {
            	ISFSObject dbItem = res.getSFSObject(a);
            	ISFSObject userVarObj = new SFSObject();
            	
            	userVarObj.putInt("r", dbItem.getInt("race"));
            	userVarObj.putUtfString("c", dbItem.getUtfString("cipher"));
            	userVarObj.putUtfString("s", dbItem.getUtfString("seed"));
            	
            	cipherArray.addSFSObject(userVarObj);
            }
        }
        catch(SQLException e)
        {
            return "SQL Failed: " + e.toString();
        }
		
		if(cipherArray.size() > 0)
		{
			try 
			{
	        	UserVariable ciphers = new SFSUserVariable("c", cipherArray);
	        	ciphers.setHidden(true);
				user.setVariable(ciphers);
				return "set";
			} 
			catch (SFSVariableException s) 
			{
				return "Uservar Failed: " + s.toString();
			}
		}
		else
		{
			return "none";
		}
	}
	
	/*static String removeLastCharacter(String str) 
	{
		   String result = Optional.ofNullable(str)
		   .filter(sStr -> sStr.length() != 0)
		   .map(sStr -> sStr.substring(0, sStr.length() - 1))
		   .orElse(str);
		   return result;
	}
	
	static String GetStringFromCipher(ISFSObject cipherObj)
	{
		String returnString = "";
		for(int a = 0; a < alpha.length; a++)
		{
			//returnString += cipherObj.getUtfString(alpha[a]);
		}
		return returnString;
	}
	
	static ISFSObject GetCipherFromString(String str)
	{
		char[] charArray = str.toCharArray();
		ISFSObject cipherObj = new SFSObject();
		for(int a = 0; a < str.length(); a++)
		{
			//cipherObj.putUtfString(alpha[a].toString(),String.valueOf(charArray[a]));
		}
		return cipherObj;
	}
	
	
	*/
}
