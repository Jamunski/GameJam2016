/*********************************************HEADER*********************************************\
 * Created By: Jordan Vassilev
 * Last Updated on: 28/1/16
 * Function: 
\*********************************************HEADER*********************************************/
/*********************************************NOTES**********************************************\
 * 
\*********************************************NOTES**********************************************/
using UnityEngine;
using System.Collections;

public enum EffectType
{
    loss = 0,
    gain
}

public struct DynamicEffect
{
    public EffectType TypeOfEffect;
    public float Value;
}

/// <summary>
/// Dynamic objects are those which can have their values affected by actors. IE: player attacks a wall(Dynamic) and destroys it
/// </summary>
public interface IDynamic
{
    void CalculateEffect(GameObject aCause, DynamicEffect aEffect);
}