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

public class KinematicTranslate : MonoBehaviour
{
    public float m_CycleSpeed;
    private Vector3 m_TranslateStart;
    public Vector3 m_TranslateEnd = new Vector3();
    public float m_CyclePercent; // Get the difference between start and end pos, multiply by % and + start pos

    IEnumerator coroutine;

    void Start()
    {
        //TEMP PLEASE KILL ME
        //converting to world coords...
        m_TranslateStart = transform.position;
        m_TranslateEnd = m_TranslateStart + m_TranslateEnd;
        // how much current value goes into max value.

        coroutine = Translate_cr();

        Activate();
    }

    public void Activate()
    {
        StartCoroutine(coroutine);
    }

    public void DeActivate()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator Translate_cr()
    {
        float percent;
        while (true)
        {
            percent = (Mathf.Sin(Time.time * (m_CycleSpeed * Mathf.PI) + (Mathf.PI * 2 * m_CyclePercent)) + 1.0f) / 2.0f;

            transform.position = Vector3.Lerp(m_TranslateStart, m_TranslateEnd, percent);

            yield return null;
        }
    }
}
