using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour
{
    enum GameState { Awake, Sleep}
    GameState m_GameState;

    public TimeSystem m_TimeSystem;
    public RitualManager m_RitualManager;

    // Use this for initialization
    void Start()
    {
        m_TimeSystem = new TimeSystem();
        m_RitualManager = new RitualManager();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_GameState)
        {
            case GameState.Awake:
                m_RitualManager.UpdateRituals();

                if(m_TimeSystem.UpdateTime())
                {
                    m_RitualManager.EndDay();
                }
                //Debug.Log(m_TimeSystem.TimeOfDayToHours() + " Time = " + m_TimeSystem.m_TimeOfDay);
                break;
            case GameState.Sleep:

                break;
            default:
                break;
        }
    }
}
