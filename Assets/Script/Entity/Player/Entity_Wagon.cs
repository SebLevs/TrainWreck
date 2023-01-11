using Entity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entity
{
    [SerializeField]
    public class ContainerNextMove
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public ContainerNextMove(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }

    public class Entity_Wagon : Entity_Train
    {
        private Queue<ContainerNextMove> m_containerNextMove;

        protected override void OnAwake()
        {
            base.OnAwake();
            m_containerNextMove = new Queue<ContainerNextMove>();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnFixedUpdate()
        {
        }

        protected override void Init()
        {
            base.Init();
            EnqueueOnInit();
        }

        public void OnIntantiateInit()
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;

            // Temporary debug
            // TODO: Find way too instantiate new wagon at position - right of current last...
            // ... wagon without it moving straight ahead for no reasons
            // ... ... Tried: Clear of queue + EnqueueOnInit() | Clean of queue | Foo
            GameObject newWagon = Instantiate(Entity_Player.Instance.WagonPrefab, transform.parent);

            newWagon.transform.position = _previousWagon.transform.position;
            newWagon.transform.rotation = _previousWagon.transform.rotation;
            _nextWagon = newWagon.GetComponent<Entity_Wagon>();
            _nextWagon.PreviousWagon = this;
            _nextWagon.Collider.enabled = false;
            Entity_Player.Instance.last = _nextWagon;
        }

        protected override void OnCollisionEnterAct(Collision2D collision)
        {

        }

        protected override void OnCollisionExitAct(Collision2D collision)
        {
        }


        protected override void OnTriggerEnterAct(Collider2D collision)
        {
        }

        protected override void OnTriggerExitAct(Collider2D collision)
        {
        }

        public void OnMove()
        {
            if (m_containerNextMove.Any())
            {
                ContainerNextMove m_nextMove = m_containerNextMove.Dequeue();
                transform.position = m_nextMove.Position;
                transform.rotation = m_nextMove.Rotation;
            }

            if (_nextWagon)
            {
                _nextWagon.OnMove();
            }
        }

        public void EnqueueTransform(Transform data)
        {
            if (_nextWagon)
            {
                _nextWagon.EnqueueTransform(transform);
            }

            m_containerNextMove.Enqueue(new ContainerNextMove(data.position, data.rotation));
        }

        public void EnqueueOnInit()
        {
            int width = (int)SpriteRenderer.sprite.textureRect.width;
            float size = width * 0.001f; // TODO: find way to make it work without jerkings (to replace hardcoded 0.03f monstruosity bellow)
            for (int i = width; i > 0; i--)
            {
                // 0.03f is bvasically width / time.deltatime - ish
                Vector3 nextPos = new Vector3(transform.position.x + 1 - i * 0.03f, transform.position.y, 0.0f);
                m_containerNextMove.Enqueue(new ContainerNextMove(nextPos, Quaternion.identity));
            }
        }
    }
}
