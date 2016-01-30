using UnityEngine;
using System.Collections;

public class TriggerBox : MonoBehaviour
{
    public bool KeyObjectIsIn;
    public GameObject KeyObject;

    void OnTriggerEnter(Collider aCollider)
    {
        if(aCollider.gameObject == KeyObject)
        {
            KeyObjectIsIn = true;
        }
    }

    void OnTriggerExit(Collider aCollider)
    {
        if (aCollider.gameObject == KeyObject)
        {
            KeyObjectIsIn = false;
        }
    }
}
