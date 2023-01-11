using UnityEngine;
using Entity;
using FiniteStateMachine;

public class EntityEnemy_Melee : Entity_Enemy
{
    public StateContainer_EnemyMelee StateContainer { get; protected set; }
    public MasterOfState<EntityEnemy_Melee> MasterOfState { get; protected set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        StateContainer = new StateContainer_EnemyMelee(this);
    }

    protected override void OnStart()
    {
        base.OnStart();
        Entity_Player.Instance.OnStartEvent.AddListener(EngageCombatState);
        Animator = GetComponent<Animator>();
    }

    protected override void OnCollisionEnterAct(Collision2D collision)
    {
    }

    protected override void OnCollisionExitAct(Collision2D collision)
    {

    }

    protected override void OnTriggerEnterAct(Collider2D collision)
    {
        base.OnTriggerEnterAct(collision);

        Entity_Train hit = collision.GetComponent<Entity_Train>();
        if (hit && !IsDead)
        {
            Animator.SetTrigger("Attack");
        }
    }

    protected override void OnTriggerExitAct(Collider2D collision)
    {
        Entity_Train hit = collision.GetComponent<Entity_Train>();
        if (hit && !IsDead)
        {
            Animator.SetTrigger("Attack");
        }
    }

    protected override void OnFixedUpdate()
    {
        MasterOfState.OnUpdate();
    }

    protected override void Init()
    {
        base.Init();
        MasterOfState = new MasterOfState<EntityEnemy_Melee>(StateContainer.IddleState);
        Entity_Player.Instance.OnStartEvent.AddListener(EngageCombatState);
    }

    public override void EngageCombatState()
    {
        MasterOfState.OnTransitionState(StateContainer.CombatState);
    }

    public void AE_ActivateAttackTrigger()
    {
        attackBox.SetActive(!attackBox.activeSelf);
    }
}
