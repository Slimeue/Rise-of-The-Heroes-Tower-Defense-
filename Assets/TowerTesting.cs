using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TowerTesting : MonoBehaviour
{
    PlayerInput playerInput;
    private InputAction _touchPressedAction;
    private InputAction _touchPlacePressAction;
    private InputAction _touchPressAction;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _touchPressedAction = playerInput.actions["Pressed"];
        _touchPressAction = playerInput.actions["Press"];
        _touchPlacePressAction = playerInput.actions["PlacePress"];
    }

    private void OnEnable()
    {
        _touchPressAction.started += DebugLog;
        //_touchPressedAction.started += DebugLog;
    }

    private void OnDisable()
    {
        _touchPressAction.started -= DebugLog;
        //_touchPressedAction.started -= DebugLog;
    }

    private void DebugLog(InputAction.CallbackContext context)
    {
        Debug.Log(gameObject.name);
    }


}
