using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Movement Settings")]
    public PlayerMovementParameters BasePlayerMovementParameters;

    [HideInInspector] public Vector3 Velocity;
    [HideInInspector] public InputsManager InputsManager { get; private set; }
    [HideInInspector] public PlayerMovementParameters PlayerMovementParameters;

    private CharacterController _characterController;

    // States
    private readonly IdlePlayerState _idleState = new();
    private readonly RunningPlayerState _runningState = new();
    public PlayerState CurrentState { get; private set; }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        InputsManager = InputsManager.instance;
        PlayerMovementParameters = BasePlayerMovementParameters;

        _idleState.Init(this);
        _runningState.Init(this);
        ChangeState(_idleState);
    }

    private void Update()
    {
        CurrentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        _characterController.Move(Velocity * Time.fixedDeltaTime);
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState?.StateExit(newState);
        var previousState = CurrentState;
        CurrentState = newState;
        CurrentState?.StateEnter(previousState);
    }

    // Accessors for external use
    public IdlePlayerState IdleState => _idleState;
    public RunningPlayerState RunningState => _runningState;
}
