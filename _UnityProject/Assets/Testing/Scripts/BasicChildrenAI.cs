using UnityEngine;
using System.Collections;

public class BasicChildrenAI : MonoBehaviour
{
    public enum ChildState { Free, Interacting, Chase, Search, Ritual }
    [SerializeField] ChildState m_ChildState;


    public IRitual Ritual;

    public Animator Animator;

    public Boredom m_Boredom;
    Timer m_Timer;
    Timer m_ResetTimer;

    public GameObject m_Cat;
    GameObject m_InteractionObject;

    NavMeshAgent m_Agent;

    TestManager Manager;

    LayerMask ChildrenMask;
    LayerMask DistractableMask;

    //Boredom Levels
    const int ATTACK_CAT_THRESHOLD = 75;
    const int STOP_ATTACK_CAT_THRESHOLD = 25;
    const int INTERACT_WITH_OTHER_KID_THRESHOLD = 40;
    const int INTERACT_WITH_HOUSEHOLD_OBEJCT = 50;

    //Movement Speeds
    const float MIN_RANGE_OF_NEUTRAL_SPEEDS = 1.0f;
    const float MAX_RANGE_OF_NEUTRAL_SPEEDS = 3.0f;

    const float CHASE_SPEED = 5.0f;

    //Search Statistics
    const float INTERACTION_RANGE = 2.5f;
    const float VISION_CONE = 0.2f;
    const float VISION_RANGE = 10.0f;

    const float TRAVEL_RANGE = 8.0f;


    // Use this for initialization
    void Start()
    {
        Ritual = GetComponent<IRitual>();

        m_Boredom = new Boredom();
        m_Timer = new Timer();
        m_ResetTimer = new Timer();

        m_Agent = GetComponent<NavMeshAgent>();

        ChildrenMask = LayerMask.GetMask("Child");
        DistractableMask = LayerMask.GetMask("Distractables");

        Manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<TestManager>();
        Manager.m_RitualManager.RegisterChild(this);

        InitState(ChildState.Free);
    }


    // Update is called once per frame
    void Update()
    {
        HandleState();

        //Debug.Log(m_Target.tag);
        //Debug.Log(AIUtils.CanSeeTarget(transform.position, m_Target.transform.position, 25.0f, m_Target.tag));
        //Debug.Log(AIUtils.InFrontOfTarget(transform.forward, transform.position, m_Target.transform.position, 0.5f));
        //transform.position += transform.forward * Time.deltaTime;
    }

    public ChildState GetChildState()
    {
        return m_ChildState;
    }

    public void InitState(ChildState aState)
    {
        m_ChildState = aState;
        //Debug.Log("Initiation " + m_ChildState);


        switch (m_ChildState)
        {
            case ChildState.Free:
                //Locate a Destination
                Vector3 target = Vector3.zero;
                int counter = 0;
                while (target == Vector3.zero)
                {
                    target = (AIUtils.ReturnRandomLocation(transform.position, TRAVEL_RANGE));
                    counter++;
                    if (counter > 5000)
                    {
                        Debug.LogError("Stuck in While Loop");
                        break;
                    }
                }

                m_Agent.SetDestination(target);

                //Randomize a Speed
                m_Agent.speed = Random.Range(MIN_RANGE_OF_NEUTRAL_SPEEDS, MAX_RANGE_OF_NEUTRAL_SPEEDS);
                break;

            case ChildState.Interacting:

                //Set Desitnation to be Distraction Object
                m_Agent.SetDestination(m_InteractionObject.transform.position);

                //Sit in a Position
                m_Agent.speed = 0;
                
                //Reset the Timer
                m_Timer.CancalTime();
                break;

            case ChildState.Chase:
                //Set Fast Speed
                m_Agent.speed = CHASE_SPEED;
                break;

            case ChildState.Search:
                m_ResetTimer.CancalTime();
                m_Agent.speed = MIN_RANGE_OF_NEUTRAL_SPEEDS;
                m_Agent.SetDestination(m_Cat.transform.position);
                break;

            case ChildState.Ritual:
                Ritual.Action(m_Agent);
                break;

            default:
                Debug.LogError("Unimplemented State");
                break;
        }
    }

    void HandleState()
    {
        switch (m_ChildState)
        {
            case ChildState.Free:
                //Handle Boredom Detection
                HandleBoredomDetection();

                //Apply Boredom Loss
                m_Boredom.DrainBoredom();

                //Detect Distance from Target
                if ((transform.position - m_Agent.destination).magnitude <= 1.0f)
                {
                    if (m_Timer.Delay(Random.Range(0.0f,3.0f), Time.deltaTime))
                    {
                        InitState(ChildState.Free);
                    }
                }

                break;

            case ChildState.Interacting:
                //Wait for Timer
                if (m_Timer.Delay(TimeSystem.SECONDS_PER_HOUR, Time.deltaTime))
                {
                    //Replenish Boredom
                    m_Boredom.ReplenishBoredom();
                    InitState(ChildState.Free);
                }
                break;

            case ChildState.Chase:
                //Set Destination
                m_Agent.SetDestination(m_Cat.transform.position);

                //HandleBoredom Detection
                HandleBoredomDetection();

                //Apply Boredom Loss
                m_Boredom.DrainBoredom();

                //Lost Sight of Cat
                if (!HasSightOfCat)
                {
                    InitState(ChildState.Search);
                }
                break;

            case ChildState.Search:

                HandleBoredomDetection();

                m_Boredom.DrainBoredom();

                if ((m_Agent.destination - transform.position).magnitude <= 0.5f)
                {
                    if (m_Timer.Delay(Random.Range(0.5f, 2.0f), Time.deltaTime))
                    {
                        m_Agent.SetDestination(AIUtils.ReturnRandomLocation(transform.position, 2.0f));
                    }
                }

                if (m_ResetTimer.Delay(Random.Range(5.0f, 8.0f), Time.deltaTime))
                {
                    InitState(ChildState.Free);
                }

                break;

            case ChildState.Ritual:
                if((transform.position - m_Agent.destination).magnitude < 1.0f)
                {
                    //Set animation
                    Animator.SetBool("ritualIsOn", true);

                    InitState(ChildState.Ritual);
                }

                //Wait for Timer
                if(Ritual.Ended)
                {
                //Replenish Boredom
                    m_Boredom.ReplenishBoredom();
                    Ritual.Condition();

                    //Set animation
                    Animator.SetBool("ritualIsOn",false);

                    InitState(ChildState.Free);
                }

                

                //If not InitState(Free)
                break;

            default:
                Debug.LogError("Unimplemented State");
                break;
        }
    }

    void HandleBoredomDetection()
    {
        //Check if the Cat is within the Thresholds
        if (m_Boredom.BoredomLevel <= ATTACK_CAT_THRESHOLD && m_Boredom.BoredomLevel >= STOP_ATTACK_CAT_THRESHOLD)
        {
            //Check if the Cat is Free, (Not Chasing, Not Interacting, Not Mid Ritual)
            if (m_ChildState == ChildState.Free || m_ChildState == ChildState.Search)
            {
                //Check if Cat is Infront of the AI
                if (AIUtils.InFrontOfTarget(transform.forward, transform.position, m_Cat.transform.position, VISION_CONE))
                {
                    //Check if the AI can see Cat
                    if (AIUtils.CanSeeTarget(transform.position, m_Cat.transform.position, VISION_RANGE, m_Cat))
                    {
                        InitState(ChildState.Chase);
                    }
                }
            }
        }

        //Check the Threshold for Interacting with other kids
        if (m_Boredom.BoredomLevel <= INTERACT_WITH_OTHER_KID_THRESHOLD)
        {
            //Allow the Child to Interact with another kid if he is Either Free or Chasing
            if (m_ChildState == ChildState.Free || m_ChildState == ChildState.Chase || m_ChildState == ChildState.Search)
            {
                //Find all Relative Objects within Interaction Distance
                RaycastHit[] hitInfos = Physics.SphereCastAll(transform.position, INTERACTION_RANGE, transform.forward, INTERACTION_RANGE, ChildrenMask);

                for (int i = 0; i < hitInfos.Length; i++)
                {
                    //Get a reference to the other child
                    BasicChildrenAI child = hitInfos[i].transform.GetComponent<BasicChildrenAI>();
                    if (child == null)
                    {
                        //If the other object is not a child, Move on to the next object
                        continue;
                    }

                    //Check if the other Child is also Bored
                    if (child.m_Boredom.BoredomLevel <= INTERACT_WITH_OTHER_KID_THRESHOLD)
                    {
                        //Check if the other Child is infront of this child
                        if (AIUtils.InFrontOfTarget(transform.forward, transform.position, hitInfos[i].transform.position, VISION_CONE))
                        {
                            Debug.Log(this + " Is interacting with " + m_InteractionObject);
                            //Set that Child to Interact with this child
                            child.SetInteractingWithChild(this);
                            //Set this Child to interact with that child
                            this.SetInteractingWithChild(child);
                            return;
                        }
                    }
                }
            }
        }

        //Check threshold on Interacting with household objects
        if (m_Boredom.BoredomLevel <= INTERACT_WITH_HOUSEHOLD_OBEJCT)
        {
            //Check cat is in Appropiate states
            if (m_ChildState == ChildState.Free || m_ChildState == ChildState.Chase || m_ChildState == ChildState.Search)
            {
                //Find all Relative Objects
                RaycastHit[] hitInfos = Physics.SphereCastAll(transform.position, INTERACTION_RANGE, transform.forward, INTERACTION_RANGE, DistractableMask);

                for (int i = 0; i < hitInfos.Length; i++)
                {
                    //Check if Object is Seen
                    if (AIUtils.InFrontOfTarget(transform.forward, transform.position, hitInfos[i].transform.position, VISION_CONE))
                    {
                        //Set the Interaction Object to the Household Object
                        m_InteractionObject = hitInfos[i].transform.gameObject;
                        Debug.Log(this + " Is interacting with " + m_InteractionObject);
                        InitState(ChildState.Interacting);
                        return;
                    }

                }
            }
        }
    }

    bool HasSightOfCat
    {
        get
        {
            if (AIUtils.InFrontOfTarget(transform.forward, transform.position, m_Cat.transform.position, VISION_CONE))
            {
                //Check if the AI can see Cat
                return (AIUtils.CanSeeTarget(transform.position, m_Cat.transform.position, VISION_RANGE, m_Cat));
            }
            return false;
        }
    }

    public void SetInteractingWithChild(BasicChildrenAI aInteractingObject)
    {
        m_InteractionObject = aInteractingObject.gameObject;
        InitState(ChildState.Interacting);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.25f);

        Gizmos.DrawSphere(transform.position, INTERACTION_RANGE);
    }
}

[System.Serializable]
public class Boredom : System.Object
{
    public int BoredomLevel = 100;

    Timer m_Timer = new Timer();

    const int MAX_BORDOM = 100;
    const float TIME_PER_DRAIN = 2.0f;
    const int AMOUNT_DRAINED = 1;


    public Boredom()
    {
        m_Timer = new Timer();
    }

    public void DrainBoredom()
    {
        if (m_Timer.Delay(TIME_PER_DRAIN, Time.deltaTime))
        {
            BoredomLevel -= AMOUNT_DRAINED;

            if (BoredomLevel < 0)
            {
                BoredomLevel = 0;
            }
        }
    }

    public void ReplenishBoredom()
    {
        BoredomLevel = MAX_BORDOM;
    }
}
