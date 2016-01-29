/*********************************************HEADER*********************************************\
 * Created By: Jordan Vassilev
 * Last Updated on: 06/12/15
 * Function: 
\*********************************************HEADER*********************************************/
/*********************************************NOTES**********************************************\
 * 
\*********************************************NOTES**********************************************/
using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviourSubject
{
    private GameObject m_OptionsMenuCanvas;
    private UnityEngine.EventSystems.EventSystem m_EventSystem;

    void Awake()
    {
        AddObserver(GameManager.Instance);
        m_OptionsMenuCanvas = GameObject.Find("OptionsMenu");
        m_EventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
    }

    void OnEnable()
    {
        m_EventSystem.SetSelectedGameObject(GameObject.Find("Resume"));
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Notify(gameObject, GameEvent.Gameplay);
    }

    public void Restart()
    {
        Notify(gameObject, GameEvent.ReloadingScene);
    }

    public void Options()
    {
        gameObject.SetActive(false);
        m_OptionsMenuCanvas.SetActive(true);
    }

    public void MainMenu()
    {
        GameManager.Instance.m_SceneToLoad = "MainMenu";
        Notify(gameObject, GameEvent.LoadingScene);
    }
}
