using UnityEngine;
using System.Collections;

public class AIUtils
{
    /// <summary>
    /// Check to see if the Player is Infront of the AI
    /// </summary>
    /// <param name="aForward">AI's Forward</param>
    /// <param name="aPosition">AI's Position</param>
    /// <param name="aTarget">AI's Target</param>
    /// <param name="aDotThreshold">AI's Cone Range</param>
    /// <returns>Is the Target Infront of the AI</returns>
    public static bool InFrontOfTarget(Vector3 aForward, Vector3 aPosition, Vector3 aTarget, float aDotThreshold)
    {
        Vector3 vectorToTarget = aTarget - aPosition;

        return (Vector3.Dot(aForward.normalized, vectorToTarget.normalized) > aDotThreshold);
    }

    /// <summary>
    /// Checks to see if the AI can see a Target
    /// By Checking Distance, as well as objects Between
    /// </summary>
    /// <param name="aPosition">AI's Position</param>
    /// <param name="aTargetPosition">AI's Target Position</param>
    /// <param name="aSearchDistance">AI's Sight Range</param>
    /// <param name="aTargetsTag">Target's Tag</param>
    /// <returns>Can AI see the Target</returns>
    public static bool CanSeeTarget(Vector3 aPosition, Vector3 aTargetPosition, float aSearchDistance, string aTargetsTag)
    {
        Vector3 vectorToTarget = aTargetPosition - aPosition;
        RaycastHit hitinfo;

        if (Physics.Raycast(aPosition, vectorToTarget, out hitinfo, aSearchDistance))
        {
            return (hitinfo.transform.gameObject.tag == aTargetsTag);
        }
        return false;
    }

    /// <summary>
    /// Checks to see if the AI can see a Target
    /// By Checking Distance, as well as objects Between
    /// </summary>
    /// <param name="aPosition">AI's Position</param>
    /// <param name="aTargetPosition">AI's Target Position</param>
    /// <param name="aSearchDistance">AI's Sight Range</param>
    /// <param name="aTargetsGameObject">Target's GameObject</param>
    /// <returns>Can AI see the Target</returns>
    public static bool CanSeeTarget(Vector3 aPosition, Vector3 aTargetPosition, float aSearchDistance, GameObject aTargetsGameObject)
    {
        Vector3 vectorToTarget = aTargetPosition - aPosition;
        RaycastHit hitinfo;

        if (Physics.Raycast(aPosition, vectorToTarget, out hitinfo, aSearchDistance))
        {
            return (hitinfo.transform.gameObject == aTargetsGameObject);
        }
        return false;
    }

    public static bool ReturnRandomLocation(Vector3 aPosition, float aRange, out Vector3 aResult)
    {
        Vector3 randomPoint = aPosition + Random.insideUnitSphere * aRange;

        NavMeshHit hitInfo;
        if (NavMesh.SamplePosition(randomPoint, out hitInfo, 1.0f, NavMesh.AllAreas))
        {
            aResult = hitInfo.position;
            return true;
        }

        aResult = Vector3.zero;
        return false;
    }

    public static Vector3 ReturnRandomLocation(Vector3 aPosition, float aRange)
    {
        Vector3 randomPoint = aPosition + Random.insideUnitSphere * aRange;

        Debug.DrawRay(randomPoint, Vector3.up * 3.0f, Color.red, 5.0f);

        NavMeshHit hitInfo;
        if (NavMesh.SamplePosition(randomPoint, out hitInfo, 1.0f, NavMesh.AllAreas))
        {

            return hitInfo.position;
        }

         
        return Vector3.zero;
    }
}
