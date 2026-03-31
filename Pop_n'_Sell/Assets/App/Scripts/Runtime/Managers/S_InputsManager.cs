using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_InputsManager : MonoBehaviour
{
    [TabGroup("References")]
    [Title("Player Input")]
    [SerializeField] private PlayerInput playerInput;

    [TabGroup("Outputs")]
    [SerializeField] private RSE_OnEscapeInput rseOnEscapeInput;

    private IA_PlayerInput iaPlayerInput = null;
    private bool inputInitialized = false;

    private void Awake()
    {
        inputInitialized = false;
        iaPlayerInput = new IA_PlayerInput();
        playerInput.actions = iaPlayerInput.asset;
    }

    private void OnEnable()
    {
        playerInput.actions.Enable();

        iaPlayerInput.Player.Escape.performed += OnEscapeChanged;
    }

    private void OnDisable()
    {
        playerInput.actions.Disable();

        iaPlayerInput.Player.Escape.performed -= OnEscapeChanged;
    }

    private void Start()
    {
        StartCoroutine(S_Utils.DelayRealTime(0.6f, () => inputInitialized = true));
    }

    void DesactivatePlayerInput(Transform transform)
    {
        if (inputInitialized)
        {
            iaPlayerInput.Player.Disable();
        }   
    }

    private void OnEscapeChanged(InputAction.CallbackContext ctx)
    {
        if (inputInitialized)
        {
            rseOnEscapeInput.Call();
        }
    }
}