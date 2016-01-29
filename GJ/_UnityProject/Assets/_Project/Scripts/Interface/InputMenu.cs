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
//using System.Collections.Generic;
using UnityEngine.UI;

public class InputMenu : MonoBehaviour
{
    private GameObject m_OptionsMenuCanvas;
    private UnityEngine.EventSystems.EventSystem m_EventSystem;
    private InputStrings m_Player1;
    private InputStrings m_Player2;

    #region // ButtonTextObjects
    private Text m_InputTypeText;
    private Text m_InputTypeText2;
    private Text m_InteractText;
    private Text m_InteractText2;
    private Text m_JumpText;
    private Text m_JumpText2;
    private Text m_SprintText;
    private Text m_SprintText2;
    private Text m_AttackText;
    private Text m_AttackText2;
    private Text m_PauseText;
    private Text m_PauseText2;
    private Text m_HelpText;
    private Text m_HelpText2;
    #endregion

    void Awake()
    {
        m_OptionsMenuCanvas = GameObject.Find("OptionsMenu");
        m_EventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();

        m_InputTypeText = GameObject.Find("KeyboardOrXbox").GetComponentInChildren<Text>();
        m_InteractText = GameObject.Find("Interact").GetComponentInChildren<Text>();
        m_JumpText = GameObject.Find("Jump").GetComponentInChildren<Text>();
        m_SprintText = GameObject.Find("Sprint").GetComponentInChildren<Text>();
        m_AttackText = GameObject.Find("Attack").GetComponentInChildren<Text>();
        m_PauseText = GameObject.Find("Pause").GetComponentInChildren<Text>();
        m_HelpText = GameObject.Find("Help").GetComponentInChildren<Text>();

        if (GameObject.Find("KeyboardOrXbox 2"))
        {
            m_InputTypeText2 = GameObject.Find("KeyboardOrXbox 2").GetComponentInChildren<Text>();
            m_InteractText2 = GameObject.Find("Interact 2").GetComponentInChildren<Text>();
            m_JumpText2 = GameObject.Find("Jump 2").GetComponentInChildren<Text>();
            m_SprintText2 = GameObject.Find("Sprint 2").GetComponentInChildren<Text>();
            m_AttackText2 = GameObject.Find("Attack 2").GetComponentInChildren<Text>();
            m_PauseText2 = GameObject.Find("Pause 2").GetComponentInChildren<Text>();
            m_HelpText2 = GameObject.Find("Help 2").GetComponentInChildren<Text>();
        }
    }

    void OnEnable()
    {
        m_EventSystem.SetSelectedGameObject(GameObject.Find("KeyboardOrXbox"));
        m_Player1 = InputManager.Instance.m_InputStrings[(int)PlayerNumber.One];
        m_Player2 = InputManager.Instance.m_InputStrings[(int)PlayerNumber.Two];
        RefreshButtonText();
    }

    void RefreshButtonText()
    {
        m_InputTypeText.text = "Input: " + m_Player1.InputType;
        m_InteractText.text = "Interact: " + m_Player1.InteractInput;
        m_JumpText.text = "Jump: " + m_Player1.JumpInput;
        m_SprintText.text = "Sprint: " + m_Player1.SprintInput;
        m_AttackText.text = "Attack: " + m_Player1.AttackInput;
        m_PauseText.text = "Pause: " + m_Player1.PauseInput;
        m_HelpText.text = "Help: " + m_Player1.HelpInput;

        if (GameObject.Find("KeyboardOrXbox 2"))
        {
            m_InputTypeText2.text = "Input: " + m_Player2.InputType;
            m_InteractText2.text = "Interact: " + m_Player2.InteractInput;
            m_JumpText2.text = "Jump: " + m_Player2.JumpInput;
            m_SprintText2.text = "Sprint: " + m_Player2.SprintInput;
            m_AttackText2.text = "Attack: " + m_Player2.AttackInput;
            m_PauseText2.text = "Pause: " + m_Player2.PauseInput;
            m_HelpText2.text = "Help: " + m_Player2.HelpInput;
        }
    }

    public void CycleControlType(Button aButton)
    {
        //Cycle between Keyboard or Xbox controls
        if (aButton.name == "KeyboardOrXbox")
        {
            switch (m_Player1.InputType)
            {
                case InputType.Joystick:
                    {
                        InputManager.Instance.SetInputType(ref m_Player1, InputType.Keyboard);
                    }
                    break;
                case InputType.Keyboard:
                    {
                        InputManager.Instance.SetInputType(ref m_Player1, InputType.Joystick);
                    }
                    break;
            }
            aButton.GetComponentInChildren<Text>().text = "Input: " + m_Player1.InputType;
            InputManager.Instance.ReInitializeInputStrings(ref m_Player1);
            InputManager.Instance.m_InputStrings[(int)PlayerNumber.One] = m_Player1;
            InputManager.Instance.SetPlayerControllerNumbers();
            Debug.Log(InputManager.Instance.m_InputStrings[(int)PlayerNumber.One].InputType);
        }
        else
        {
            switch (m_Player2.InputType)
            {
                case InputType.Joystick:
                    {
                        InputManager.Instance.SetInputType(ref m_Player2, InputType.Keyboard);
                    }
                    break;
                case InputType.Keyboard:
                    {
                        InputManager.Instance.SetInputType(ref m_Player2, InputType.Joystick);
                    }
                    break;
            }
            aButton.GetComponentInChildren<Text>().text = "Input: " + m_Player2.InputType;
            InputManager.Instance.ReInitializeInputStrings(ref m_Player2);
            InputManager.Instance.m_InputStrings[(int)PlayerNumber.Two] = m_Player2;
            InputManager.Instance.SetPlayerControllerNumbers();
            Debug.Log(InputManager.Instance.m_InputStrings[(int)PlayerNumber.Two].InputType);
        }
        RefreshButtonText();
    }

    public void Interact(Button aButton)
    {
        //Change interact input
        if (aButton.name == "Interact")
        {
            //InputManager.Instance.SetInputString(ref m_Player1.InteractInput);
            aButton.GetComponentInChildren<Text>().text = "Interact: " + m_Player1.InteractInput;
        }
        else
        {
            //InputManager.Instance.SetInputString(ref m_Player2.InteractInput);
            aButton.GetComponentInChildren<Text>().text = "Interact: " + m_Player2.InteractInput;
        }
    }

    public void Jump(Button aButton)
    {
        if (aButton.name == "Jump")
        {
            //InputManager.Instance.SetInputString(ref m_Player1.JumpInput);
            aButton.GetComponentInChildren<Text>().text = "Jump: " + m_Player1.JumpInput;
        }
        else
        {
            //InputManager.Instance.SetInputString(ref m_Player2.JumpInput);
            aButton.GetComponentInChildren<Text>().text = "Jump: " + m_Player2.JumpInput;
        }
    }

    public void Sprint(Button aButton)
    {
        if (aButton.name == "Sprint")
        {
            //InputManager.Instance.SetInputString(ref m_Player1.SprintInput);
            aButton.GetComponentInChildren<Text>().text = "Sprint: " + m_Player1.SprintInput;
        }
        else
        {
            //InputManager.Instance.SetInputString(ref m_Player2.SprintInput);
            aButton.GetComponentInChildren<Text>().text = "Sprint: " + m_Player2.SprintInput;
        }
    }

    public void Attack(Button aButton)
    {
        if (aButton.name == "Attack")
        {
            //InputManager.Instance.SetInputString(ref m_Player1.AttackInput);
            aButton.GetComponentInChildren<Text>().text = "Attack: " + m_Player1.AttackInput;
        }
        else
        {
            //InputManager.Instance.SetInputString(ref m_Player2.AttackInput);
            aButton.GetComponentInChildren<Text>().text = "Attack: " + m_Player2.AttackInput;
        }
    }

    public void Pause(Button aButton)
    {
        if (aButton.name == "Pause")
        {
            //InputManager.Instance.SetInputString(ref m_Player1.PauseInput);
            aButton.GetComponentInChildren<Text>().text = "Pause: " + m_Player1.PauseInput;
        }
        else
        {
            //InputManager.Instance.SetInputString(ref m_Player2.PauseInput);
            aButton.GetComponentInChildren<Text>().text = "Pause: " + m_Player2.PauseInput;
        }
    }

    public void Help(Button aButton)
    {
        if (aButton.name == "Help")
        {
            //InputManager.Instance.SetInputString(ref m_Player1.HelpInput);
            aButton.GetComponentInChildren<Text>().text = "Help: " + m_Player1.HelpInput;
        }
        else
        {
            //InputManager.Instance.SetInputString(ref m_Player2.HelpInput);
            aButton.GetComponentInChildren<Text>().text = "Help: " + m_Player2.HelpInput;
        }
    }

    public void Back()
    {
        gameObject.SetActive(false);
        m_OptionsMenuCanvas.SetActive(true);
    }
}