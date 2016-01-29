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

public class HUD : MonoBehaviour
{
	// private Member Variables
	private GameObject HealthBar;
	private GameObject HealthPool;

	private const float MAX_BAR_LENGTH = 200;
	private const float HEALTH_MAX = 100.0f;

	// public Member Variables
	public Actor m_Actor;

	// Unity Callbacks
	void Awake()
	{
		HealthBar = GameObject.Find(gameObject.name + "/TopLeft/HealthBar");

		HealthPool = GameObject.Find(gameObject.name + "/TopLeft/HealthBar/HealthPool");
	}

	public void InitializeBars()
	{
		Debug.Log(HealthBar);
		HealthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(HEALTH_MAX / MAX_BAR_LENGTH * 540, 0);
	}

	// public Methods
	public void UpdateBars()
	{
		HealthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(HEALTH_MAX / MAX_BAR_LENGTH * 540, 0);
	}

	public void UpdatePools()
	{
		HealthPool.transform.localScale = new Vector3(m_Actor.m_Statistics.m_Pools.Health / HEALTH_MAX, 1.0f, 0.0f);
	}

	public void DisplayVictory()
	{
	}

	public void DisplayGameOver()
	{
	}
}