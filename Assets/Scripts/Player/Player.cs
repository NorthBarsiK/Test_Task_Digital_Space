using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour, PlayerControls.IControlsActions
{
    [SerializeField] private float movementSpeed = 3.0f;
    [Space]
    [SerializeField] private float maxVerticalLookAngle = 75.0f;
    [SerializeField] private float minVerticalLookAngle = -75.0f;
    [SerializeField] private float mouseSensivity = 0.1f;

    #region InputActions
    private PlayerControls playerControls;
    private Vector2 movementVectorInput = new Vector2();
    private Vector2 mouseDeltaInput = new Vector2();
    private bool pushButtonInput = false;
    public void OnMovement(InputAction.CallbackContext context)
    {
        movementVectorInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDeltaInput = context.ReadValue<Vector2>();
    }

    public void OnPushButton(InputAction.CallbackContext context)
    {
        pushButtonInput = context.ReadValueAsButton();
    }

    public void OnExit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Controls.SetCallbacks(this);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    #endregion

    private Camera playerCamera = null;
    void Start()
    {
        playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Looking();
    }

    private float verticalMouseMotion = 0;
    private void Looking()
    {
        float horizontalMouseMotion = mouseDeltaInput.x * mouseSensivity;
        transform.eulerAngles += new Vector3(0.0f, horizontalMouseMotion, 0.0f);

        verticalMouseMotion -= mouseDeltaInput.y * mouseSensivity;
        verticalMouseMotion = Mathf.Clamp(verticalMouseMotion, minVerticalLookAngle, maxVerticalLookAngle);
        playerCamera.transform.localEulerAngles = new Vector3(verticalMouseMotion, 0.0f, 0.0f);
    }

    void FixedUpdate()
    {
        Movement();
        ClampPlayerPosition();
        PressButton();
    }

    private void Movement()
    {
        Vector3 velocity = new Vector3(movementVectorInput.x, 0, movementVectorInput.y);
        velocity *= movementSpeed * Time.fixedDeltaTime;
        velocity = transform.TransformDirection(velocity);
        transform.position += velocity;
    }

    //Create invisible walls for player
    [Header("Movement Constraints")]
    [SerializeField] private float playerMovementMinPositionX = 0.0f;
    [SerializeField] private float playerMovementMaxPositionX = 0.0f;
    [SerializeField] private float playerMovementMinPositionZ = 0.0f;
    [SerializeField] private float playerMovementMaxPositionZ = 0.0f;
    private void ClampPlayerPosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, playerMovementMinPositionX, playerMovementMaxPositionX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, playerMovementMinPositionZ, playerMovementMaxPositionZ);
        transform.position = clampedPosition;
    }

    [Space] [SerializeField] private float buttonPressDistance = 3.0f;
    private Vector3 centerOfScreen = Vector3.zero;
    private void PressButton()
    {
        centerOfScreen.x = Screen.width / 2;
        centerOfScreen.y = Screen.height / 2;
        Ray ray = playerCamera.ScreenPointToRay(centerOfScreen);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, buttonPressDistance))
        {
            var hitObject = hit.transform.GetComponent<IButton>();
            if (hitObject != null)
            {
                HUD.instance.ShowUseButtonTip();
                if (pushButtonInput)
                {
                    hitObject.OnButtonPressed();
                }
            }
        }
        else
        {
            HUD.instance.HideUseButtonTip();
        }
    }
}

