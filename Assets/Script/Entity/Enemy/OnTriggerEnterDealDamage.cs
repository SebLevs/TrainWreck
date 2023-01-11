using Entity;
using UnityEngine;

public class OnTriggerEnterDealDamage : MonoBehaviour
{
    [SerializeField] private int damage = 7;
    private Entity_Living m_livingEntity;

    private void Start()
    {
        m_livingEntity = GetComponentInParent<Entity_Living>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity_Train hit = collision.GetComponent<Entity_Train>();

        if (hit && !m_livingEntity.IsDead)
        {
            hit.OnHit(damage);
        }
    }
}
