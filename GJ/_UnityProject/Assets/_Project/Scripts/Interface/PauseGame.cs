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

public class PauseGame : MonoBehaviourSubject
{
    private GameObject m_PauseCanvas;
    private GameObject m_OptionsCanvas;
    private GameObject m_InputCanvas;
    private PauseInputMenu m_InputMenu;

    // Use this for initialization
    void Start()
    {
        AddObserver(GameManager.Instance);
        m_PauseCanvas = GameObject.Find("PauseMenu");
        m_OptionsCanvas = GameObject.Find("OptionsMenu");
        m_InputCanvas = GameObject.Find("InputMenu");

        m_InputMenu = m_InputCanvas.GetComponent<PauseInputMenu>();

        m_PauseCanvas.SetActive(false);
        m_OptionsCanvas.SetActive(false);
        m_InputCanvas.SetActive(false);
    }

    public void Pause(GameObject aPauser)
    {
        if (!m_PauseCanvas.activeSelf && !m_OptionsCanvas.activeSelf && !m_InputCanvas.activeSelf)
        {
            m_PauseCanvas.SetActive(true);
            Notify(aPauser, GameEvent.Pausing);
            m_InputMenu.m_PlayerNumber = aPauser.GetComponent<Actor>().PlayerNumber;
        }
        else if (m_PauseCanvas.activeSelf)
        {
            m_PauseCanvas.SetActive(false);
            Notify(aPauser, GameEvent.Gameplay);
        }
        else if (m_OptionsCanvas.activeSelf)
        {
            m_OptionsCanvas.SetActive(false);
            Notify(aPauser, GameEvent.Gameplay);
        }
        else if (m_InputCanvas.activeSelf)
        {
            m_InputCanvas.SetActive(false);
            Notify(aPauser, GameEvent.Gameplay);
        }
    }
}
