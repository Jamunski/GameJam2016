using UnityEngine;
using System.Collections;

public class CatMovement : MonoBehaviour
{
    public Camera m_Camera;
    Vector3 Velocity;
    Vector3 TurnVelocity;
    LayerMask GroundMask;
    LayerMask PickableObjects;

    Rigidbody rb;
    
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GroundMask = LayerMask.GetMask("Ground");
        PickableObjects = LayerMask.GetMask("Pickups");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            DropItem();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            HoldingItem();
        }

        Ray ray = new Ray(transform.position, -transform.up);
        //if (Physics.SphereCast(ray, 1.0f, 1.0f, GroundMask))
        {
            Velocity = Vector3.zero;
            TurnVelocity = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                Velocity += m_Camera.transform.forward;
            }
            if (Input.GetKey(KeyCode.D))
            {
                TurnVelocity = new Vector3(0, 100, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Velocity -= m_Camera.transform.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                TurnVelocity = new Vector3(0, -100, 0);
            }

            Velocity.y = 0;
            Velocity.Normalize();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Velocity *= 5.0f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 force;
                //force = transform.forward * 6;
                force = transform.up * 6;

                rb.AddForce(force, ForceMode.VelocityChange);
            }
        }
    }

    void FixedUpdate()
    {
        transform.position += Velocity * Time.fixedDeltaTime;
        transform.eulerAngles += TurnVelocity * Time.fixedDeltaTime;
    }


    public GameObject Face;
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
                PickedItem.position = Face.transform.position;
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

    void HoldingItem()
    {
        if (PickedItem != null && ChildRB != null)
        {
            ChildRB.angularVelocity = Vector3.zero;
            ChildRB.velocity = Vector3.zero;

            if(!AIUtils.InFrontOfTarget(transform.forward, transform.position, PickedItem.position, 0.8f))
            {
                if((Face.transform.position - PickedItem.position).magnitude > 1.0f)
                {
                    DropItem();
                }
            }
        }

    }

}
