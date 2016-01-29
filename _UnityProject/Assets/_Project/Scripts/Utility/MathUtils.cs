using UnityEngine;
using System;

public class MathUtils
{
    public static float CompareEpsilon = 0.00001f;

    //Eases from the start to the end.  This is meant to be called over many frames.  The
    //values will change fast at first and gradually slow down.
    public static float LerpTo(float easeSpeed, float start, float end, float dt)
    {
        float diff = end - start;

        diff *= Mathf.Clamp(dt * easeSpeed, 0.0f, 1.0f);

        return diff + start;
    }

    //Eases from the start to the end.  This is meant to be called over many frames.  The
    //values will change fast at first and gradually slow down.
    public static Vector3 LerpTo(float easeSpeed, Vector3 start, Vector3 end, float dt)
    {
        Vector3 diff = end - start;

        diff *= Mathf.Clamp(dt * easeSpeed, 0.0f, 1.0f);

        return diff + start;
    }

    //Eases from the start to the end.  This is meant to be called over many frames.  The
    //values will change fast at first and gradually slow down.
    public static Vector3 SlerpTo(float easeSpeed, Vector3 start, Vector3 end, float dt)
    {
        float percent = Mathf.Clamp(dt * easeSpeed, 0.0f, 1.0f);

        return Vector3.Slerp(start, end, percent);
    }

    //Eases from the start to the end.  This is meant to be called over many frames.  The
    //values will change fast at first and gradually slow down.
    public static Vector3 SlerpTo(float easeSpeed, Vector3 start, Vector3 end, Vector3 slerpCenter, float dt)
    {
        Vector3 startOffset = start - slerpCenter;
        Vector3 endOffset = end - slerpCenter;

        float percent = Mathf.Clamp(dt * easeSpeed, 0.0f, 1.0f);

        return Vector3.Slerp(startOffset, endOffset, percent) + slerpCenter;
    }

    //Eases from the start to the end.  This is meant to be called over many frames.  The
    //values will change fast at first and gradually slow down.
    public static Quaternion LerpTo(float easeSpeed, Quaternion start, Quaternion end, float dt)
    {
        float percent = Mathf.Clamp(dt * easeSpeed, 0.0f, 1.0f);

        return Quaternion.Slerp(start, end, percent);
    }

    //Calculates the rotation angle in degrees of a 2D vector
    public static float CalcRotationDegs(float x, float y)
    {
        return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
    }

    //Use this to compare floating point numbers, when you want to allow for a small degree of error
    public static bool AlmostEquals(float v1, float v2, float epsilon)
    {
        return Mathf.Abs(v2 - v1) <= epsilon;
    }

    //Use this to compare floating point numbers, when you want to allow for a small degree of error
    public static bool AlmostEquals(float v1, float v2)
    {
        return AlmostEquals(v1, v2, CompareEpsilon);
    }

    //Clamps a vector along the x-z plane
    public static Vector3 HorizontalClamp(Vector3 v, float maxLength)
    {
        float horizLengthSqrd = v.x * v.x + v.z * v.z;

        if (horizLengthSqrd <= maxLength * maxLength)
        {
            return v;
        }

        float horizLength = Mathf.Sqrt(horizLengthSqrd);

        v.x *= maxLength / horizLength;
        v.z *= maxLength / horizLength;

        return v;
    }

    public static Vector3 HorizontalClamp(Vector3 v, float minLength, float maxLength)
    {
        float horizLengthSqrd = v.x * v.x + v.z * v.z;

        if (horizLengthSqrd < minLength * minLength)
        {
            if (horizLengthSqrd > 0.0f)
            {
                float horizLength = Mathf.Sqrt(horizLengthSqrd);

                v.x *= minLength / horizLength;
                v.z *= minLength / horizLength;
            }
            else
            {
                //The direction of the vector is undefined in this case.  Choosing the z axis and using 
                //that.  
                v = Vector3.forward * minLength;
            }
        }
        else if (horizLengthSqrd > maxLength * maxLength)
        {
            float horizLength = Mathf.Sqrt(horizLengthSqrd);

            v.x *= maxLength / horizLength;
            v.z *= maxLength / horizLength;
        }

        return v;
    }

    //Clamps a vector between two values
    public static Vector3 Clamp(Vector3 v, float minLength, float maxLength)
    {
        float lengthSqrd = v.sqrMagnitude;

        if (lengthSqrd < minLength * minLength)
        {
            if (lengthSqrd > 0.0f)
            {
                float length = Mathf.Sqrt(lengthSqrd);

                v *= minLength / length;
            }
            else
            {
                //The direction of the vector is undefined in this case.  Choosing the z axis and using 
                //that.  
                v = Vector3.forward * minLength;
            }
        }
        else if (lengthSqrd > maxLength * maxLength)
        {
            float length = Mathf.Sqrt(lengthSqrd);

            v *= maxLength / length;
        }

        return v;
    }

    //This function will project a point inside a capsule to the bottom of the capsule. 
    //The capsule is assumed to be oriented along the y-axis.
    public static Vector3 ProjectToBottomOfCapsule(
        Vector3 ptToProject,
        Vector3 capsuleCenter,
        float capsuleHeight,
        float capsuleRadius
        )
    {
        //Calculating the length of the line segment part of the capsule
        float lineSegmentLength = capsuleHeight - 2.0f * capsuleRadius;

        //Clamp line segment length
        lineSegmentLength = Math.Max(lineSegmentLength, 0.0f);

        //Calculate the line segment that goes along the capsules "Height"
        Vector3 bottomLineSegPt = capsuleCenter;
        bottomLineSegPt.y -= lineSegmentLength * 0.5f;

        //Get displacement from bottom of line segment
        Vector3 ptDisplacement = ptToProject - bottomLineSegPt;

        //Calculate needed distances
        float horizDistSqrd = ptDisplacement.x * ptDisplacement.x + ptDisplacement.z * ptDisplacement.z;

        float radiusSqrd = capsuleRadius * capsuleRadius;

        //The answer will be undefined if the pt is horizontally outside of the capsule
        if (horizDistSqrd > radiusSqrd)
        {
            return ptToProject;
        }

        //Calc projected pt
        float heightFromSegPt = -Mathf.Sqrt(radiusSqrd - horizDistSqrd);

        Vector3 projectedPt = ptToProject;
        projectedPt.y = bottomLineSegPt.y + heightFromSegPt;

        return projectedPt;
    }


    public static Vector3 ClampToCylinder(
            Vector3 position,
            Vector3 cylinderCenter,
            float cylinderRadius,
            float cylinderHeight
            )
    {
        //Calc offset from center
        Vector3 horizOffset = position - cylinderCenter;

        //Horizontal Clamping
        horizOffset = HorizontalClamp(horizOffset, cylinderRadius);

        //VerticalClamping
        float halfHeight = cylinderHeight * 0.5f;

        horizOffset.y = Mathf.Clamp(horizOffset.y, -halfHeight, halfHeight);

        //Update position
        position = horizOffset + cylinderCenter;

        return position;
    }

    public static Vector3 ClampToCylinder(
        Vector3 position,
        Vector3 cylinderCenter,
        float cylinderMinRadius,
        float cylinderMaxRadius,
        float cylinderHeight
        )
    {
        //Calc offset from center
        Vector3 horizOffset = position - cylinderCenter;

        //Horizontal Clamping
        horizOffset = HorizontalClamp(horizOffset, cylinderMinRadius, cylinderMaxRadius);

        //VerticalClamping
        float halfHeight = cylinderHeight * 0.5f;

        horizOffset.y = Mathf.Clamp(horizOffset.y, -halfHeight, halfHeight);

        //Update position
        position = horizOffset + cylinderCenter;

        return position;
    }

    public static Vector2 RandomUnitVector2()
    {
        float angleRadians = UnityEngine.Random.Range(0.0f, 2.0f * Mathf.PI);

        Vector2 unitVector = new Vector2(
            Mathf.Cos(angleRadians),
            Mathf.Sin(angleRadians)
            );

        return unitVector;
    }
}
