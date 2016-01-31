using UnityEngine;
using System.Collections;

public class Timer
{
	float CurrentTime;
	float TotalTime;
	const float FINISH_TIME = 0.0f;

	bool TimeStarted;

	public Timer()
	{

	}

	public void TimerUpdate()
	{
		if (TimeStarted) 
		{
			CurrentTime -= Time.deltaTime;
		}
	}

	public void StartTime(float aDelay)
	{
		TotalTime = aDelay;
		CurrentTime = aDelay;

		TimeStarted = true;
	}

    public void CancalTime()
    {
        TimeStarted = false;
    }

	public bool TimeFinished
	{
		get
		{
			if (CurrentTime <= FINISH_TIME) 
			{
				TotalTime = 0.0f;
				TimeStarted = false;
				return true;
			}
			return false;
		}
	}

	public bool Delay(float aAmount)
	{
		if (TimeStarted == false) 
		{
			StartTime (aAmount);
		}
		return TimeFinished;
	}

    public bool Delay(float aAmount, float aDeltaTime)
    {
        if (TimeStarted == false)
        {
            StartTime(aAmount);
        }
        else
        {
            CurrentTime -= aDeltaTime;
        }
        return TimeFinished;
    }

	public float PercentOfPool()
	{
		if (CurrentTime / TotalTime > 0.01f) 
		{
			return CurrentTime / TotalTime;
		} 
		else 
		{
			return 0.0f;
		}
	}

	public float DebugTimer()
	{
		return CurrentTime;
	}
}
