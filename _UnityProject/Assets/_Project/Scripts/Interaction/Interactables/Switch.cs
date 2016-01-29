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

public class Switch : MonoBehaviour, IInteractable
{
    public GameObject[] target;
    public Sprite icon;
    public string m_SwitchName = "Switch";

    void Start()
    {
        this.GetComponent<Renderer>().material.color = Color.red;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (target != null)
        {
            for (int i = 0; i < target.Length; i++)
            {
                Gizmos.DrawLine(transform.position, target[i].transform.position);
                Gizmos.DrawIcon(transform.position, icon.name);
            }
        }
    }

    public void OnInteraction()
    {
        for (int i = 0; i < target.Length; i++)
        {
            target[i].GetComponent<IActivatable>().OnActivate();
            Debug.Log("Switch Interacted");
            if (this.GetComponent<Renderer>().material.color != Color.green)
            {
                this.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                this.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    public string Name
    {
        get { return m_SwitchName; }
    }

    public Sprite Icon
    {
        get { return icon; }
    }
}