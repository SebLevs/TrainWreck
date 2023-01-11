using UnityEngine;

namespace Entity
{
    public abstract class Entity_Collider : Entity_Origin
    {
        public Rigidbody2D Rigidbody { get; protected set; }
        public Collider2D Collider { get; protected set; }

        protected override void OnAwake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnterAct(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            OnCollisionExitAct(collision);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTriggerEnterAct(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnTriggerExitAct(collision);
        }

        protected abstract void OnCollisionEnterAct(Collision2D collision);
        protected abstract void OnCollisionExitAct(Collision2D collision);
        protected abstract void OnTriggerEnterAct(Collider2D collision);
        protected abstract void OnTriggerExitAct(Collider2D collision);

        protected override void Init()
        {
            //Collider.enabled = true;
        }
    }
}

