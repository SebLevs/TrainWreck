using Pathfinder;
using UnityEngine;

namespace Entity
{
    public abstract class Entity_Enemy : Entity_Living
    {
        [SerializeField] private int m_scoreWorth = 7;
        [SerializeField] protected GameObject attackBox;
        public PathfindingUtility PathFindingUtility { get; protected set; }
        public ParticleSystem ParticleSystem { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();
            PathFindingUtility = GetComponent<PathfindingUtility>();
            ParticleSystem = GetComponent<ParticleSystem>();
        }

        protected override void OnStart()
        {
            base.OnStart();
            
        }

        protected override void OnFixedUpdate()
        {
        }

        protected override void OnTriggerEnterAct(Collider2D collision)
        {
            OnNewTargetInRange(collision);
        }

        protected override void Init()
        {
            base.Init();
            SetCurrentHP(m_scoreWorth);
            PathFindingUtility.enabled = true;
            SpriteRenderer.enabled = true;
            Collider.enabled = true;
        }

        // Terrible code, last minute implementation to have somewhat of a gameplay, don't juge :P
        public void InitDelegage(float time) 
        {
            Invoke("Init", time);
            Invoke("EngageCombatState", time);
        }

        public abstract void EngageCombatState();

        public override void OnDeath()
        {
            base.OnDeath();
            ScoreManager.Instance.AddScore(m_scoreWorth);
        }

        public void OnCriticalDeath()
        {
            SetCurrentHP(0);
            ScoreManager.Instance.AddScore(m_scoreWorth);
            PathFindingUtility.enabled = false;
            SpriteRenderer.enabled = false;
            Collider.enabled = false;
            ParticleSystem.Play(); // TODO: Make into a pool
            attackBox.SetActive(false);
        }

        /// <summary>
        /// Uses the only box trigger on this.gameObject<br/>
        /// You may override this method for enemy type specific behaviour
        /// </summary>
        protected virtual void OnNewTargetInRange(Collider2D collision)
        {
            if (!IsDead)
            {
                Entity_Train hit = collision.GetComponent<Entity_Train>();
                if (hit)
                {
                    PathFindingUtility.Target = hit.transform;
                    PathFindingUtility.LookAt2D();
                }
            }
        }
    }
}
