using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsManager : MonoBehaviour
{
    // permet de r�cup les inputs bools/float ou vector2 partout dans le code
    #region InputVariables
    private bool _inputInteract;
    private Vector2 _move;
    private Vector2 _lookaround;
    #endregion
    #region InputPropri�t�s 
    public bool InputInteract { get => _inputInteract; set => _inputInteract=value; }
    public Vector2 Move { get => _move; }
    public Vector2 Lookaround { get => _lookaround;}
    #endregion

    #region InputMethodes
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _inputInteract = true;
        }
        
        if (context.canceled)
        {
            _inputInteract = false;
        }
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }

    #endregion
    public static InputsManager instance = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        SetupInputs();

        // Initialisation du Game Manager...
    }

    private void InvokeInputMethod(string methodName, InputAction.CallbackContext context)
    {
        var method = GetType().GetMethod(methodName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
        if (method != null)
        {
            method.Invoke(this, new object[] { context });
        }
        else
        {
            Debug.LogWarning($"Méthode {methodName} introuvable dans InputsManager.");
        }
    }

    #region Init/InputAction
    public PlayerInput _playerInputs;
    [SerializeField] private InputActionAsset _inputActionAsset;

    private void SetupInputs()
    {
        _playerInputs = gameObject.AddComponent<PlayerInput>();
        _playerInputs.camera = FindFirstObjectByType<Camera>();
        _playerInputs.notificationBehavior = PlayerNotifications.InvokeUnityEvents;
        _playerInputs.actions = _inputActionAsset;

        foreach (var action in _playerInputs.actions)
        {
            action.performed += ctx => InvokeInputMethod($"On{action.name}", ctx);
            action.canceled += ctx => InvokeInputMethod($"On{action.name}", ctx);
            action.Enable();
        }
    }
    #endregion
}
