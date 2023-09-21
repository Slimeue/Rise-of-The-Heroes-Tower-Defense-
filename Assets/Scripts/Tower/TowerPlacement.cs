using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UI;

public class TowerPlacement : MonoBehaviour
{
    //References
    public GameObject _characterParents;
    public GameObject[] _meleePlatform;
    public GameObject[] _characterHolder;
    CanvasEnabler canvasEnabler;
    //
    [Space(5)]
    [Header("Canvas")]
    [SerializeField] GameObject _CharConfirmPlacementCanvas;
    [SerializeField] GameObject _charInfoCanvas;
    [SerializeField] string _platformTag;

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

    [Space(5)]
    [Header("Raycast")]
    [SerializeField] LayerMask _layerPlatform;
    float maxDistance;

    [SerializeField] CinemachineVirtualCamera vcam1;
    [SerializeField] CinemachineVirtualCamera vcam2;

    bool rayCastHit;

    bool _isPlacing;

    // Material mat;

    private GameObject _instPreviewObj;
    private GameObject _instObj;

    private enum State
    {
        Default,
        Placing,
        HoldPlacing,
        Placed
    }

    private State currentState;


    private void Awake()
    {

        currentState = State.Default;

        _meleePlatform = GameObject.FindGameObjectsWithTag(_platformTag);
        _characterHolder = GameObject.FindGameObjectsWithTag("TowerHolder");

    }

    private void Update()
    {

        switch (currentState)
        {
            case State.Default:
                Debug.Log("Name: " + gameObject.name + currentState);
                break;
            case State.Placing:
                OnPlacing();
                Debug.Log("Name: " + gameObject.name + currentState);
                break;
            case State.HoldPlacing:
                Debug.Log("Name: " + gameObject.name + currentState);
                break;
            case State.Placed:
                Debug.Log("Name: " + gameObject.name + currentState);
                break;
        }

    }


    public void OnPressed()
    {


        Debug.Log("Game object name: !" + gameObject.name);
        Debug.Log("name of prefab: " + _tower.name);
        if (_isPlacing)
        {
            NormalTime();
            _isPlacing = false;
            Destroy(_instPreviewObj);
            EnableTowerHolder(gameObject.GetComponent<Button>());
            _CharConfirmPlacementCanvas.SetActive(false);
            vcam1.Priority = 1;
            vcam2.Priority = 0;
            _charInfoCanvas.SetActive(false);
            DisablePlatformCanvas();
            currentState = State.Default;
            return;
        }
        if (currentState == State.Default)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;

            Debug.Log("Press");
            _instPreviewObj = Instantiate(_previewTower, transform.position, Quaternion.identity);//previewModel
            _instPreviewObj.transform.parent = transform;
            DisableTowerHolder(gameObject.GetComponent<Button>());
            EnablePlatformCanvas();
            PlacingTime();
            _charInfoCanvas.SetActive(true);
            currentState = State.Placing;
            _isPlacing = true;
        }

    }

    public void ConfirmPlacement()
    {

        if (currentState == State.HoldPlacing)
        {
            EnableTowerHolder(gameObject.GetComponent<Button>());
            DisablePlatformCanvas();
            NormalTime();
            _isPlacing = false;
            _instObj = Instantiate(_tower, _instPreviewObj.transform.position, Quaternion.identity);
            _instObj.transform.parent = _characterParents.transform;
            Debug.Log("Name: " + _instObj.name);
            Destroy(_instPreviewObj);
            _charInfoCanvas.SetActive(false);
            DisablePlatformCanvas();
            _CharConfirmPlacementCanvas.SetActive(false);
            vcam1.Priority = 1;
            vcam2.Priority = 0;
            currentState = State.Default;
            gameObject.SetActive(false);
        }

    }

    public void OnCancelPlacement()
    {
        NormalTime();
        _isPlacing = false;
        Destroy(_instPreviewObj);
        EnableTowerHolder(gameObject.GetComponent<Button>());
        _CharConfirmPlacementCanvas.SetActive(false);
        vcam1.Priority = 1;
        vcam2.Priority = 0;
        _charInfoCanvas.SetActive(false);
        DisablePlatformCanvas();
        currentState = State.Default;
    }

    public void OnPlacing()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("Placing");
            if (_instPreviewObj != null)
            {
                _instPreviewObj.SetActive(true);
                _position = _mainCam.ScreenPointToRay(touch.position);
                rayCastHit = Physics.Raycast(_position, out RaycastHit raycastHit, maxDistance = Mathf.Infinity, _layerPlatform);
                if (rayCastHit)
                {
                    _instPreviewObj.transform.position = raycastHit.transform.position;
                    Debug.DrawRay(_position.origin, _position.direction * 20, Color.red);
                    Debug.Log("Touch Started");
                    _CharConfirmPlacementCanvas.SetActive(true);
                    _CharConfirmPlacementCanvas.transform.position = _instPreviewObj.transform.position;
                    currentState = State.HoldPlacing;
                }
            }
        }


    }


    #region enabling
    void EnablePlatformCanvas()
    {
        foreach (GameObject platform in _meleePlatform)
        {
            canvasEnabler = platform.GetComponent<CanvasEnabler>();
            canvasEnabler.EnableCanvas();
        }
    }

    void DisablePlatformCanvas()
    {
        foreach (GameObject platform in _meleePlatform)
        {
            canvasEnabler = platform.GetComponent<CanvasEnabler>();
            canvasEnabler.DisableCanvas();
        }
    }

    void DisableTowerHolder(Button butButton)
    {
        Button clickedButton = butButton.gameObject.GetComponent<Button>();
        foreach (GameObject holder in _characterHolder)
        {
            Button buttonRef = holder.GetComponent<Button>();

            if (buttonRef != clickedButton)
            {
                buttonRef.enabled = false;
            }
        }
    }

    void EnableTowerHolder(Button butButton)
    {
        Button clickedButton = butButton.gameObject.GetComponent<Button>();
        foreach (GameObject holder in _characterHolder)
        {
            Button buttonRef = holder.GetComponent<Button>();

            if (buttonRef != clickedButton)
            {
                buttonRef.enabled = true;
            }
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
