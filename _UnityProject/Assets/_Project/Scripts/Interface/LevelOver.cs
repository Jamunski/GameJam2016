/*********************************************HEADER*********************************************\
 * Created By: Jordan Vassilev
 * Last Updated on: 13/12/15
 * Function: 
\*********************************************HEADER*********************************************/
/*********************************************NOTES**********************************************\
 * 
\*********************************************NOTES**********************************************/
using UnityEngine;
using System.Collections;

public class LevelOver : MonoBehaviourSubject
{
    void Start()
    {
        AddObserver(GameManager.Instance);
    }

    public void Restart()
    {
        Notify(gameObject, GameEvent.ReloadingScene);
    }

    public void MainMenu()
    {
        GameManager.Instance.m_SceneToLoad = "MainMenu";
        Notify(gameObject, GameEvent.LoadingScene);
    }
}
