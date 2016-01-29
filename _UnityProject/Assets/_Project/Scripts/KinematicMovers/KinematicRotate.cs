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

public class KinematicRotate : MonoBehaviour
{
    public Vector3 RotationValuePerSecond = new Vector3();
    IEnumerator coroutine;

    void Start()
    {
        coroutine = Rotate_cr();

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

    IEnumerator Rotate_cr()
    {
        while (true)
        {
            transform.Rotate(RotationValuePerSecond * Time.deltaTime);

            yield return null;
        }
    }
}
