using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)] // Important : Exécuté avant les autres scripts
public class InputsManager : MonoBehaviour
{
    public static InputsManager instance { get; private set; }

    [Header("Input Settings")]
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private GameObject player;

    private PlayerInput playerInput;
    private Vector2 _moveInput;
    private bool _interactPressed;

    public Vector2 Move => _moveInput;
    public bool InputInteract => _interactPressed;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SetupInputSystem();
    }

    private void SetupInputSystem()
    {
        if (inputActions == null)
        {
            Debug.LogError("❌ InputActionAsset not assigned in InputsManager!");
            return;
        }

        playerInput = gameObject.AddComponent<PlayerInput>();
        playerInput.actions = inputActions;
        playerInput.notificationBehavior = PlayerNotifications.InvokeUnityEvents;
        playerInput.camera = Camera.main;

        playerInput.actions["Movement"].performed += ctx => OnMove(ctx.ReadValue<Vector2>());
        playerInput.actions["Movement"].canceled += ctx => _moveInput = Vector2.zero;

        foreach (var action in playerInput.actions)
        {
            action.Enable();
        }
    }

    private void OnMove(Vector2 moveInput)
    {
        _moveInput = moveInput;

        if (Mathf.Abs(moveInput.x) > 0.1f)
        {
            RotatePlayer90();
        }

        if (Mathf.Abs(moveInput.y) > 0.1f)
        {
            FlipPlayer();
        }
    }

    private void RotatePlayer90()
    {
        if (player != null)
        {
            player.transform.Rotate(0, 90, 0);
        }
    }

    private void FlipPlayer()
    {
        if (player != null)
        {
            Vector3 scale = player.transform.localScale;
            scale.x *= -1;
            player.transform.localScale = scale;
        }
    }
}
