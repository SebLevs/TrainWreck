using Entity;
using UnityEngine;

public class OnOutsideBoundKillPlayer : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Entity_Train hit = collision.GetComponent<Entity_Train>();

        if (hit)
        {
            hit.OnInstantDeath();
        }
    }
}
