using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacement : MonoBehaviour
{
    PlayerInput playerInput;

    private InputAction _touchPressedAction;
    private InputAction _touchHoldAction;



    Ray _position;
    bool _isPlacing;

    [SerializeField] LayerMask layer;
    [SerializeField] private GameObject _tower;
    [SerializeField] private Camera _mainCam;

    // [SerializeField] private Material mainMat;
    // [SerializeField] private Material transpMat;

    [SerializeField] Material[] _materia;
    Renderer rend;

    // Material mat;

    GameObject _instObj;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        _touchPressedAction = playerInput.actions["Pressed"];
        _touchHoldAction = playerInput.actions["Hold"];

    }



    private void Update()
    {
        // if (isPlacing)
        // {
        //     touchHoldAction.started += OnPlacing;
        //     touchHoldAction.performed += OnPlacing;
        // }
        // else
        // {
        //     touchHoldAction.started -= OnPlacing;
        //     touchHoldAction.performed -= OnPlacing;
        // }

    }

    private void OnEnable()
    {


        _touchPressedAction.performed += OnPressed;
        _touchPressedAction.canceled += OnPressedCanceled;
        _touchHoldAction.performed += OnPlacing;
        _touchHoldAction.canceled += OnPlacing;





    }

    private void OnDisable()
    {
        _touchPressedAction.performed -= OnPressed;
        _touchHoldAction.performed -= OnPlacing;
        _touchHoldAction.canceled -= OnPressedCanceled;


        // touchHoldAction.performed -= OnPlacing;
    }

    public void OnPressed(InputAction.CallbackContext context)
    {

        _isPlacing = true;
        Debug.Log("Click");
        //TODO: Instantiate Preview Model of Tower

        _instObj = Instantiate(_tower, transform.position, Quaternion.identity);
        rend = _instObj.GetComponent<Renderer>();
        rend.sharedMaterial = _materia[1];
        // mat = _instObj.GetComponent<Renderer>().material;
        // mat = transpMat;
    }

    void OnPressedCanceled(InputAction.CallbackContext context)
    {
        _isPlacing = false;
        rend.sharedMaterial = _materia[0];
        //Destroy(_instObj);
    }

    void OnPlacing(InputAction.CallbackContext context)
    {

        _position = _mainCam.ScreenPointToRay(Touchscreen.current.position.ReadValue());

        if (Physics.Raycast(_position, out RaycastHit raycastHit) && _isPlacing)
        {
            _instObj.transform.position = raycastHit.transform.position;
            Debug.DrawRay(_position.origin, _position.direction * 20, Color.red);
            Debug.Log(raycastHit.point);
        }


    }







}
