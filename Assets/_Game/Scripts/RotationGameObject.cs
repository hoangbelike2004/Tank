using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationGameObject : MonoBehaviour
{
    #region InputSystem
    [SerializeField] private InputActionAsset _actionRotation;
    public InputActionAsset action
    {
        get => _actionRotation;
        set=> _actionRotation = value;
    }
    protected InputAction LeftClickPressedInputAction { get; set; }
    protected InputAction MouseLookInputAction { get; set; }
    #endregion

    #region Variables
    private bool _isRotationAllowed;
    private Camera _camera;
    [SerializeField] private float speedTouchToRotation,speedAutomatic;
    [SerializeField] private bool _inverted;
    #endregion
    void Awake()
    {
        InitializeInputSystem();
    }
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;//hiden cursor
        _camera = Camera.main;
    }
    public void Update()
    {
        
        if (!_isRotationAllowed)
        {
            gameObject.transform.Rotate(0, speedAutomatic, 0);
        }
        else
        {
            Vector2 mouseDelta = GetMouseLookInput();

            mouseDelta *= speedTouchToRotation * Time.deltaTime;
            transform.Rotate(Vector3.up * (_inverted ? 1 : -1), mouseDelta.x, Space.World);
            //transform.Rotate(Vector3.right * (_inverted ? -1 : 11), mouseDelta.x, Space.World);
        }
    }
    private void InitializeInputSystem()
    {
        LeftClickPressedInputAction = action.FindAction("Left Click");
        if (LeftClickPressedInputAction != null)
        {
            LeftClickPressedInputAction.started += OnLeftClickPressed;
            LeftClickPressedInputAction.performed += OnLeftClickPressed;
            LeftClickPressedInputAction.canceled += OnLeftClickPressed;
        }
        MouseLookInputAction = action.FindAction("Mouse Look");
        action.Enable();
    }
    protected virtual void OnLeftClickPressed(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            _isRotationAllowed = true;
        }
        else if (context.canceled)
        {
            _isRotationAllowed = false;
        }
    }

    protected virtual Vector2 GetMouseLookInput()
    {
        if(MouseLookInputAction != null)
        {
            return MouseLookInputAction.ReadValue<Vector2>();//lay gia tri dau vao cua chuot(toc do di chuot tren man)
        }
        return Vector2.zero;
    }
}
