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

    LayerMask PickableObjects;


    void Start()
    {
        m_Stats = GetComponent<Actor>().m_Statistics;
        m_Player = gameObject.GetComponent<Actor>();
        PickableObjects = LayerMask.GetMask("PickUps");
    }

    public void Jump()
    {
        Debug.Log(gameObject.name + ": Jump");
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
}
