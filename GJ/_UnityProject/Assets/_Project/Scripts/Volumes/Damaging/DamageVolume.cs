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

public class DamageVolume : MonoBehaviour
{
    public float TriggerDamagePerSecond;

    void OnTriggerStay(Collider other)
    {
        //Get the players colliding with the trigger...
        if (other.GetComponentInParent<Actor>())
        {
            other.GetComponentInParent<Actor>().m_Statistics.CalculateHealth(gameObject, TriggerDamagePerSecond * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //Get the players colliding with the trigger...
        if (other.gameObject.GetComponentInParent<Actor>())
        {
            other.gameObject.GetComponentInParent<Actor>().m_Statistics.CalculateHealth(gameObject, TriggerDamagePerSecond * Time.deltaTime);
        }
    }
}
