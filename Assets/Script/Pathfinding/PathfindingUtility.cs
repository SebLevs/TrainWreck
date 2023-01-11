using UnityEngine;

namespace Pathfinder
{
    public class PathfindingUtility : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody { get; set; }
        
        [Tooltip("To be used when this.gameObject must always face player")]
        [SerializeField] private bool _isAlwaysFacingPlayer = false;
        [Space(10)]
        [SerializeField] private Transform m_target;
        [SerializeField][Min(0)] private float m_speed = 1.0f;
        [SerializeField][Min(0)] private float m_reachedTargetAtDistance = 1.0f;

        public Transform Target { private get => m_target; set => m_target = value; }

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();

            if (m_target == null)
            {
                m_target = Entity_Player.Instance.Locomotive.transform;
            }
        }

        private void FixedUpdate()
        {
            if (!GameManager.Instance.IsPaused)
            {
                if (_isAlwaysFacingPlayer)
                {
                    LookAt2D();
                }
            }
            else 
            {
                Rigidbody.velocity = Vector2.zero;
            }
        }

        public void LookAt2D()
        {
            if (m_target)
            {
                transform.right = m_target.transform.position - transform.position;
            }
        }

        /// <summary>
        /// Will move towards target if OnReachedTarget returns false
        /// </summary>
        public void OnMoveToTarget()
        {
            if (!OnReachedTarget())
            {
                LookAt2D();
                Rigidbody.velocity = transform.right * m_speed;
            }
        }

        /// <summary>
        /// Will stop all movement when target is reached
        /// </summary>
        public bool OnReachedTarget()
        {
            if (!m_target)
            {
                return false;
            }

            if (Vector3.Distance(m_target.transform.position, transform.position) <= m_reachedTargetAtDistance)
            {
                Rigidbody.velocity = Vector2.zero;
                return true;
            }

            return false;
        }
    }
}
