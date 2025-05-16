using UnityEngine;

public class RunningPlayerState : PlayerState
{
    public override void StateEnter(PlayerState previousState) { }

    public override void StateUpdate()
    {
        if (StateMachine.PlayerController.IsDead) return;
        Vector2 input = _inputs.Move;
        if (input == Vector2.zero)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
            return;
        }

        Vector3 direction = new(input.x, 0, input.y);

        if (_inputs != null && StateMachine != null && StateMachine.MovementParameters != null)
        {
            StateMachine.Velocity = direction.normalized * StateMachine.MovementParameters.maxSpeed;
        }
    }
}
