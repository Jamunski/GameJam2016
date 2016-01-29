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

public interface IInteractable
{
    void OnInteraction();
    string Name { get; }
    Sprite Icon { get; }
}
