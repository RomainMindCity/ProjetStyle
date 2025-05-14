using UnityEngine;

public class IdlePlayerState : PlayerState
{
    public override void StateEnter(PlayerState previousState)
    {
        StateMachine.Velocity = Vector3.zero;
    }

    public override void StateUpdate()
    {
        Vector2 input = _inputs.Move;
        if (input != Vector2.zero)
        {
            StateMachine.ChangeState(StateMachine.RunningState);
        }
    }
}
