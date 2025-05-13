using UnityEngine;

public class RunningPlayerState : PlayerState
{
    public override void StateUpdate()
    {
        if (_inputsManager.Move == Vector2.zero)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
            return;
        }

        Vector2 input = _inputsManager.Move.normalized;
        Vector3 move = new Vector3(input.x, 0, input.y); // Z pour déplacement avant/arrière

        StateMachine.Velocity = move * _params.maxSpeed;
    }

    public override void StateExit(PlayerState nextState)
    {
        StateMachine.Velocity = Vector3.zero;
    }
}
