using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine StateMachine;
    protected InputsManager _inputsManager;
    protected PlayerMovementParameters _params;

    public void Init(PlayerStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        _inputsManager = stateMachine.InputsManager;
        _params = stateMachine.PlayerMovementParameters;
        OnStateInit();
    }

    protected virtual void OnStateInit() { }
    public virtual void StateEnter(PlayerState previousState) { }
    public virtual void StateExit(PlayerState nextState) { }
    public virtual void StateUpdate() { }
}
