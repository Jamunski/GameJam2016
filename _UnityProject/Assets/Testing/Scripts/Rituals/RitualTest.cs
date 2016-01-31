using UnityEngine;
using System.Collections;

/// <summary>
/// This Ritual is a Move a Box(ConditionBox) into the TriggerBox
/// </summary>
public class RitualTest : MonoBehaviour, IRitual
{
    //MAX OF 1 -> 12
    const int TIME_OF_DAY_OF_OCCURANCE = 6;

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
                Debug.Log(ConditionComplete);
            }
        }
    }

    public void Action(NavMeshAgent aAgent)
    {
        aAgent.SetDestination(m_TriggerBox.transform.position);
    }





}
