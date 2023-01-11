using FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateContainer_EnemyMelee : StateContainer
{
    [SerializeField] private State_EnemyMeleeIdle m_iddleState;
    [SerializeField] private State_EnemyMeleeCombat m_combatState;
    [SerializeField] private State_EnemyMeleeDeath m_deathState;

    public State_EnemyMeleeIdle IddleState { get => m_iddleState; }
    public State_EnemyMeleeCombat CombatState { get => m_combatState; }
    public State_EnemyMeleeDeath DeathState { get => m_deathState; }

    public StateContainer_EnemyMelee(EntityEnemy_Melee context)
    {
        m_iddleState = new State_EnemyMeleeIdle(context);
        m_combatState = new State_EnemyMeleeCombat(context);
        m_deathState = new State_EnemyMeleeDeath(context);
    }
}

public class State_EnemyMeleeIdle : State_Origin<EntityEnemy_Melee>
{
    public State_EnemyMeleeIdle(EntityEnemy_Melee context) : base(context)
    {
    }

    public override void HandleStateTransition()
    {
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}

public class State_EnemyMeleeCombat : State_Origin<EntityEnemy_Melee>
{
    public State_EnemyMeleeCombat(EntityEnemy_Melee context) : base(context)
    {

    }

    public override void HandleStateTransition()
    {
        if (m_context.IsDead)
        {
            m_context.MasterOfState.OnTransitionState(m_context.StateContainer.DeathState);
        }
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        if (GameManager.Instance.IsPaused)
        {

        }

        m_context.PathFindingUtility.OnMoveToTarget();
    }
}

public class State_EnemyMeleeDeath : State_Origin<EntityEnemy_Melee>
{
    public State_EnemyMeleeDeath(EntityEnemy_Melee context) : base(context)
    {
    }

    public override void HandleStateTransition()
    {
        if (!m_context.IsDead)
        {
            Debug.Log("Not dead anymore");
            m_context.MasterOfState.OnTransitionState(m_context.StateContainer.CombatState);
        }
    }

    public override void OnEnter()
    {
        m_context.AudioSource.PlayOneShot(m_context.AudioSource.clip);
        m_context.transform.GetChild(0).gameObject.SetActive(false);
        Entity_Player.Instance.OnStartEvent.RemoveListener(m_context.EngageCombatState);
        m_context.Animator.SetTrigger("IsDead");
        m_context.InitDelegage(15);
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}