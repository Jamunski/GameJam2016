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

public class VideoMenu : MonoBehaviour
{
    private GameObject m_OptionsMenuCanvas;
    private UnityEngine.EventSystems.EventSystem m_EventSystem;

    void Awake()
    {
        m_OptionsMenuCanvas = GameObject.Find("OptionsMenu");
        m_EventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
    }

    void OnEnable()
    {
        m_EventSystem.SetSelectedGameObject(GameObject.Find("Back"));
    }

    public void Back()
    {
        gameObject.SetActive(false);
        m_OptionsMenuCanvas.SetActive(true);
    }
}
