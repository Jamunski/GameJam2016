/*********************************************HEADER*********************************************\
 * Created By: Jordan Vassilev
 * Last Updated on: 06/12/15
 * Function: Capable of observing subjects
\*********************************************HEADER*********************************************/
/*********************************************NOTES**********************************************\
 * 
\*********************************************NOTES**********************************************/
using UnityEngine;
using System.Collections;

public abstract class Observer
{
    ~Observer() { }
    public abstract void OnNotify(ref GameObject aEntity, GameEvent aEvent);
}
