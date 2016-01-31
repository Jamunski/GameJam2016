/*********************************************HEADER*********************************************\
 * Created By: Jordan Vassilev
 * Last Updated on: 13/12/15
 * Function: 
\*********************************************HEADER*********************************************/
/*********************************************NOTES**********************************************\
 * 
\*********************************************NOTES**********************************************/
using UnityEngine;
using System.Collections;

//Player visible pools which values can fluctuate and need to be calculated... Calculated based on the Player Attributes...
public struct Pools
{
	public float Health;
}

public struct Stats
{
	public float Speed;
}

public class ActorStatistics
{
	//REFERENCES
	private Actor m_Actor;

	//POOL STARTS
	private const float HEALTH_MAX = 100.0f;

	//MODIFICATION CONSTANTS
	private const float SPRINT_MODIFIER = 1.5f;

	//NON CONSTANT MODIFIERS
	private float m_SpeedModifier; // Based off the Load and Burden effects...

	public Pools m_Pools = new Pools();
	public Stats m_Stats = new Stats();

	//Don't know really
	public bool m_IsSprinting;
	private float m_SprintSpeed;
	public float m_FinalSpeed { get; private set; }

	public ActorStatistics(Actor aActor)
	{
		m_Actor = aActor;
		InitializeStats();
		InitializePools();
	}

	//private Methods

	private void InitializeStats()
	{
		m_Stats.Speed = 5.0f;
	}

	private void InitializePools()
	{
		m_Pools.Health = HEALTH_MAX;
	}

	//public Methods
	//Statistic Calculation Functions
	public void CalculateSpeed()
	{
		//NOT SURE YET... Needs more work...
		m_SprintSpeed = m_Stats.Speed * SPRINT_MODIFIER;
		m_FinalSpeed = m_Stats.Speed;
		if (m_IsSprinting)
		{
			m_FinalSpeed = m_SprintSpeed;
		}
	}

	//Should be changed to calculate life, this function would then be used for all cases in which health pools are changed
	public void CalculateHealth(GameObject aCause, float aValue, EffectType aHealthEffect = EffectType.loss) //GameObject dealing damage, Damage, DamageType
	{
		try
		{
			switch (aHealthEffect)
			{
				case EffectType.loss:
					if (m_Pools.Health > 0)
					{
						m_Pools.Health -= aValue;
						m_Actor.m_HUD.UpdatePools();
					}
					break;
				case EffectType.gain:
					m_Pools.Health += aValue;
					m_Actor.m_HUD.UpdatePools();
					break;
			}

			if (m_Pools.Health > 0)
			{
				return;
			}

			m_Pools.Health = 0;
			m_Actor.m_HUD.DisplayGameOver();
		}
		catch
		{
			Debug.Log(m_Actor.name + " Statistics HUD reference is NULL");
		}
	}
}