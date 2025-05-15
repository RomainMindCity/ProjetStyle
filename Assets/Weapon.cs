using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int attackDamage = 10;
    internal bool isAttacking = false;

    private void OnTriggerEnter(Collider other)
    {
        ITriggerable triggerable = other.GetComponent<ITriggerable>();
        if (triggerable != null)
        {
            triggerable.OnTriggerEnterObject(gameObject);
        }

        if (isAttacking)
        {
            Debug.Log($"Weapon hit {other.name}");
            HandleAttack(other);
        }
    }

    private void HandleAttack(Collider other)
    {
        IAttackable attackable = other.GetComponent<IAttackable>();
        if (attackable != null)
        {
            attackable.TakeHit(gameObject, attackDamage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ITriggerable triggerable = other.GetComponent<ITriggerable>();
        if (triggerable != null)
        {
            triggerable.OnTriggerExitObject(gameObject);
        }
    }
}
