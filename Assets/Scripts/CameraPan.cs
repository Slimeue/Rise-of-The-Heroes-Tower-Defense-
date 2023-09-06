using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CameraPan : MonoBehaviour
{

    [SerializeField] Camera _cam;


    PlayerInput playerInput;

    InputAction touchPanAction;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        touchPanAction = playerInput.actions["Pan"];
    }

    private void OnEnable()
    {
        touchPanAction.performed += TouchMove;
    }

    private void OnDisable()
    {
        touchPanAction.performed -= TouchMove;
    }

    private void TouchMove(InputAction.CallbackContext context)
    {
        Vector3 position = _cam.WorldToViewportPoint(touchPanAction.ReadValue<Vector2>());

        _cam.transform.position = position;


        Debug.Log(position);
    }

    //Comments

}
