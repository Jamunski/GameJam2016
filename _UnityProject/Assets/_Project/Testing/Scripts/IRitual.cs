using UnityEngine;
using System.Collections;

public interface IRitual
{
    bool Started { get;}
    bool Ended { get;}

    bool InitRitual { get; set; }

    bool ConditionComplete { get; set; }

    /// <summary>
    /// Condition to Frame Kid
    /// </summary>
    /// <returns> Returns Ture if Kid is Eff'd</returns>
    void Condition();


    /// <summary>
    /// What the Child will do during the Event
    /// </summary>
    void Action(NavMeshAgent aAgent);
}
