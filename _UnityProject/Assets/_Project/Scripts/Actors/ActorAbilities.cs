/*********************************************HEADER*********************************************\
 * Created By: Jordan Vassilev
 * Last Updated on: 13/12/15
 * Function: 
\*********************************************HEADER*********************************************/
/*********************************************NOTES**********************************************\
 * Needs to be structured in such a way that abilities can be changed and calls to them will remain
 * the same...
\*********************************************NOTES**********************************************/
using UnityEngine;
using System.Collections;

public class ActorAbilities : MonoBehaviour
{
    private ActorStatistics m_Stats;

    void Start()
    {
        m_Stats = GetComponent<Actor>().m_Statistics;
    }

    public void Jump()
    {
        Debug.Log(gameObject.name + ": Jump");
    }

    public void Sprint()
    {
		Debug.Log(gameObject.name + ": Sprint");
        m_Stats.m_IsSprinting = true;
        m_Stats.CalculateSpeed();
    }

    public void Carry()
    {
        Debug.Log(gameObject.name + ": Carry");
    }

    public void Magic()
    {
        Debug.Log(gameObject.name + ": Magic");
    }
}
