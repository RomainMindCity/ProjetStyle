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
    [SerializeField] private Weapon weapon;
    [SerializeField] private HealthBar healthBar;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        healthBar?.Initialize(maxHealth);
        weapon = GetComponentInChildren<Weapon>();
        weapon.owner = this.gameObject;
    }

    public void TakeHit(GameObject attacker, int damage)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        healthBar?.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        healthBar?.UpdateHealth(currentHealth);
    }

    private void Die()
    {
        if (animator != null)
        {
            animator.SetBool("Die", true);
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
