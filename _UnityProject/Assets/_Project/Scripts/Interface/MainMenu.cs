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

public class MainMenu : MonoBehaviourSubject
{
    private GameObject m_NewGameMenuCanvas;
    private GameObject m_LoadGameMenuCanvas;
    private GameObject m_LevelSelectMenuCanvas;
    private GameObject m_OptionsMenuCanvas;
    private GameObject m_InputMenuCanvas;
    private GameObject m_VideoMenuCanvas;
    private UnityEngine.EventSystems.EventSystem m_EventSystem;
    private Object m_ContinueScene;

    void Awake()
    {
        AddObserver(GameManager.Instance);
        Notify(gameObject, GameEvent.Menu);
        m_EventSystem = GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
    }

    void Start()
    {
        m_NewGameMenuCanvas = GameObject.Find("NewGameMenu");
        m_LoadGameMenuCanvas = GameObject.Find("LoadGameMenu");
        m_LevelSelectMenuCanvas = GameObject.Find("LevelSelectMenu");
        m_OptionsMenuCanvas = GameObject.Find("OptionsMenu");
        m_InputMenuCanvas = GameObject.Find("InputMenu");
        m_VideoMenuCanvas = GameObject.Find("VideoMenu");
        m_LevelSelectMenuCanvas.SetActive(false);
        m_OptionsMenuCanvas.SetActive(false);
        m_InputMenuCanvas.SetActive(false);
        m_VideoMenuCanvas.SetActive(false);
        m_EventSystem.SetSelectedGameObject(GameObject.Find("Play"));
    }

    void OnEnable()
    {
        m_EventSystem.SetSelectedGameObject(GameObject.Find("Play"));
    }

    public void Continue(Object aSceneToLoad)
    {
        GameManager.Instance.m_SceneToLoad = aSceneToLoad.name;
        Notify(gameObject, GameEvent.LoadingScene);
    }

    public void LevelSelect()
    {
        gameObject.SetActive(false);
        m_LevelSelectMenuCanvas.SetActive(true);
    }

    public void Options()
    {
        gameObject.SetActive(false);
        m_OptionsMenuCanvas.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
