using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class TowerPlacement : MonoBehaviour
{
    PlayerInput playerInput;

    private InputAction _touchPressedAction;
    private InputAction _touchHoldAction;

    [Space(5)]
    [Header("Canvas")]
    [SerializeField] GameObject _CharConfirmPlacementCanvas;
    [SerializeField] GameObject _charInfoCanvas;

    float placingGameTimeSpeed = 0.1f;
    float normalGameTimeSpeed = 1f;

    Ray _position;
    [Space(5)]
    [Header("Character Object")]
    [SerializeField] private GameObject _previewTower;
    [SerializeField] private GameObject _tower;
    [Space(5)]
    [Header("Camera")]
    [SerializeField] private Camera _mainCam;

    [Header("Object Material")]
    [SerializeField] Material[] _materia;


    [SerializeField] CinemachineVirtualCamera vcam1;
    [SerializeField] CinemachineVirtualCamera vcam2;

    Renderer rend;

    bool _isPlacing;

    // Material mat;

    GameObject _instPreviewObj;
    GameObject _instObj;

    public enum State
    {
        Default,
        Placing,
        HoldPlacing,
        Placed
    }

    private State currentState;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _touchPressedAction = playerInput.actions["Pressed"];
        _touchHoldAction = playerInput.actions["Hold"];
        currentState = State.Default;
        // _touchPressedAction.canceled += OnHoldCanceled;
    }



    private void Start()
    {

    }

    private void Update()
    {

        switch (currentState)
        {
            case State.Default:

                Time.timeScale = normalGameTimeSpeed;
                vcam1.Priority = 1;
                vcam2.Priority = 0;
                Debug.Log(currentState);
                _charInfoCanvas.SetActive(false);
                break;
            case State.Placing:
                _touchPressedAction.performed += OnPlacing;
                _touchPressedAction.canceled += Hold;
                Time.timeScale = placingGameTimeSpeed;
                //TODO:: Move the camera a bit when palcing
                vcam1.Priority = 0;
                vcam2.Priority = 1;
                Debug.Log(currentState);
                _charInfoCanvas.SetActive(true);
                break;
            case State.HoldPlacing:


                break;
            case State.Placed:


                break;
        }


    }

    private void OnDisable()
    {
        _touchHoldAction.performed -= OnPlacing;
    }

    public void OnPressed()
    {
        if (_isPlacing)
        {
            _isPlacing = false;
            Destroy(_instPreviewObj);
            _CharConfirmPlacementCanvas.SetActive(false);
            currentState = State.Default;
            return;
        }
        if (currentState == State.Default)
        {
            Debug.Log("Press");
            _instPreviewObj = Instantiate(_previewTower, transform.position, Quaternion.identity);//previewModel
            _instPreviewObj.transform.parent = transform;
            //rend = _instPreviewObj.GetComponent<Renderer>();
            //rend.sharedMaterial = _materia[1];
            _isPlacing = true;
            currentState = State.Placing;
        }

    }


    public void ConfirmPlacement()
    {
        if (currentState == State.HoldPlacing)
        {
            _isPlacing = false;
            //rend.sharedMaterial = _materia[0];
            Debug.Log("Touch Ended");
            currentState = State.Default;
            _instObj = Instantiate(_tower, _instPreviewObj.transform.position, Quaternion.identity);
            Destroy(_instPreviewObj);
            _CharConfirmPlacementCanvas.SetActive(false);

        }
    }

    public void OnCancelPlacement()
    {
        _CharConfirmPlacementCanvas.SetActive(false);
        currentState = State.Placing;
    }

    public void Hold(InputAction.CallbackContext context)
    {
        if (currentState == State.Placing)
        {
            currentState = State.HoldPlacing;
            _touchPressedAction.performed -= OnPlacing;
            _CharConfirmPlacementCanvas.SetActive(true);
            _CharConfirmPlacementCanvas.transform.position = _instPreviewObj.transform.position;
        }
    }

    public void OnPlacing(InputAction.CallbackContext context)
    {

        Debug.Log("Placing");
        _position = _mainCam.ScreenPointToRay(Touchscreen.current.position.ReadValue());

        if (Physics.Raycast(_position, out RaycastHit raycastHit))
        {
            _instPreviewObj.transform.position = raycastHit.transform.position;
            Debug.DrawRay(_position.origin, _position.direction * 20, Color.red);
            Debug.Log("Touch Started");
        }
    }
}
