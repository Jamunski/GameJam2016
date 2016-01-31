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
    private Animator m_Animator;
    private FollowCamera m_Camera;
    private Rigidbody m_PlayerRigidBody = null;
    private float m_RotationSpeed = Mathf.PI * 30;
    Vector3 m_Velocity = Vector3.zero;

    //Unity Callbacks
    void Start()
    {
        m_Player = gameObject.GetComponent<Actor>();
        m_PlayerRigidBody = GetComponent<Rigidbody>();

        m_Camera = m_Player.m_Camera;

        m_Animator = GetComponent<Animator>();
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
                float currentSpeed = m_Velocity.magnitude;
                float accel = 5f;

                currentSpeed += accel * Time.fixedDeltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, m_Player.m_Statistics.m_FinalSpeed);

                float oldVelY = m_Velocity.y;
                m_Velocity = (Vector3.ProjectOnPlane(m_Camera.transform.forward,Vector3.up).normalized * -aInput.y * currentSpeed);
                
                if(!Physics.Raycast(transform.position,Vector3.down, 0.22f,LayerMask.GetMask("Camera Obstacle")))
                    m_Velocity.y = oldVelY + Physics.gravity.y * Time.fixedDeltaTime;

                //m_Velocity.y = 0;
                //Vector3.Normalize(m_Velocity);
                m_Animator.SetBool("isWalking", true);
            }
            else
            {
                //if(m_Velocity.magnitude <0.1f)
                //{
                    m_Velocity = new Vector3(0,m_Velocity.y, 0);
                //}
                //else
                //{
                //    // //Clamp shit
                    //float velX = m_Velocity.x;
                    //float velZ = m_Velocity.z;


                    //if (velX < 0)
                    //{
                    //    velX = Mathf.Clamp(velX, -m_Player.m_Statistics.m_FinalSpeed, 0);
                    //}
                    //else
                    //{
                    //    velX = Mathf.Clamp(velX, 0, m_Player.m_Statistics.m_FinalSpeed);
                    //}

                    //if (velZ < 0)
                    //{
                    //    velZ = Mathf.Clamp(velZ, -m_Player.m_Statistics.m_FinalSpeed, 0);
                    //}
                    //else
                    //{
                    //    velZ = Mathf.Clamp(velZ, 0, m_Player.m_Statistics.m_FinalSpeed);
                    //}
                    

                    //m_Velocity = new Vector3(Mathf.Lerp(m_Velocity.x, 0, Time.fixedDeltaTime * 5), m_Velocity.y, Mathf.Lerp(m_Velocity.x, 0, Time.fixedDeltaTime * 5));
                   
                    //m_Velocity = new Vector3(velX, m_Velocity.y, velZ);

                    m_Velocity.y += Physics.gravity.y * Time.fixedDeltaTime;

                    m_Animator.SetBool("isWalking", false);
                //}
               
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

            m_Camera.m_CameraRotationY = aInput.x * Time.fixedDeltaTime * m_RotationSpeed;
            m_Camera.m_CameraRotationX = -aInput.y * Time.fixedDeltaTime * m_RotationSpeed;
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