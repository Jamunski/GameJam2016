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

public class CheckKeyString : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log(Input.inputString);
        }
    }
}
