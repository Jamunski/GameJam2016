/*********************************************HEADER*********************************************\
 * Created By: Jordan Vassilev
 * Last Updated on: 06/12/15
 * Function: Any class which needs to be observed by any observer classes, such as the GameManager
 * and InputManager, must inherit from this class
\*********************************************HEADER*********************************************/
/*********************************************NOTES**********************************************\
 * 
\*********************************************NOTES**********************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonoBehaviourSubject : MonoBehaviour
{
    public string m_ParentName;

    List<Observer> m_ObserverList;
    int m_NumObservers;

    private void InitializeList()
    {
        m_ObserverList = new List<Observer>();
    }

    public void Notify(GameObject aEntity, GameEvent aEvent) // Entity responsible for the event and the event that occured
    {
        for (int i = 0; i < m_NumObservers; i++)
        {
            m_ObserverList[i].OnNotify(ref aEntity, aEvent);
        }
    }

    public void AddObserver(Observer aObserver)
    {
        if(m_ObserverList == null)
        {
            InitializeList();
        }
        if (!m_ObserverList.Contains(aObserver))
        {
            m_ObserverList.Add(aObserver);
            m_NumObservers++;
        }
    }

    public void RemoveObserver(Observer aObserver)
    {
        m_ObserverList.Remove(aObserver);
    }
}
