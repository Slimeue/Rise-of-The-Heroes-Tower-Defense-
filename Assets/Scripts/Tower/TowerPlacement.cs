using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class TowerPlacement : MonoBehaviour
{
    //References

    public GameObject[] _meleePlatform;
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

    Renderer rend;

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
                DisablePlatformCanvas();
                break;
            case State.Placing:
                Time.timeScale = placingGameTimeSpeed;
                OnPlacing();
                EnablePlatformCanvas();
                //TODO:: Move the camera a bit when palcing
                vcam1.Priority = 0;
                vcam2.Priority = 1;
                Debug.Log(currentState);
                _charInfoCanvas.SetActive(true);
                break;
            case State.HoldPlacing:
                Debug.Log(currentState);
                _CharConfirmPlacementCanvas.SetActive(true);
                _CharConfirmPlacementCanvas.transform.position = _instPreviewObj.transform.position;

                break;
            case State.Placed:
                Debug.Log(currentState);

                break;
        }

    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {


    }

    public void OnPressed()
    {


        Debug.Log("Game object name: !" + gameObject.name);
        Debug.Log("name of prefab: " + _tower.name);
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
            currentState = State.Placing;
            _isPlacing = true;
        }

    }

    public void ConfirmPlacement()
    {

        if (currentState == State.HoldPlacing)
        {
            DisablePlatformCanvas();
            _isPlacing = false;

            _instObj = Instantiate(_tower, _instPreviewObj.transform.position, Quaternion.identity);
            _instObj.transform.parent = transform.transform;
            Debug.Log("Name: " + _instObj.name);
            Destroy(_instPreviewObj);
            _CharConfirmPlacementCanvas.SetActive(false);

            currentState = State.Default;
        }

    }

    public void OnCancelPlacement()
    {

        _CharConfirmPlacementCanvas.SetActive(false);
        _instPreviewObj.SetActive(false);

    }

    public void Hold()
    {
        if (currentState == State.Placing)
        {

            currentState = State.HoldPlacing;
        }
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
                    Hold();

                }
            }
        }


    }

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
}
