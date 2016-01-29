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

public class Door : MonoBehaviour, IActivatable, IInteractable
{
    private Vector3 m_OpenPos;
    private Vector3 m_ClosedPos;
    private Vector3 m_TargetPos;

    private IEnumerator Coroutine;

    public Sprite door;
    public string m_DoorName = "Door";
    public bool isLocked = false;


    void Start()
    {
        m_OpenPos = transform.position + new Vector3(0.0f, 7.5f, 0.0f);
        m_ClosedPos = transform.position;
        m_TargetPos = m_ClosedPos;
        Coroutine = move_cr(this.transform.position, m_TargetPos);
    }

    public void OnInteraction()
    {
        if (!isLocked)
        {
            OnActivate();
            return;
        }
        Debug.Log("Door Locked");
        //Shake the door...
    }

    public void OnActivate()
    {
        Debug.Log("Door Activated");
        if (m_TargetPos == m_OpenPos)
        {
            m_TargetPos = m_ClosedPos;
        }
        else if (m_TargetPos == m_ClosedPos)
        {
            m_TargetPos = m_OpenPos;
        }
        StopCoroutine(Coroutine);
        Coroutine = move_cr(this.transform.position, m_TargetPos);
        StartCoroutine(Coroutine);
    }

    public Sprite Icon
    {
        get { return door; }
    }

    public string Name
    {
        get { return m_DoorName; }
    }

    private IEnumerator move_cr(Vector3 start, Vector3 end)
    {
        float t = 0;
        float duration = Vector3.Distance(start, end) / 3;

        while (t < duration)
        {
            float val = t / duration;
            transform.position = Vector3.Lerp(start, end, val);

            t += Time.deltaTime;

            yield return null;
        }

        transform.position = end;
    }
}
