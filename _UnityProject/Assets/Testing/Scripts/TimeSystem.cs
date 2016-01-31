using UnityEngine;
using System.Collections;

[System.Serializable]
public class TimeSystem : System.Object
{
    public static float m_TimeOfDay;
    public float TimeOfDay;
    //Calculated Hours
    public const float SECONDS_PER_DAY = 360.0f;
    public const float SECONDS_PER_HOUR = 30.0f;


    public TimeSystem()
    {

    }

    public bool UpdateTime()
    {
        TimeOfDay += Time.deltaTime;
        m_TimeOfDay = TimeOfDay ;


        if(DayIsOver)
        {
            TimeOfDay = 0;
            m_TimeOfDay = 0;
            return true;
        }

        return false;
    }

    public static float TimeOfDayToHours()
    {
        return Mathf.Floor(m_TimeOfDay / SECONDS_PER_HOUR);
    }

    public static float HoursToSeconds(int aHours)
    {
        return SECONDS_PER_HOUR * aHours;
    }


    bool DayIsOver
    {
        get
        {
            return (m_TimeOfDay >= SECONDS_PER_DAY);
        }
    }

}
