using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
public class TowerManager : MonoBehaviour
{



    //TowerHolder
    GameObject[] towerHolder;

    //Platforms
    GameObject[] _platFormsReference;
    string _platformTag;
    CanvasEnabler canvasEnabler;
    //References

    PlayerInput playerInput;

    private InputAction _touchHoldAction;
    private InputAction _touchPressAction;

    CoinsManager coinsManager;
    //Ray
    [Space(5)]
    [Header("Raycast")]
    LayerMask _layerPlatform;
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

    //TimeScale Properties
    float placingGameTimeSpeed = 0.1f;
    float normalGameTimeSpeed = 1f;

    public bool _isPlacing;

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
        coinsManager = FindObjectOfType<CoinsManager>();
        currentState = State.Default;

        towerHolder = GameObject.FindGameObjectsWithTag("TowerHolder");
    }

    GameObject _towerHolder;

    private GameObject _charObj;

    private GameObject _instPreviewObj;
    private GameObject _instObj;

    private int _charCost;




    private void Update()
    {
        switch (currentState)
        {
            case State.Default:
                normalCam.Priority = 1;
                placingCam.Priority = 0;
                Debug.Log("Name: " + gameObject.name + currentState);
                if (_towerHolder != null)
                {
                    EnableTowerHolder(_towerHolder.gameObject.GetComponent<TowerTesting>());
                }
                NormalTime();
                _charInfoCanvas.SetActive(false);
                break;
            case State.Placing:
                PlacingTime();
                _charInfoCanvas.SetActive(true);
                if (_towerHolder != null)
                {
                    DisableTowerHolder(_towerHolder.gameObject.GetComponent<TowerTesting>());
                }
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

    void UnsubscribeAction()
    {
        _touchPressAction.performed -= OnHolding;
        _touchPressAction.canceled -= EndTest;
    }

    public void StartPlacing(GameObject _previewGameObject, GameObject _charObj, int cost, LayerMask layerMask, String platform, GameObject _towerHolder)
    {
        this._charObj = _charObj;
        _layerPlatform = layerMask;
        _platformTag = platform;
        this._towerHolder = _towerHolder;
        _platFormsReference = GameObject.FindGameObjectsWithTag(_platformTag);

        if (currentState == State.HoldPlacing || currentState == State.Placing)
        {
            UnsubscribeAction();
            DisablePlatformCanvas();
            Debug.Log("Press2");
            Destroy(_instPreviewObj);
            _CharConfirmPlacementCanvas.SetActive(false);
            currentState = State.Default;
        }

        else if (currentState == State.Default)
        {
            EnablePlatformCanvas();

            Debug.Log("Press1");
            _isPlacing = true;
            _touchPressAction.performed += OnHolding;
            _touchPressAction.canceled += EndTest;
            _instPreviewObj = Instantiate(_previewGameObject, transform.position, Quaternion.identity);
            currentState = State.Placing;
        }

        _charCost = cost;

    }

    public void ConfirmPlace()
    {

        if (currentState == State.HoldPlacing)
        {
            DisablePlatformCanvas();
            UnsubscribeAction();
            TowerHolderDisabler();//TODO: Make towerHolder disappear
            coinsManager.MinusCoin(_charCost);
            Debug.Log("Minus: " + _charCost);
            _CharConfirmPlacementCanvas.SetActive(false);
            Destroy(_instPreviewObj);
            _instObj = Instantiate(_charObj, _instPreviewObj.transform.position, Quaternion.identity);
            currentState = State.Default;
        }
    }

    public void CancelPlacement()
    {
        DisablePlatformCanvas();
        _CharConfirmPlacementCanvas.SetActive(false);
        Destroy(_instPreviewObj);
        UnsubscribeAction();
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
                    float offset = 2f;
                    Vector3 newPosition = _CharConfirmPlacementCanvas.transform.position;
                    newPosition.y += offset;
                    _CharConfirmPlacementCanvas.transform.position = newPosition;
                }
            }
        }
    }

    #region Enabling/Disabling TowerHolder
    public void TowerHolderDisabler()
    {
        TowerTesting towerTesting = _towerHolder.GetComponent<TowerTesting>();
        towerTesting._placed = true;
    }
    #endregion

    void DisableTowerHolder(TowerTesting butButton)
    {
        Button clickedButton = butButton.gameObject.GetComponent<Button>();
        foreach (GameObject holder in towerHolder)
        {
            Button buttonRef = holder.GetComponent<Button>();

            if (buttonRef != clickedButton)
            {
                buttonRef.enabled = false;
            }
        }
    }

    void EnableTowerHolder(TowerTesting butButton)
    {
        Button clickedButton = butButton.gameObject.GetComponent<Button>();
        foreach (GameObject holder in towerHolder)
        {
            Button buttonRef = holder.GetComponent<Button>();

            if (buttonRef != clickedButton)
            {
                buttonRef.enabled = true;
            }
        }
    }

    #region Enabling Canvas


    void EnablePlatformCanvas()
    {
        foreach (GameObject platform in _platFormsReference)
        {
            canvasEnabler = platform.GetComponent<CanvasEnabler>();
            canvasEnabler.EnableCanvas();
        }
    }

    void DisablePlatformCanvas()
    {
        foreach (GameObject platform in _platFormsReference)
        {
            canvasEnabler = platform.GetComponent<CanvasEnabler>();
            canvasEnabler.DisableCanvas();
        }
    }
    #endregion

    #region timeScale

    void PlacingTime()
    {
        Time.timeScale = placingGameTimeSpeed;
    }
    void NormalTime()
    {
        Time.timeScale = normalGameTimeSpeed;
    }

    #endregion
}
