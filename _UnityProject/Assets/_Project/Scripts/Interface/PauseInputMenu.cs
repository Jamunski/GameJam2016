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

public class PauseInputMenu : MonoBehaviour
{
    private GameObject m_OptionsMenuCanvas;
    private UnityEngine.EventSystems.EventSystem m_EventSystem;
    private InputStrings m_Player;

    #region // ButtonTextObjects
    private Text m_InputTypeText;
    private Text m_InteractText;
    private Text m_JumpText;
    private Text m_SprintText;
    private Text m_AttackText;
    private Text m_PauseText;
    private Text m_HelpText;
    #endregion

    public PlayerNumber m_PlayerNumber;

    void Awake()
    {
        m_OptionsMenuCanvas = GameObject.Find("OptionsMenu");
        m_EventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();

        m_InputTypeText = GameObject.Find("KeyboardOrXbox").GetComponentInChildren<Text>();
        m_InteractText = GameObject.Find("Interact").GetComponentInChildren<Text>();
        m_JumpText = GameObject.Find("Jump").GetComponentInChildren<Text>();
        m_SprintText = GameObject.Find("Sprint").GetComponentInChildren<Text>();
        m_AttackText = GameObject.Find("Carry").GetComponentInChildren<Text>();
        m_PauseText = GameObject.Find("Pause").GetComponentInChildren<Text>();
        m_HelpText = GameObject.Find("Help").GetComponentInChildren<Text>();
    }

    void OnEnable()
    {
        m_EventSystem.SetSelectedGameObject(GameObject.Find("KeyboardOrXbox"));
        m_Player = InputManager.Instance.m_InputStrings[(int)m_PlayerNumber]; // Set the player string to be = to the playerString of the player who invoked pause...
        RefreshButtonText();
    }

    void RefreshButtonText()
    {
        m_InputTypeText.text = "Input: " + m_Player.InputType;
        m_InteractText.text = "Interact: " + m_Player.InteractInput;
        m_JumpText.text = "Jump: " + m_Player.JumpInput;
        m_SprintText.text = "Sprint: " + m_Player.SprintInput;
        m_AttackText.text = "Carry: " + m_Player.CarryInput;
        m_PauseText.text = "Pause: " + m_Player.PauseInput;
        m_HelpText.text = "Help: " + m_Player.HelpInput;
    }

    public void CycleControlType(Button aButton)
    {
        //Cycle between Keyboard or Xbox controls
        if (aButton.name == "KeyboardOrXbox")
        {
            switch (m_Player.InputType)
            {
                case InputType.Joystick:
                    {
                        InputManager.Instance.SetInputType(ref m_Player, InputType.Keyboard);
                    }
                    break;
                case InputType.Keyboard:
                    {
                        InputManager.Instance.SetInputType(ref m_Player, InputType.Joystick);
                    }
                    break;
            }
            aButton.GetComponentInChildren<Text>().text = "Input: " + m_Player.InputType;
            InputManager.Instance.ReInitializeInputStrings(ref m_Player);
            InputManager.Instance.m_InputStrings[(int)m_PlayerNumber] = m_Player;
            InputManager.Instance.SetPlayerControllerNumbers();
            Debug.Log(InputManager.Instance.m_InputStrings[(int)PlayerNumber.One].InputType);
        }
        RefreshButtonText();
    }

    public void Interact(Button aButton)
    {
        if (aButton.name == "Interact")
        {
            //Change interact input
            //InputManager.Instance.SetInputString(ref m_Player1.InteractInput);
            aButton.GetComponentInChildren<Text>().text = "Interact: " + m_Player.InteractInput;
        }
    }

    public void Jump(Button aButton)
    {
        if (aButton.name == "Jump")
        {
            //InputManager.Instance.SetInputString(ref m_Player1.JumpInput);
            aButton.GetComponentInChildren<Text>().text = "Jump: " + m_Player.JumpInput;
        }
    }

    public void Sprint(Button aButton)
    {
        if (aButton.name == "Sprint")
        {
            //InputManager.Instance.SetInputString(ref m_Player1.SprintInput);
            aButton.GetComponentInChildren<Text>().text = "Sprint: " + m_Player.SprintInput;
        }
    }

    public void Attack(Button aButton)
    {
        if (aButton.name == "Carry")
        {
            //InputManager.Instance.SetInputString(ref m_Player1.AttackInput);
            aButton.GetComponentInChildren<Text>().text = "Carry: " + m_Player.CarryInput;
        }
    }

    public void Pause(Button aButton)
    {
        if (aButton.name == "Pause")
        {
            //InputManager.Instance.SetInputString(ref m_Player1.PauseInput);
            aButton.GetComponentInChildren<Text>().text = "Pause: " + m_Player.PauseInput;
        }
    }

    public void Help(Button aButton)
    {
        if (aButton.name == "Help")
        {
            //InputManager.Instance.SetInputString(ref m_Player1.HelpInput);
            aButton.GetComponentInChildren<Text>().text = "Help: " + m_Player.HelpInput;
        }
    }

    public void Back()
    {
        gameObject.SetActive(false);
        m_OptionsMenuCanvas.SetActive(true);
    }
}