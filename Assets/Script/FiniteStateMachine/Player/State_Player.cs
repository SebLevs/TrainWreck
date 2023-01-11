using System;
using UnityEngine;

namespace FiniteStateMachine
{
    [Serializable]
    public class StateContainer_Player : StateContainer
    {
        [SerializeField] private State_PlayerIddle m_iddleState;
        [SerializeField] private State_PlayerCombat m_combatState;
        [SerializeField] private State_PlayerEndGame m_deathState;

        public State_PlayerIddle IddleState { get => m_iddleState; }
        public State_PlayerCombat CombatState { get => m_combatState; }
        public State_PlayerEndGame DeathState { get => m_deathState; }

        public StateContainer_Player(Entity_Player context)
        {
            m_iddleState = new State_PlayerIddle(context);
            m_combatState = new State_PlayerCombat(context);
            m_deathState = new State_PlayerEndGame(context);
        }
    }

    [Serializable]
    public class State_PlayerIddle : State_Origin<Entity_Player>
    {
        public State_PlayerIddle(Entity_Player context): base(context)
        {
        }

        public override void HandleStateTransition()
        {
            if (m_context.Inputs.Direction.x != 0 || m_context.Inputs.Direction.y != 0)
            {
                m_context.MasterOfState.OnTransitionState(m_context.StateContainer.CombatState);
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
            if (m_context.Inputs.Direction.x != 0 || m_context.Inputs.Direction.y != 0)
            {
                m_context.Locomotive.OnEngineStart();
            }
        }
    }

    [Serializable]
    public class State_PlayerCombat : State_Origin<Entity_Player>
    {
        public State_PlayerCombat(Entity_Player context): base(context)
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
            m_context.OnStartEvent.Invoke();
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
            if (m_context.Inputs.Direction.x != 0 || m_context.Inputs.Direction.y != 0)
            {
                m_context.Locomotive.OnInput(m_context.Inputs.Direction.x);
            }
        }
    }

    [Serializable]
    public class State_PlayerEndGame : State_Origin<Entity_Player>
    {
        public State_PlayerEndGame(Entity_Player context) : base(context)
        {
        }

        public override void HandleStateTransition()
        {
        }

        public override void OnEnter()
        {
            ScoreManager.Instance.ShowRestart("WRECKED!");
            GameManager.Instance.eventGamePaused.Invoke();
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}
