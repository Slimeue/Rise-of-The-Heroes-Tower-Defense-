using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour
{

    //References
    PlayerInput playerInput;

    private InputAction _touchHoldAction;
    private InputAction _touchPressAction;

    //Ray
    [Space(5)]
    [Header("Raycast")]
    [SerializeField] LayerMask _layerPlatform;
    Ray _position;
    bool rayCastHit;

    [Space(5)]
    [Header("Camera")]
    [SerializeField] private Camera _mainCam;
    [SerializeField] CinemachineVirtualCamera normalCam;
    [SerializeField] CinemachineVirtualCamera placingCam;

    [Space(5)]
    [Header("Canvas")]
    [SerializeField] GameObject _CharConfirmPlacementCanvas;
    [SerializeField] GameObject _charInfoCanvas;

    bool _isPlacing;

    float maxDistance;

    private enum State
    {
        Default,
        Placing,
        HoldPlacing
    }

    private State currentState;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _touchHoldAction = playerInput.actions["Hold"];
        _touchPressAction = playerInput.actions["TouchPress"];

        currentState = State.Default;
    }

    private GameObject _charObj;

    private GameObject _instPreviewObj;
    private GameObject _instObj;

    private void Update()
    {
        switch (currentState)
        {
            case State.Default:
                normalCam.Priority = 1;
                placingCam.Priority = 0;
                Debug.Log("Name: " + gameObject.name + currentState);
                break;
            case State.Placing:
                normalCam.Priority = 0;
                placingCam.Priority = 1;
                Debug.Log("Name: " + gameObject.name + currentState);
                break;
            case State.HoldPlacing:
                Debug.Log("Name: " + gameObject.name + currentState);
                break;
        }
    }

    public void OnHolding(InputAction.CallbackContext context)
    {
        Debug.Log("Holding");
        _touchHoldAction.performed += Placing;
    }

    void EndTest(InputAction.CallbackContext context)
    {
        _isPlacing = false;
        currentState = State.HoldPlacing;
    }

    public void StartPlacing(GameObject gameObject, GameObject _charObj)
    {
        this._charObj = _charObj;
        if (currentState == State.Default)
        {
            //normalCam = 0;
            //placingCam = 1;
            _isPlacing = true;
            _touchPressAction.performed += OnHolding;
            _touchPressAction.canceled += EndTest;
            _instPreviewObj = Instantiate(gameObject, transform.position, Quaternion.identity);
            currentState = State.Placing;
        }
    }

    public void ConfirmPlace()
    {
        if (currentState == State.HoldPlacing)
        {
            _CharConfirmPlacementCanvas.SetActive(false);
            _touchPressAction.performed -= OnHolding;
            _touchPressAction.canceled -= EndTest;
            Destroy(_instPreviewObj);
            _instObj = Instantiate(_charObj, _instPreviewObj.transform.position, Quaternion.identity);
            currentState = State.Default;
        }
    }

    public void CancelPlacement()
    {
        _CharConfirmPlacementCanvas.SetActive(false);
        Destroy(_instPreviewObj);
        _touchPressAction.performed -= OnHolding;
        _touchPressAction.canceled -= EndTest;
        currentState = State.Default;
    }

    public void Placing(InputAction.CallbackContext context)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (_instPreviewObj != null)
            {
                _position = _mainCam.ScreenPointToRay(Touchscreen.current.position.ReadValue());
                rayCastHit = Physics.Raycast(_position, out RaycastHit raycastHit, maxDistance = Mathf.Infinity, _layerPlatform);
                if (rayCastHit && _isPlacing)
                {
                    _instPreviewObj.transform.position = raycastHit.transform.position;
                    Debug.DrawRay(_position.origin, _position.direction * 20, Color.red);
                    Debug.Log("Touch Started");
                    _CharConfirmPlacementCanvas.SetActive(true);
                    _CharConfirmPlacementCanvas.transform.position = _instPreviewObj.transform.position;
                }
            }
        }
    }




}
