using UnityEngine;
using Entity;
using FiniteStateMachine;
using UnityEngine.Events;

public class Entity_Player : Entity_Origin
{
    public static Entity_Player Instance { get; set; }

    [SerializeField] private GameObject wagonPrefab;

    [SerializeField] private PlayerInputReference m_inputs;

    private MasterOfState<Entity_Player> m_masterOfState;
    [SerializeField] private StateContainer_Player m_stateContainer;

    public UnityEvent OnStartEvent;

    public bool IsDead => Locomotive.IsDead;
    public GameObject WagonPrefab { get => wagonPrefab; }
    public Entity_Locomotive Locomotive { get; set; }
    public Entity_Wagon last;
    public PlayerInputReference Inputs { get => m_inputs; }
    public MasterOfState<Entity_Player> MasterOfState { get => m_masterOfState; }
    public StateContainer_Player StateContainer { get => m_stateContainer; }

    protected override void OnAwake()
    {
        Init();
        m_stateContainer = new StateContainer_Player(this);
        m_masterOfState = new MasterOfState<Entity_Player>(m_stateContainer.IddleState);
        Locomotive = GetComponentInChildren<Entity_Locomotive>();
    }

    protected override void OnStart()
    {
        last.GetComponent<BoxCollider2D>().enabled = false;
    }

    protected override void OnFixedUpdate()
    {
        if (m_masterOfState.CurrentState is State_PlayerCombat)
        {
            Locomotive.OnCombatHandleWagons();
        }
    }

    protected override void OnUpdate()
    {
        m_masterOfState.OnUpdate();
    }

    protected override void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
