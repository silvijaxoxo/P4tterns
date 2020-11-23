using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour{
    
    public Timer timer;

    public void Click()
    {
        if (timer.IsTimerRunning())
            Pause();
        else
            Resume();
    }
	
	private void Pause()
    {
        timer.PauseTimer();
    }

    private void Resume()
    {
        timer.ResumeTimer();
    }
}
