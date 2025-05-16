using UnityEngine;

public interface IAttackable
{
    void TakeHit(GameObject attacker, int damage);
}
