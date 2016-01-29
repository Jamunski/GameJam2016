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

public class FollowCamera : MonoBehaviour, ICamera
{
	public GameObject m_Target;
	public Vector3 m_CameraOffset = new Vector3(0,2,-4);
	public float m_CameraRotationY;
	public float m_CameraRotationX;

	private float m_DistanceFromCamera;

	void Start()
	{
		transform.position = m_Target.transform.position + m_CameraOffset;
		m_DistanceFromCamera = Vector3.Distance(transform.position, m_Target.transform.position);
	}

	void Update()
	{
		transform.RotateAround(m_Target.transform.position, Vector3.up, m_CameraRotationY);
		transform.RotateAround(m_Target.transform.position, transform.right, m_CameraRotationX);
		transform.LookAt(m_Target.transform.position);


		Vector3 thing = transform.position - m_Target.transform.position;
		transform.position = m_Target.transform.position + thing.normalized * m_DistanceFromCamera;
		m_CameraRotationY = 0;
		m_CameraRotationX = 0;
	}
}
