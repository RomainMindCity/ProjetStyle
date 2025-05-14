using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : MonoBehaviour
{
    [Header("Movement Parameters")]
    public PlayerMovementParameters movementParameters;
    public Animator playerAnimator;

    [HideInInspector] public Vector3 Velocity;
    [HideInInspector] public InputsManager InputsManager { get; private set; }

    private CharacterController _controller;

    // States
    private IdlePlayerState _idleState = new();
    private RunningPlayerState _runningState = new();
    public PlayerState CurrentState { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        InputsManager = InputsManager.instance;

        if (InputsManager == null)
        {
            Debug.LogError("InputsManager.instance is null! Make sure it's in the scene.");
        }

        _idleState.Init(this);
        _runningState.Init(this);

        ChangeState(_idleState);
    }

    private void Update()
    {
        _controller.Move(Velocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        CurrentState?.StateUpdate();
    }

    public void ChangeState(PlayerState newState)
    {
        var previous = CurrentState;
        CurrentState?.StateExit(newState);
        CurrentState = newState;
        CurrentState?.StateEnter(previous);

        if (playerAnimator != null)
        {
            if (CurrentState is RunningPlayerState)
            {
                playerAnimator.SetFloat("WalkSpeed", 1f);
            }
            else
            {
                playerAnimator.SetFloat("WalkSpeed", 0f);
            }
        }
    }

    public IdlePlayerState IdleState => _idleState;
    public RunningPlayerState RunningState => _runningState;
}
