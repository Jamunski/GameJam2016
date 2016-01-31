using UnityEngine;
using System.Collections;

public class StairRitual : MonoBehaviour, IRitual
{
    //MAX OF 1 -> 12
    const int TIME_OF_DAY_OF_OCCURANCE = 3;

    public bool Started
    {
        get
        {
            return (TimeSystem.TimeOfDayToHours() >= TIME_OF_DAY_OF_OCCURANCE);
        }

    }
    public bool Ended
    {
        get
        {
            return (TimeSystem.TimeOfDayToHours() >= TIME_OF_DAY_OF_OCCURANCE + 2);
        }
    }

    public bool InitRitual { get; set; }
    public bool ConditionComplete { get; set; }

    public GameObject m_ConditionBox;
    public TriggerBox m_TriggerBox;

    public void Condition()
    {
        if (Started && !Ended)
        {
            if (m_TriggerBox.KeyObjectIsIn)
            {
                ConditionComplete = true;
//                Debug.Log(ConditionComplete);
            }
        }
    }

    public GameObject[] m_Positions;
    int Counter = 0;

    public void Action(NavMeshAgent aAgent)
    {
        Debug.Log(Counter);
        aAgent.SetDestination(m_Positions[Counter].transform.position);
        ++Counter;
        if(Counter > m_Positions.Length - 1)
        {
            Counter = 0;
        }
    }





}
