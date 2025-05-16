using System.Collections;
using Unity.Behavior;
using UnityEngine;

public class Enemy : MonoBehaviour, IAttackable, IHealth
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeHit(GameObject attacker, int damage)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    private void Die()
    {
        if (animator != null)
        {
            animator.Play("Death_B");
        }

        BehaviorGraphAgent behaviorGraphAgent = GetComponentInChildren<BehaviorGraphAgent>();
        if (behaviorGraphAgent != null)
        {
            behaviorGraphAgent.enabled = false;
        }
        StartCoroutine(WaitAndDestroy(10f));
    }

    IEnumerator WaitAndDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
