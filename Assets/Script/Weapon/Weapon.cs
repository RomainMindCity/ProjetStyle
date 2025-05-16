using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int attackDamage = 10;
    [HideInInspector] public GameObject owner;

    [SerializeField] private Animator _animator;
    [SerializeField] private string attackBoolParameter = "IsAttacking";

    // Propriété qui lit la valeur dans l'Animator
    private bool IsAttacking
    {
        get
        {
            return _animator != null && _animator.GetBool(attackBoolParameter);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (owner != null && (other.gameObject == owner || other.transform.IsChildOf(owner.transform)))
            return;


        ITriggerable triggerable = other.GetComponent<ITriggerable>();
        if (triggerable != null)
        {
            triggerable.OnTriggerEnterObject(gameObject);
        }

        if (IsAttacking)
        {
            HandleAttack(other);
        }
    }

    private void HandleAttack(Collider other)
    {
        IAttackable attackable = other.GetComponentInParent<IAttackable>();
        if (attackable == null)
            attackable = other.GetComponent<IAttackable>();

        if (attackable != null)
        {
            attackable.TakeHit(gameObject, attackDamage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (owner != null && (other.gameObject == owner || other.transform.IsChildOf(owner.transform)))
            return;

        ITriggerable triggerable = other.GetComponent<ITriggerable>();
        if (triggerable != null)
        {
            triggerable.OnTriggerExitObject(gameObject);
        }
    }
}
