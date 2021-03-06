package battlefield;

import com.smartfoxserver.v2.core.ISFSEvent;
import com.smartfoxserver.v2.core.SFSEventParam;
import com.smartfoxserver.v2.db.IDBManager;
import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.exceptions.SFSException;
import com.smartfoxserver.v2.extensions.BaseServerEventHandler;
import com.smartfoxserver.v2.persistence.room.SFSStorageException;

public class RoomRemovedEvent extends BaseServerEventHandler
{
	IDBManager sfs2xDBManager;
	@Override
	public void handleServerEvent(ISFSEvent event) throws SFSException 
	{
		sfs2xDBManager = getParentExtension().getParentZone().getDBManager();
		Room room = (Room) event.getParameter(SFSEventParam.ROOM);
		
		try
		{
			getParentExtension().getParentZone().getRoomPersistenceApi().saveRoom(room);
		}
		catch (SFSStorageException a)
		{
			trace("catch" + a);
		}
	}
}
