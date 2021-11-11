package battlefield;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

import com.smartfoxserver.v2.SmartFoxServer;
import com.smartfoxserver.v2.extensions.SFSExtension;
import com.smartfoxserver.v2.util.TaskScheduler;

public class CampaignRoomExtension extends SFSExtension 
{
	TaskScheduler scheduler = new TaskScheduler(2);
	
	List<ScheduledFuture<?>> sf = new ArrayList<ScheduledFuture<?>>();
	
	private class firstSlotRunner implements Runnable
    {
		int slot;
        private int runningCycles = 0;
 
        public firstSlotRunner(int s)
        {
        	slot = s;
        	trace("constructor");
        }
        public void run()
        {
        	trace("running");
            try
            {
            	trace("in loop " + slot);
                if (runningCycles > 0)
                {
                	trace("should end " + slot);
                	sf.get(slot).cancel(true);
                }
                runningCycles++;
            }
            catch (Exception e)
            {
                // Handle exceptions here
            }
        }
    }
	
	/*public void givenUsingTimer_whenSchedulingTaskOnce_thenCorrect() 
	{
	    TimerTask task = new TimerTask() 
	    {
	        public void run() 
	        {
	            System.out.println("Task performed on: " + new Date() + "n" +
	              "Thread's name: " + Thread.currentThread().getName());
	        }
	    };
	    
	    Timer timer = new Timer("Timer");
	    
	    long delay = 1000L;
	    timer.schedule(task, delay);
	}*/
	@Override
	public void init() 
	{
		trace("init room");
		SmartFoxServer sfs = SmartFoxServer.getInstance();
		int index = sf.size();
		ScheduledFuture<?> firstSlotScheduledFuture = sfs.getTaskScheduler().scheduleAtFixedRate(new firstSlotRunner(index), 0, 1, TimeUnit.SECONDS);
		sf.add(firstSlotScheduledFuture);
	}
}
