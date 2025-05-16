using UnityEngine;

public class AttackPlayerState : PlayerState
{
    private float _attackDuration = 0.5f;
    private float _attackTimer;
    private bool _attackEnded = false;
    private float _waitAfterAttack = 0.5f;
    private float _waitTimer = 0f;
    private Animator _animator;
    public override void StateEnter(PlayerState previousState)
    {
        _attackTimer = _attackDuration;

        StateMachine.Velocity = Vector3.zero;

        if (_animator == null && StateMachine != null)
        {
            _animator = StateMachine.GetComponentInChildren<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Animator component not found on StateMachine GameObject.");
            }
        }

        StateMachine.Velocity = Vector3.zero;

        if (StateMachine.weapon != null && _animator != null)
        {
            _animator.SetBool("IsAttacking", true);
            _animator.SetTrigger("Attack");
        }
        else if (StateMachine.weapon == null)
        {
            Debug.LogError("Weapon component not found in children.");
        }
    }

    public override void StateUpdate()
    {
        if (!_attackEnded)
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer <= 0f)
            {
                if (_animator != null)
                {
                    _animator.SetBool("IsAttacking", false);
                }
                _attackEnded = true;
                _waitTimer = _waitAfterAttack;
            }
        }
        else
        {
            _waitTimer -= Time.deltaTime;
            if (_waitTimer <= 0f)
            {
                StateMachine.ChangeState(StateMachine.IdleState);
            }
        }
    }

    private void PerformAttack()
    {
        StateMachine.PlayAnimation();
        if (StateMachine.weapon != null && _animator != null)
        {
            _animator.SetBool("IsAttacking", true);
        }
        else
        {
            Debug.LogError("Weapon component not found in children.");
        }
    }
}
