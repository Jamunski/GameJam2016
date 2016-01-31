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

public class ActorMovement : MonoBehaviour
{
    //private Member variables
    private Actor m_Player = null;
    private FollowCamera m_Camera;
    private Rigidbody m_PlayerRigidBody = null;
    private float m_RotationSpeed = Mathf.PI * 60;
    Vector3 m_Velocity = Vector3.zero;

    //Unity Callbacks
    void Start()
    {
        m_Player = gameObject.GetComponent<Actor>();
        m_PlayerRigidBody = GetComponent<Rigidbody>();

        m_Camera = m_Player.m_Camera;
    }

    void FixedUpdate()
    {
        m_PlayerRigidBody.velocity = m_Velocity;
    }

    //public Methods
    public void Movement(Vector3 aInput)
    {
        try
        {
            if (aInput != Vector3.zero)
            {
                m_Velocity = (m_Camera.transform.forward * -aInput.y * m_Player.m_Statistics.m_FinalSpeed);
                m_Velocity.y = 0;
                Vector3.Normalize(m_Velocity);
            }
            else
            {
                m_Velocity.y += Physics.gravity.y * Time.fixedDeltaTime;
                m_Velocity = new Vector3(Mathf.Lerp(m_Velocity.x, 0, Time.fixedDeltaTime * 5), m_Velocity.y, Mathf.Lerp(m_Velocity.x, 0, Time.fixedDeltaTime * 5));
            }
            

            

            Vector3 cameraVector = new Vector3(m_Camera.transform.forward.x, 0, m_Camera.transform.forward.z);
            transform.rotation = Quaternion.LookRotation(cameraVector);
        }
        catch
        {
            Debug.Log("No Camera");
            Vector3 velocity = Vector3.zero;

            velocity = (transform.forward * -aInput.y * m_Player.m_Statistics.m_FinalSpeed);
            velocity.y = 0;

            Vector3.Normalize(velocity);

            transform.position += velocity * Time.deltaTime;
        }
    }

    public void Camera(Vector3 aInput)
    {
        try
        {
            Quaternion newRotation = Quaternion.identity;

            newRotation = Quaternion.Euler(new Vector3(aInput.y * Time.fixedDeltaTime * m_RotationSpeed, aInput.x * Time.fixedDeltaTime * m_RotationSpeed, 0));

            m_Camera.m_CameraRotationY = newRotation.eulerAngles.y;
            m_Camera.m_CameraRotationX = -newRotation.eulerAngles.x;
        }
        catch
        {
            Debug.Log("No Camera??");
            Quaternion newRotation = Quaternion.identity;

            newRotation = Quaternion.Euler(new Vector3(aInput.y * Time.fixedDeltaTime * m_RotationSpeed, aInput.x * Time.fixedDeltaTime * m_RotationSpeed, 0));

            m_Player.transform.rotation *= newRotation;
        }
    }
}