/*********************************************HEADER*********************************************\
 * Created By: Jordan Vassilev
 * Last Updated on: 06/12/15
 * Function: Observer class which will observe and set the state of the game. 
 * If m_State is set to Saving, the game manager will observe this and make the appropriate
 * function calls to save the game
\*********************************************HEADER*********************************************/
/*********************************************NOTES**********************************************\
 * 
\*********************************************NOTES**********************************************/
using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public enum GameEvent
{
    Menu,
    CharacterSelecting,
    Gameplay,
    Pausing,
    GameOver,
    Victory,
    Saving,
    Loaded,
    EndingScene,
    ReloadingScene,
    LoadingScene
}

public class GameManager : Observer
{
    public enum GameState
    {
        Menu = 0, // Change controls so appropriate for MainMenu
        CharacterSelect, // Set each player to control specific parts of the interface
        Gameplay, // Ensure there are players in the scene and set controls for them
        BossBattle, // Perhaps change the camera and some other settings
        Puzzle, // Perhaps change the camera and some other settings
        Paused, // Stop all game objects from updating, pause all events and change to PauseMenu controls.
        GameOver,
        LevelComplete,
        Saved,
        Loaded,
        SceneEnded, // Preparing to move to another scene, anything queued to not be destroyed on load will then be set as a child of DontDestroyOnLoad/Temp
        SceneLoaded // Starting a new scene, any children of DontDestroyOnLoad are released into their appropriate Folders
    }

    // private members
    private static GameManager instance;

    public string m_SceneToLoad { private get; set; }
    public GameState m_State { get; private set; }
    public GameState m_CurrentState { get; private set; }

    private GameManager()
    {
        m_State = new GameState();
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null) { instance = new GameManager(); }
            return instance;
        }
    }

    public void DontDestroy(GameObject obj)
    {
        GameObject DontDestroyOnLoad = GameObject.Find("DontDestroyOnLoad");
        if (DontDestroyOnLoad)
        {
            obj.transform.SetParent(DontDestroyOnLoad.transform);
        }
        else
        {
            GameObject.Instantiate(Resources.Load("DontDestroyOnLoad"));
            DontDestroyOnLoad = GameObject.FindObjectOfType<DontDestroyOnLoad>().gameObject;
            DontDestroyOnLoad.name = "DontDestroyOnLoad";
            obj.transform.SetParent(DontDestroyOnLoad.transform);
        }
    }

    public bool SaveGame() //Pass the location to save the game
    {
        return true;
    }

    public bool LoadGame() //Pass the location to load the game from
    {
        return true;
    }

    public void EndScene()
    {
        //Prepare this scene to be destroyed and ensure that the next scene will receive everything it needs from this one.
    }

    public void LoadScene() //Pass the next scene to move on to
    {
        Application.LoadLevel(m_SceneToLoad);
    }

    public void ReloadScene() //ReLoad the scene you are currently in
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    private void DefaultSceneLoad()
    {
        Application.LoadLevel("MainMenu");
    }

    private void PauseGame()
    {
        if(m_State == GameState.Paused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public override void OnNotify(ref GameObject aEntity, GameEvent aEvent)
    {
        TransitionStates(ref aEntity, aEvent);
    }

    private void TransitionStates(ref GameObject aEntity, GameEvent aEvent)
    {
        switch (aEvent)
        {
            case GameEvent.Menu:
                {
                    InputManager.Instance.m_State = InputState.Menu;
                    m_State = GameState.Menu;
                    Debug.Log("StateChangedTo: MainMenu");
                }
                break;
            case GameEvent.CharacterSelecting:
                {
                    InputManager.Instance.m_State = InputState.CharacterSelect;
                    m_State = GameState.CharacterSelect;
                    Debug.Log("StateChangedTo: CharacterSelect");
                }
                break;
            case GameEvent.Pausing:
                {
                    InputManager.Instance.m_State = InputState.Menu;
                    //PauseGame();
                    m_State = GameState.Paused;
                    Debug.Log("StateChangedTo: Paused");
                }
                break;
            case GameEvent.Gameplay:
                {
                    InputManager.Instance.m_State = InputState.Gameplay;
                    m_State = GameState.Gameplay;
                    Debug.Log("StateChangedTo: Gameplay");
                }
                break;
            case GameEvent.GameOver:
                {
                    m_State = GameState.GameOver;
                    Debug.Log("StateChangedTo: GameOver");
                }
                break;
            case GameEvent.Victory:
                {
                    m_State = GameState.LevelComplete;
                    Debug.Log("StateChangedTo: LevelComplete");
                }
                break;
            case GameEvent.Saving:
                {
                    if (SaveGame())
                    {
                        m_State = GameState.Saved;
                        Debug.Log("StateChangedTo: Saved");
                    }
                    else
                    {
                        Debug.Log("Failed Saving!!");
                    }
                }
                break;
            case GameEvent.Loaded:
                {
                    if (LoadGame())
                    {
                        m_State = GameState.Loaded;
                        Debug.Log("StateChangedTo: Loaded");
                    }
                    else
                    {
                        Debug.Log("Failed Loading!!");
                    }
                }
                break;
            case GameEvent.EndingScene:
                {
                    EndScene();
                    m_State = GameState.SceneEnded;
                    Debug.Log("StateChangedTo: SceneEnded");
                }
                break;
            case GameEvent.ReloadingScene:
                {
                    ReloadScene();
                    m_State = GameState.SceneLoaded;
                    Debug.Log("StateChangedTo: SceneLoaded");

                    if (Application.loadedLevelName != "MainMenu")
                    {
                        TransitionStates(ref aEntity, GameEvent.Gameplay);
                    }
                    else
                    {
                        TransitionStates(ref aEntity, GameEvent.Menu);
                    }
                }
                break;
            case GameEvent.LoadingScene:
                {
                    LoadScene();
                    m_State = GameState.SceneLoaded;
                    Debug.Log("StateChangedTo: SceneLoaded");

                    if (Application.loadedLevelName != "MainMenu")
                    {
                        TransitionStates(ref aEntity, GameEvent.Gameplay);
                    }
                    else
                    {
                        TransitionStates(ref aEntity, GameEvent.Menu);
                    }
                }
                break;
            default:
                {
                    Debug.Log("Unhandled Event!!!!");
                }
                break;
        }

        m_CurrentState = m_State;
    }
}

/* NOTES:
needs to have a default state which will load anything a scene will need to function properly
Only class which does changing states


*/