using Reference.Variable;
using UnityEngine;

namespace Entity
{
    public class Entity_Locomotive : Entity_Train
    {
        [SerializeField] private FloatVariable m_speed;
        [SerializeField] private FloatVariable m_rotationSpeed;

        protected override void OnAwake()
        {
            base.OnAwake();
        }

        protected override void OnStart()
        {
            base.OnStart();
            GameManager.Instance.eventGamePaused.AddListener(OnEngineStop);
        }

        protected override void OnFixedUpdate()
        {
        }

        protected override void OnTriggerEnterAct(Collider2D collision)
        {

        }

        protected override void OnTriggerExitAct(Collider2D collision)
        {
        }

        protected override void OnCollisionEnterAct(Collision2D collision)
        {
            Entity_Living _entity = collision.gameObject.GetComponent<Entity_Living>();
            if (_entity)
            {
                if (_entity is Entity_Wagon)
                {
                    OnInstantDeath();
                    OnDeath();
                }
                else if (_entity is Entity_Enemy && !_entity.IsDead)
                {
                    (_entity as Entity_Enemy).OnCriticalDeath();
                }
            }
        }

        protected override void OnCollisionExitAct(Collision2D collision)
        {
        }

        public override void OnDeath()
        {
            base.OnDeath();
            Entity_Player.Instance.MasterOfState.OnTransitionState(Entity_Player.Instance.StateContainer.DeathState);
        }

        public void OnEngineStart()
        {
            Rigidbody.AddForce(transform.right * m_speed.Value);
        }

        public void OnEngineStop()
        {
            Rigidbody.velocity = Vector2.zero;
        }

        public void OnInput(float angle)
        {
            Rigidbody.rotation = Rigidbody.rotation - 1 * angle * m_rotationSpeed.Value;
            Rigidbody.velocity = transform.right * m_speed.Value;
        }

        public void OnCombatHandleWagons()
        {
            Rigidbody.velocity = transform.right * m_speed.Value;
            _nextWagon.EnqueueTransform(transform);
            _nextWagon.OnMove();
        }
    }
}