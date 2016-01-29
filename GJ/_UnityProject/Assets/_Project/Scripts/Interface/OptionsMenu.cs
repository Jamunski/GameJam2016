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

public class OptionsMenu : MonoBehaviour
{
    private GameObject m_MainMenuCanvas;
    private GameObject m_InputMenuCanvas;
    private GameObject m_VideoMenuCanvas;
    private GameObject m_PauseMenuCanvas;
    private UnityEngine.EventSystems.EventSystem m_EventSystem;

    void Awake()
    {
        m_EventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
        m_InputMenuCanvas = GameObject.Find("InputMenu");
        if(GameObject.Find("VideoMenu") != null)
        m_VideoMenuCanvas = GameObject.Find("VideoMenu");
        if (GameObject.Find("MainMenu") != null)
        m_MainMenuCanvas = GameObject.Find("MainMenu");
        if (GameObject.Find("PauseMenu") != null)
        m_PauseMenuCanvas = GameObject.Find("PauseMenu");
    }

    void OnEnable()
    {
        m_EventSystem.SetSelectedGameObject(GameObject.Find("Input"));
    }

    public void Input()
    {
        gameObject.SetActive(false);
        m_InputMenuCanvas.SetActive(true);
    }

    public void Video()
    {
        gameObject.SetActive(false);
        m_VideoMenuCanvas.SetActive(true);
    }

    public void Back()
    {
        gameObject.SetActive(false);
        if (m_MainMenuCanvas != null)
        {
            m_MainMenuCanvas.SetActive(true);
        }
        if (m_PauseMenuCanvas != null)
        {
            m_PauseMenuCanvas.SetActive(true);
        }
    }
}
