using UnityEngine;
using System.Collections;

public class TriggerBox : MonoBehaviour
{
    public bool KeyObjectIsIn;
    public GameObject KeyObject;

    void OnTriggerEnter(Collider aCollider)
    {
        KeyObjectIsIn = (aCollider.gameObject == KeyObject);
    }
}
