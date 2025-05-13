using UnityEngine;

public class IdlePlayerState : PlayerState
{
    public override void StateEnter(PlayerState previousState)
    {
        StateMachine.Velocity = Vector3.zero;
    }

    public override void StateUpdate()
    {
        if (_inputsManager.Move != Vector2.zero)
        {
            StateMachine.ChangeState(StateMachine.RunningState);
        }
    }
}
