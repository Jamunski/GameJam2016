/*********************************************HEADER*********************************************\
 * Created By: Jordan Vassilev
 * Last Updated on: 13/12/15
 * Function: 
\*********************************************HEADER*********************************************/
/*********************************************NOTES**********************************************\
 * Needs to be structured in such a way that abilities can be changed and calls to them will remain
 * the same...
\*********************************************NOTES**********************************************/
using UnityEngine;
using System.Collections;

public class ActorAbilities : MonoBehaviour
{
    private ActorStatistics m_Stats;
    private Actor m_Player;
    public bool isHoldingObject = false;

	ActorMovement m_Movement;

	public Coroutine m_JumpCR;

	public float JumpHeight;
	public float JumpSpeed;

    LayerMask PickableObjects;


    void Start()
    {
        m_Stats = GetComponent<Actor>().m_Statistics;
		m_Movement = GetComponent<ActorMovement> ();
		m_JumpCR = null;
		JumpHeight = 2;
		JumpSpeed = 2;
        m_Player = gameObject.GetComponent<Actor>();
        PickableObjects = LayerMask.GetMask("PickUps");
    }

    public void Jump()
    {
        Debug.Log(gameObject.name + ": Jump");

		if (m_JumpCR == null) 
		{
			Debug.Log("Jump Started");
			m_JumpCR = StartCoroutine (jump_cr ());
		}

    }

    public void Sprint()
    {
		Debug.Log(gameObject.name + ": Sprint");
        m_Stats.m_IsSprinting = true;
        m_Stats.CalculateSpeed();
    }

    public void Carry()
    {
        Debug.Log(gameObject.name + isHoldingObject);

        if (isHoldingObject)
        {
            DropItem();
            isHoldingObject = false;
        }
        else
        {
            Debug.Log(gameObject.name + ": dsfd");

            PickUp();
            isHoldingObject = true;
        }
        
    }

    public void Magic()
    {
        Debug.Log(gameObject.name + ": Magic");
    }

    public Transform PickedItem;
    Rigidbody ChildRB;

    void PickUp()
    {

        RaycastHit[] hitInfo = Physics.SphereCastAll(transform.position, 1.0f, transform.forward, 1.0f, PickableObjects);
        if (hitInfo.Length > 0)
        {
            if (PickedItem == null)
            {
                PickedItem = hitInfo[0].collider.transform;
                PickedItem.SetParent(transform);
                PickedItem.position = m_Player.Face.transform.position;
                ChildRB = PickedItem.GetComponent<Rigidbody>();

                ChildRB.useGravity = false;
            }
        }
    }
    void DropItem()
    {

        if (PickedItem != null && ChildRB != null)
        {
            //Child Rigigd Body
            ChildRB.useGravity = true;
            ChildRB = null;

            //PickedItem
            PickedItem.parent = null;
            PickedItem = null;
        }
    }

    public void HoldingItem()
    {
        if (PickedItem != null && ChildRB != null)
        {
            ChildRB.angularVelocity = Vector3.zero;
            ChildRB.velocity = Vector3.zero;

            if (!AIUtils.InFrontOfTarget(transform.forward, transform.position, PickedItem.position, 0.8f))
            {
                if ((m_Player.Face.transform.position - PickedItem.position).magnitude > 1.0f)
                {
                    DropItem();
                }
            }
        }

    }

	IEnumerator jump_cr()
	{
		float time = Mathf.Sqrt(JumpHeight)/JumpSpeed;
		float t = -Mathf.Sqrt(JumpHeight)/JumpSpeed;

		while (t<time) 
		{
			t+=Time.deltaTime;
			float yVel = (-t*t*JumpSpeed)+ JumpHeight;
			m_Movement.m_Velocity = new Vector3 (m_Movement.m_Velocity.x, - Mathf.Sign(t) * yVel, m_Movement.m_Velocity.z);
			yield return null;
		}

		m_JumpCR = null;
	}
}
