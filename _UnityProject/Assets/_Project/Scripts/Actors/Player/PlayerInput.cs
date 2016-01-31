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

public class PlayerInput : MonoBehaviour
{
    public string m_Input;
    private Actor m_Player = null;

    private bool isInteractPressed;
    private bool isJumpPressed;
    private bool isCarryPressed;
    private bool isPausePressed;
    private bool isHelpPressed;

    private float InteractValueLastFrame;
    private float JumpValueLastFrame;
    private float SprintValueLastFrame;
    private float CarryValueLastFrame;
    private float PauseValueLastFrame;
    private float HelpValueLastFrame;

    void Awake()
    {
        m_Player = gameObject.GetComponent<Actor>();
    }

    //Abstracts
    public Vector3 Camera()
    {
        //Mouse Stuff
        float axisX = Input.GetAxis(InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType + ((InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType == InputType.Joystick && InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].PlayerNumber != PlayerNumber.God) ? (InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].JoystickNumber.ToString()) : "") + "_" + InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].CameraXInput);
        float axisY = Input.GetAxis(InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType + ((InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType == InputType.Joystick && InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].PlayerNumber != PlayerNumber.God) ? (InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].JoystickNumber.ToString()) : "") + "_" + InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].CameraYInput);

        return new Vector3(axisX, axisY, 0);
    }

    public Vector3 Movement()
    {
        float axisX = Input.GetAxis(InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType + ((InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType == InputType.Joystick && InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].PlayerNumber != PlayerNumber.God) ? (InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].JoystickNumber.ToString()) : "") + "_" + InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].MoveXInput);
        float axisY = Input.GetAxis(InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType + ((InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType == InputType.Joystick && InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].PlayerNumber != PlayerNumber.God) ? (InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].JoystickNumber.ToString()) : "") + "_" + InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].MoveYInput);

        return new Vector3(axisX, -axisY, 0);
    }

    public float Interact() { return AxisDown(Input.GetAxis(InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType + ((InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType == InputType.Joystick && InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].PlayerNumber != PlayerNumber.God) ? (InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].JoystickNumber.ToString()) : "") + "_" + InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InteractInput), ref isInteractPressed, ref InteractValueLastFrame); }

    public float Jump() { return AxisDown(Input.GetAxis(InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType + ((InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType == InputType.Joystick && InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].PlayerNumber != PlayerNumber.God) ? (InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].JoystickNumber.ToString()) : "") + "_" + InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].JumpInput), ref isJumpPressed, ref JumpValueLastFrame); }

    public float Sprint() { return Input.GetAxis(InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType + ((InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType == InputType.Joystick && InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].PlayerNumber != PlayerNumber.God) ? (InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].JoystickNumber.ToString()) : "") + "_" + InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].SprintInput); }

    public float Carry() { return AxisDown(Input.GetAxis(InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType + ((InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType == InputType.Joystick && InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].PlayerNumber != PlayerNumber.God) ? (InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].JoystickNumber.ToString()) : "") + "_" + InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].CarryInput), ref isCarryPressed, ref CarryValueLastFrame); }

    public float Pause() { return AxisDown(Input.GetAxis(InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType + ((InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType == InputType.Joystick && InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].PlayerNumber != PlayerNumber.God) ? (InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].JoystickNumber.ToString()) : "") + "_" + InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].PauseInput), ref isPausePressed, ref PauseValueLastFrame); }

    public float Help() { return AxisDown(Input.GetAxis(InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType + ((InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].InputType == InputType.Joystick && InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].PlayerNumber != PlayerNumber.God) ? (InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].JoystickNumber.ToString()) : "") + "_" + InputManager.Instance.m_InputStrings[(int)m_Player.PlayerNumber].HelpInput), ref isHelpPressed, ref HelpValueLastFrame); }

    private float AxisDown(float aButtonPressed, ref bool aButtonLastFrame, ref float aButtonValueLastFrame)
    {
        bool isButtonPressed;
        float pressed = 0;
        //Only trigger once, trigger again only after the button has been released...
        if (aButtonPressed > aButtonValueLastFrame)
        {
            //Button pressed, set button pressed to true and don't register any more presses until the button is released.
            isButtonPressed = true;
        }
        else
        {
            isButtonPressed = false;
        }

        //if the button was not pressed last frame and the button was pressed this frame
        if (!aButtonLastFrame && isButtonPressed)
        {
            aButtonLastFrame = true;
            pressed = 1;
        }
        else if (aButtonLastFrame && isButtonPressed)
        {
            pressed = 0;
        }
        else
        {
            aButtonLastFrame = false;
        }

        aButtonValueLastFrame = aButtonPressed;

        return pressed;
    }

    //Trying to remove the need for the aButtonLastFrame member variable.... Not Finished :(
    private float AxisDown(float aButtonPressed, ref float aButtonValueLastFrame)
    {
        float pressed = 0;

        //if the button was not pressed last frame and the button was pressed this frame
        if (aButtonValueLastFrame < aButtonPressed)
        {
            aButtonValueLastFrame = aButtonPressed;
            pressed = 1;
            return pressed;
        }
        else
        {
            aButtonValueLastFrame = aButtonPressed;
            pressed = 0;
            return pressed;
        }
    }
}