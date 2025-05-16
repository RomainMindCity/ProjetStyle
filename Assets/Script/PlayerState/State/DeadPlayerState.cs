using UnityEngine;

public class DeadPlayerState : PlayerState
{
    public override void StateEnter(PlayerState previousState)
    {
        StateMachine.Velocity = Vector3.zero;

        StateMachine.GetComponent<CharacterController>().enabled = false;

        if (StateMachine.PlayerAnimator != null)
        {
            StateMachine.PlayerAnimator.SetTrigger("Die");
        }
    }

    public override void StateUpdate()
    {
    }
}
