using UnityEngine;
using Reference.Primitive;

namespace Entity
{
    public abstract class Entity_Living : Entity_Collider
    {
        [SerializeField] protected IntReference m_maxHP;
        [SerializeField] protected IntReference m_currentHP;
        public AudioSource AudioSource { get; protected set; }

        public SpriteRenderer SpriteRenderer { get; protected set; }
        public Animator Animator { get; protected set; }
         
        public bool IsDead => m_currentHP.Value <= 0;

        protected override void OnAwake()
        {
            base.OnAwake();
        }

        protected override void OnStart()
        {
        }

        protected override void Init()
        {
            base.Init();
            FullHeal();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
            AudioSource = GetComponent<AudioSource>();
        }

        public virtual void OnHit(int damage)
        {
            m_currentHP.Value -= damage;
            AudioSource.PlayOneShot(AudioSource.clip);

            if (m_currentHP.ClampMin(0))
            {
                OnDeath();
            }
        }

        public virtual void OnInstantDeath()
        {
            AudioSource.PlayOneShot(AudioSource.clip);
            m_currentHP.Value -= m_currentHP.Value;
            m_currentHP.ClampMin(0);
            OnDeath();
        }

        protected void OnHeal(int value)
        {
            m_currentHP.ClampMax(value);
        }

        public virtual void OnDeath()
        {
            // TODO: Create logic on death of living entity
        }

        public void SetCurrentHP(int value)
        {
            m_currentHP.Value = value;
            m_currentHP.ClampValue(0, m_maxHP.Value);
        }

        public void SetMaxHP(int value)
        {
            m_maxHP.Value = value;
            m_maxHP.ClampMin(0);
        }

        public void FullHeal()
        {
            m_currentHP.Value = m_maxHP.Value;
        }
    }
}
