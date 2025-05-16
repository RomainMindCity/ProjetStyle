using UnityEngine;

public interface IHealth
{
    int CurrentHealth { get; }
    int MaxHealth { get; }

    void TakeDamage(int amount);
    void Heal(int amount);
}
