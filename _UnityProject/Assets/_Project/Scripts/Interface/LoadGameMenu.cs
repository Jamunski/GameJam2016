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

public class LoadGameMenu : MonoBehaviour
{
    private GameObject m_MainMenuCanvas;
    private UnityEngine.EventSystems.EventSystem m_EventSystem;

    void Awake()
    {
        m_MainMenuCanvas = GameObject.Find("MainMenu");
        m_EventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
    }

    void OnEnable()
    {
        m_EventSystem.SetSelectedGameObject(GameObject.Find("LatestSave"));
    }

    public void LatestSave()
    {

    }

    public void Back()
    {
        gameObject.SetActive(false);
        m_MainMenuCanvas.SetActive(true);
    }
}
