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
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviourSubject
{
    private GameObject m_MainMenuCanvas;
    private UnityEngine.EventSystems.EventSystem m_EventSystem;

    void Awake()
    {
        AddObserver(GameManager.Instance);
        m_MainMenuCanvas = GameObject.Find("MainMenu");
        m_EventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
    }

    void OnEnable()
    {
        m_EventSystem.SetSelectedGameObject(GameObject.Find("Level01"));
    }

    public void SetButtonText()
    {
        //Not Finished...
        Button[] Things = GameObject.FindObjectsOfType<Button>();
        for (int i = 0; i < Things.Length; i++)
        {
            string fart = Things[i].GetComponent<Button>().name;
        }
    }

    public void LevelButton(Object aSceneToLoad)
    {
        GameManager.Instance.m_SceneToLoad = aSceneToLoad.name;
        Notify(gameObject, GameEvent.LoadingScene);
    }

    public void Back()
    {
        gameObject.SetActive(false);
        m_MainMenuCanvas.SetActive(true);
    }
}
