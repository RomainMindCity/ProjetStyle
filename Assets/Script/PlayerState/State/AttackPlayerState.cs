using UnityEngine;

public class AttackPlayerState : PlayerState
{
    private float _attackDuration = 0.5f;
    private float _attackTimer;

    public override void StateEnter(PlayerState previousState)
    {
        _attackTimer = _attackDuration;

        PerformAttack();
    }

    public override void StateUpdate()
    {
        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0f)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
            StateMachine.weapon.isAttacking = false;
        }
    }

    private void PerformAttack()
    {
        StateMachine.PlayAnimation();
        if (StateMachine.weapon != null)
        {
            StateMachine.weapon.isAttacking = true;
        }
        else
        {
            Debug.LogError("Weapon component not found in children.");
        }
    }
}
