using UnityEngine;
using System.Collections;

public class MouseToMove : MonoBehaviour
{
    NavMeshAgent agent;
    public Camera cameras;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cameras.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }

}
