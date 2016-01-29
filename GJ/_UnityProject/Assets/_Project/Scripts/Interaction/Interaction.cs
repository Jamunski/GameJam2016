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
using System.Collections.Generic;

public class Interaction : MonoBehaviour
{
    public IInteractable current { get; private set; }
    public float InteractionRadius;
    public Vector3 InteractionCenter = new Vector3();
    private Actor m_Actor;

    //Unity Callbacks
    void Start()
    {
        m_Actor = GetComponentInParent<Actor>();
    }

    void Update()
    {
        InteractCheck(InteractionCenter + transform.position, InteractionRadius);
    }

    void OnDrawGizmos()
    {
        Color col = new Color();
        col = Color.cyan;
        col.a = 0.2f;
        Gizmos.color = col;
        Gizmos.DrawSphere(InteractionCenter + transform.position, InteractionRadius);
    }

    //private Methods
    private void InteractCheck(Vector3 center, float radius)
    {
        float minDist = float.MaxValue;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        List<Collider> interactables = new List<Collider>();
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<IInteractable>() != null)
            {
                interactables.Add(hitColliders[i]);
            }
        }
        int n = 0;
        while (n < interactables.Count)
        {
            //compare distance between player and interactable objects
            //if current dist is smaller than min dist then store that object in ObjectToInteractWith and set minDist to that value
            if (minDist > Vector3.Distance(interactables[n].transform.position, m_Actor.transform.position))
            {
                minDist = Vector3.Distance(interactables[n].transform.position, m_Actor.transform.position);
                current = interactables[n].GetComponent<IInteractable>();
                //Debug.Log("Closest Interactable: " + interactables[n]);
            }
            n++;
        }
        if(interactables.Count < 1)
        {
            current = null;
        }
    }

    //public Methods
    public void Interact()
    {
        if (current != null)
        {
            Debug.Log("Interact successful");
            current.OnInteraction();
        }
        else
        {
            Debug.Log("Interact Failed");
            current = null;
        }
    }
}