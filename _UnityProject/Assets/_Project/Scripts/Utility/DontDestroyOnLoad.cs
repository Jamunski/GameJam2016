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

public class DontDestroyOnLoad : MonoBehaviour
{
    private string m_LastLevelLoaded;

    private GameObject m_DontDestroy;

    void Start()
    {
        m_LastLevelLoaded = Application.loadedLevelName;
        m_DontDestroy = gameObject;
        Transform.DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (m_LastLevelLoaded != Application.loadedLevelName)
        {
            if (m_DontDestroy.transform.childCount > 0)
            {
                ReleaseChildren();
            }
            m_LastLevelLoaded = Application.loadedLevelName;
        }
    }

    private void ReleaseChildren()
    {
        Debug.Log(gameObject.name + ": ReleasingChildren");
        int childCount = m_DontDestroy.transform.childCount;
        GameObject[] childArray = new GameObject[childCount];
        
        //set the children of m_Temp in the childArray
        for (int i = childCount -1; i != -1; i--)
        {
            childArray[i] =  m_DontDestroy.transform.GetChild(i).gameObject;
            SetParent(m_DontDestroy.transform.GetChild(i).gameObject);
            childCount--;
        }

        if(childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetParent(GameObject child)
    {
        if (!child.GetComponent<MonoBehaviourSubject>() || child.GetComponent<MonoBehaviourSubject>().m_ParentName == "")
        {
            Debug.Log("SetParent");
            GameObject NoParent = GameObject.Find("NoParentDefined");
            //Get the appropriate parent for the GameObject, if no parent exists their parent will be set to "NoParent"
            if (GameObject.Find("NoParentDefined") == null)
            {
                NoParent = new GameObject();
                NoParent.name = "NoParentDefined";
            }
            child.transform.SetParent(NoParent.transform);
        }
        else
        {
            GameObject Parent;
            if (GameObject.Find(child.GetComponent<MonoBehaviourSubject>().m_ParentName) == null)
            {
                Parent = new GameObject();
                Parent.name = child.GetComponent<MonoBehaviourSubject>().m_ParentName;
            }
            Parent = GameObject.Find(child.GetComponent<MonoBehaviourSubject>().m_ParentName);
            child.transform.SetParent(Parent.transform);
        }
    }
}
