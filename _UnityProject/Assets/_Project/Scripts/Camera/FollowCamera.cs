/*********************************************HEADER*********************************************\
 * Created By: Jordan Vassilev (modifed by Vicky The Great)
 * Last Updated on: 06/12/15
 * Function: 
\*********************************************HEADER*********************************************/
/*********************************************NOTES**********************************************\
 * 
\*********************************************NOTES**********************************************/
using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour, ICamera
{
	public GameObject m_Target;
	public Vector3 m_CameraOffset = new Vector3(0,2,-4);
	public float m_CameraRotationY;
	public float m_CameraRotationX;

    public float clampAmount = 60.0f;

	private float m_DistanceFromCamera;

    float m_RotX;
    float m_RotY;

    Vector3 m_InitDirVector;

	void Start()
	{
		transform.position = m_Target.transform.position + m_CameraOffset;
		m_DistanceFromCamera = Vector3.Distance(transform.position, m_Target.transform.position);
        m_InitDirVector = transform.position - m_Target.transform.position;
        m_RotY = transform.eulerAngles.y;
        m_RotX = transform.eulerAngles.x;
	}

	void Update()
	{

        Rotate(m_CameraRotationX, m_CameraRotationY);
        
        HandleObstacles();

        m_CameraRotationX = 0;
        m_CameraRotationY = 0;
	}
    
    private void Rotate(float aInputX, float aInputY)
    {
        float rotateAmountY = aInputY;
        float rotateAmountX = aInputX;
        float maxVerticalAngle = clampAmount;

        //Horizontal rotation
        m_RotY += rotateAmountY;

        //Vertical rotation
        m_RotX += rotateAmountX;

        //Clamp vertical rotation
        m_RotX = Mathf.Clamp(m_RotX, -maxVerticalAngle, maxVerticalAngle);

        //Make a variable for euler rotation
        Vector3 angles = new Vector3(m_RotX, m_RotY, 0.0f);

        //Rotate the initial direction vector to match the desired direction
        Vector3 newDir = Quaternion.Euler(angles) * Vector3.ProjectOnPlane(m_InitDirVector, Vector3.up);

        //Make look position equal to the target's position
        Vector3 lookPos = m_Target.transform.position;

        //Set direction vector's magnitude to match the goal distance from the player
        Vector3 direction = -newDir.normalized * m_DistanceFromCamera;

        //Set goal position
        transform.position = lookPos + direction;

        direction = transform.position - lookPos;

        
        //Rotate the initial direction vector to match the desired direction
        transform.forward = Quaternion.Euler(angles) * Vector3.ProjectOnPlane(m_InitDirVector.normalized, Vector3.up);
    }

    public static Vector3 LerpTo(float easeSpeed, Vector3 start, Vector3 end, float dt)
    {
        Vector3 diff = end - start;

        diff *= Mathf.Clamp(dt * easeSpeed, 0.0f, 1.0f);

        return diff + start;
    }
    
    public void HandleObstacles()
    {

        //Calculate the ray direction and return early if the ray has a length of zero
        Vector3 rayStart = m_Target.transform.position;
        Vector3 rayEnd = transform.position;

        Vector3 rayDir = rayEnd - rayStart;

        float rayDist = rayDir.magnitude;
        if (rayDist <= 0.0f)
        {
            return;
        }

        rayDir /= rayDist;

        RaycastHit[] hitInfos = Physics.SphereCastAll(rayStart, 0.2f, rayDir, rayDist, LayerMask.GetMask("Camera Obstacle"));
        if (hitInfos.Length <= 0)
        {
            return;
        }

        float minMoveUpDist = float.MaxValue;

        foreach (RaycastHit hitInfo in hitInfos)
        {
            //Calculate the move up distance, and ignore the obstacle if we already processed an 
            //obstacle that was closer
            float moveUpDist = hitInfo.distance;
            if (moveUpDist > minMoveUpDist)
            {
                continue;
            }
            //Debug.Log("Obstacle found!" + hitInfo.collider.gameObject.name);
            minMoveUpDist = moveUpDist;

        }

        //Move the camera up by the minimum distance
        if (minMoveUpDist < float.MaxValue)
        {
            transform.position = rayStart + rayDir * (minMoveUpDist - Camera.main.nearClipPlane*2);

        }
    }
}
