public abstract class PlayerState
{
    protected PlayerStateMachine StateMachine;
    protected InputsManager _inputs;

    public virtual void Init(PlayerStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        _inputs = stateMachine.InputsManager;

        if (_inputs == null)
        {
            UnityEngine.Debug.LogError("⚠ _inputs is null in PlayerState.Init()!");
        }
    }

    public virtual void StateEnter(PlayerState previousState) { }
    public virtual void StateExit(PlayerState nextState) { }
    public abstract void StateUpdate();
}
