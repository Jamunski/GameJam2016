using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour
{
    TestManager m_Manager;

    // Use this for initialization
    void Start()
    {
        m_Manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<TestManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, m_Manager.m_TimeSystem.TimeOfDay);
    }
}
