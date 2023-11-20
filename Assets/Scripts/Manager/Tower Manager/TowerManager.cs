using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using TMPro;
using System.IO;
public class TowerManager : MonoBehaviour
{



    //TowerHolder
    GameObject[] towerHolder;

    //Platforms
    public GameObject[] _platFormsReference;
    string _platformTag;
    public CanvasEnabler canvasEnabler;
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
    [SerializeField] GameObject _charDeleteCanvas;
    [SerializeField] GameObject _charInfoCanvas;
    [Header("Char Info Attributes")]
    [SerializeField] Image charInfoArtWork;
    [SerializeField] TextMeshProUGUI charInfoName;
    [SerializeField] Image charInfoTypeImage;
    [SerializeField] TextMeshProUGUI charInfoLevel;
    [SerializeField] TextMeshProUGUI charInfoHp;
    [SerializeField] TextMeshProUGUI charInfoArmor;
    [SerializeField] TextMeshProUGUI charInfoAttack;
    [SerializeField] Image charInfoSkillImage;
    [SerializeField] TextMeshProUGUI charInfoSkillName;
    [SerializeField] TextMeshProUGUI charInfoSkillType;
    [SerializeField] TextMeshProUGUI charInfoSkillDescription;
    GameObject tower;

    CharacterData characterData;


    //LoadData
    IDataService dataService = new JsonDataService();
    string saveDataPath = "/character-data"; //TODO static reference
    private CharacterStats characterStats = new CharacterStats();

    float maxHp;
    float baseArmor;
    float damageValue;
    int currentLevel;


    //TimeScale Properties
    float placingGameTimeSpeed = 0.1f;
    float normalGameTimeSpeed = 1f;

    public static bool _isPlacing;

    float maxDistance;

    private enum State
    {
        Default,
        Placing,
        HoldPlacing,
        Deleting,
        CharInfo
    }

    private State currentState;
    SoundsPlayTrack soundsPlayTrack;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _touchHoldAction = playerInput.actions["Hold"];
        _touchPressAction = playerInput.actions["TouchPress"];
        coinsManager = FindObjectOfType<CoinsManager>();
        currentState = State.Default;

        towerHolder = GameObject.FindGameObjectsWithTag("TowerHolder");
        _rangeIndicatorObj = Instantiate(rangeIndicator, gameObject.transform.position, Quaternion.identity);
        soundsPlayTrack = GetComponent<SoundsPlayTrack>();
    }

    GameObject _towerHolder;

    private GameObject _charObj;

    [SerializeField] GameObject rangeIndicator;

    private GameObject _rangeIndicatorObj;
    private GameObject _instPreviewObj;
    private GameObject _instObj;
    float heightRange = 10f;

    private int _charCost;




    private void Update()
    {

        Debug.Log(currentState);
        switch (currentState)
        {
            case State.Default:
                normalCam.Priority = 1;
                placingCam.Priority = 0;
                if (_towerHolder != null)
                {
                    EnableTowerHolder(_towerHolder.gameObject.GetComponent<TowerHolder>());
                }
                _charInfoCanvas.SetActive(false);
                _isPlacing = false;
                break;
            case State.Placing:
                PlacingTime();
                _charInfoCanvas.SetActive(true);

                if (_towerHolder != null)
                {
                    DisableTowerHolder(_towerHolder.gameObject.GetComponent<TowerHolder>());
                }
                normalCam.Priority = 0;
                placingCam.Priority = 1;
                break;
            case State.HoldPlacing:
                PlacingTime();
                break;
            case State.Deleting:
                _isPlacing = true;

                Debug.Log("Is Deleting");
                PlacingTime();
                normalCam.Priority = 0;
                placingCam.Priority = 1;
                break;
            case State.CharInfo:
                _isPlacing = true;

                PlacingTime();
                normalCam.Priority = 0;
                placingCam.Priority = 1;
                break;
        }
    }

    public void OnHolding(InputAction.CallbackContext context)
    {
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

    public void StartPlacing(GameObject _previewGameObject, GameObject _charObj, int cost, LayerMask layerMask, String platform, GameObject _towerHolder, CharacterData characterData)
    {
        this.characterData = characterData;
        this._charObj = _charObj;
        _layerPlatform = layerMask;
        _platformTag = platform;
        this._towerHolder = _towerHolder;
        _platFormsReference = GameObject.FindGameObjectsWithTag(_platformTag);

        if (currentState == State.HoldPlacing || currentState == State.Placing)
        {
            UnsubscribeAction();
            DisablePlatformCanvas();
            Destroy(_instPreviewObj);
            _CharConfirmPlacementCanvas.SetActive(false);
            _rangeIndicatorObj.SetActive(false);
            currentState = State.Default;
        }

        else if (currentState == State.Default)
        {
            EnablePlatformCanvas();

            _isPlacing = true;
            _touchPressAction.performed += OnHolding;
            _touchPressAction.canceled += EndTest;
            _instPreviewObj = Instantiate(_previewGameObject, transform.position, Quaternion.identity);
            _rangeIndicatorObj.transform.localScale = new Vector3(characterData.range, heightRange, characterData.range);
            _rangeIndicatorObj.transform.position = _instPreviewObj.transform.position;
            _rangeIndicatorObj.SetActive(true);
            currentState = State.Placing;
            //CharInfo
            LoadCharStats();

            charInfoArtWork.sprite = characterData.charArtWork;
            charInfoTypeImage.sprite = characterData.charTypeArt;
            charInfoName.text = characterData.charName;
            charInfoHp.text = maxHp.ToString("f0");
            charInfoAttack.text = damageValue.ToString("f0");
            charInfoArmor.text = baseArmor.ToString("f0");
            charInfoLevel.text = currentLevel.ToString("f0");
            charInfoSkillImage.sprite = characterData.skillData.skillArtWork;
            charInfoSkillType.text = characterData.skillData.skillType;
            charInfoSkillName.text = characterData.skillData.skillName;
            charInfoSkillDescription.text = characterData.skillData.skillDescription;

        }

        _charCost = cost;

    }

    public void LoadCharStats()
    {
        string newSaveDataPath = $"{saveDataPath}-{characterData.charName}.json";

        string path = Application.persistentDataPath + newSaveDataPath;

        if (!File.Exists(path))
        {
            if (!characterStats.stats.ContainsKey(characterData.charName))
            {
                CharacterStats.charStats characterStatsData = new CharacterStats.charStats
                {
                    charName = characterData.charName,
                    level = characterData.charLevel,
                    experienceToNextLevel = 50,
                    hp = characterData.maxHp,
                    damage = characterData.dmgValue,
                    armor = characterData.baseArmor
                };
                characterStats.stats.Clear();
                characterStats.stats.Add(characterData.charName, characterStatsData);
                dataService.SaveData(newSaveDataPath, characterStats, false);
            }
            else
            {
                return;
            }

        }


        CharacterStats charStatsData = dataService.LoadData<CharacterStats>(newSaveDataPath, false);

        try
        {
            CharacterStats.charStats charData = charStatsData.stats[characterData.charName];

            maxHp = charData.hp;
            baseArmor = charData.armor;
            damageValue = charData.damage;
            currentLevel = charData.level;

        }
        catch (Exception e)
        {
            Debug.Log(e.Message + " " + e.StackTrace);

        }

    }

    public void ConfirmPlace()
    {

        if (currentState == State.HoldPlacing)
        {
            canvasEnabler.isPlaceable = false;
            DisablePlatformCanvas();
            UnsubscribeAction();
            TowerHolderDisabler();//TODO: Make towerHolder disappear
            coinsManager.MinusCoin(_charCost);
            _CharConfirmPlacementCanvas.SetActive(false);
            Destroy(_instPreviewObj);
            _rangeIndicatorObj.SetActive(false);
            _instObj = Instantiate(_charObj, _instPreviewObj.transform.position, Quaternion.identity);
            soundsPlayTrack.Play("PlacedSFX");
            currentState = State.Default;
        }
    }

    public void CancelPlacement()
    {
        DisablePlatformCanvas();
        _rangeIndicatorObj.SetActive(false);
        _CharConfirmPlacementCanvas.SetActive(false);
        _charDeleteCanvas.SetActive(false);
        Destroy(_instPreviewObj);
        UnsubscribeAction();
        currentState = State.Default;
    }

    public void Placing(InputAction.CallbackContext context)
    {


        if (_instPreviewObj != null)
        {
            _position = _mainCam.ScreenPointToRay(Touchscreen.current.position.ReadValue());
            rayCastHit = Physics.Raycast(_position, out RaycastHit raycastHit, maxDistance = Mathf.Infinity, _layerPlatform);



            if (rayCastHit && _isPlacing)
            {
                canvasEnabler = raycastHit.collider.GetComponent<CanvasEnabler>();
                if (canvasEnabler.isPlaceable)
                {
                    _instPreviewObj.transform.position = raycastHit.transform.position;
                    Debug.DrawRay(_position.origin, _position.direction * 20, Color.red);
                    _CharConfirmPlacementCanvas.SetActive(true);
                    _CharConfirmPlacementCanvas.transform.position = _instPreviewObj.transform.position;
                    _rangeIndicatorObj.transform.position = _instPreviewObj.transform.position;

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
        TowerHolder towerTesting = _towerHolder.GetComponent<TowerHolder>();
        towerTesting._placed = true;
        towerTesting.cooldownFinished = false;

    }
    #endregion

    #region 
    void DisableTowerHolder(TowerHolder butButton)
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

    void EnableTowerHolder(TowerHolder butButton)
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
    #endregion

    #region Enabling Canvas


    void EnablePlatformCanvas()
    {
        foreach (GameObject platform in _platFormsReference)
        {
            CanvasEnabler enabler = platform.GetComponent<CanvasEnabler>();
            enabler.EnableCanvas();
        }
    }

    void DisablePlatformCanvas()
    {
        foreach (GameObject platform in _platFormsReference)
        {
            CanvasEnabler enabler = platform.GetComponent<CanvasEnabler>();
            enabler.DisableCanvas();
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

    //
    public void DeleteState(GameObject tower, CharacterData charData, float dmg, float armor, float hp)
    {
        if (currentState == State.Default)
        {
            this.tower = tower;

            _rangeIndicatorObj.transform.localScale = new Vector3(charData.range, heightRange, charData.range);
            _rangeIndicatorObj.transform.position = tower.transform.position;
            _charInfoCanvas.SetActive(true);
            _charDeleteCanvas.SetActive(true);
            _rangeIndicatorObj.SetActive(true);
            _charDeleteCanvas.transform.position = tower.transform.position;
            float yOffset = 5f;
            Vector3 newPosition = _charDeleteCanvas.transform.position;
            newPosition.y += yOffset;
            _charDeleteCanvas.transform.position = newPosition;
            currentState = State.Deleting;
            Debug.Log("StateingDelete");

            //charInfoCanvas
            charInfoArtWork.sprite = charData.charArtWork;
            charInfoTypeImage.sprite = charData.charTypeArt;
            charInfoName.text = charData.charName;
            charInfoHp.text = hp.ToString("f0");
            charInfoAttack.text = dmg.ToString("f0");
            charInfoArmor.text = armor.ToString("f0");
            charInfoLevel.text = charData.charLevel.ToString("f0");
            charInfoSkillImage.sprite = charData.skillData.skillArtWork;
            charInfoSkillType.text = charData.skillData.skillType;
            charInfoSkillName.text = charData.skillData.skillName;
            charInfoSkillDescription.text = charData.skillData.skillDescription;
            return;
        }
    }

    public void DeleteChar()
    {
        _charDeleteCanvas.SetActive(false);
        currentState = State.Default;
        Debug.Log("Deleting");
        IDeletable deletable = tower.GetComponent<IDeletable>();
        _rangeIndicatorObj.SetActive(false);

        deletable.DeleteChar();
    }

    public void SpecialCharacterClick(GameObject tower, CharacterData data, float dmg, float armor, float hp, int currentLevel)
    {
        this.tower = tower;
        if (currentState == State.Default)
        {
            currentState = State.CharInfo;
            _rangeIndicatorObj.transform.localScale = new Vector3(data.range, heightRange, data.range);
            _rangeIndicatorObj.transform.position = tower.transform.position;
            _rangeIndicatorObj.SetActive(true);
            _charInfoCanvas.SetActive(true);

            charInfoArtWork.sprite = data.charArtWork;
            charInfoTypeImage.sprite = data.charTypeArt;
            charInfoName.text = data.charName;
            charInfoHp.text = hp.ToString("f0");
            charInfoAttack.text = dmg.ToString("f0");
            charInfoArmor.text = armor.ToString("f0");
            charInfoLevel.text = currentLevel.ToString("f0");
            charInfoSkillImage.sprite = data.skillData.skillArtWork;
            charInfoSkillType.text = data.skillData.skillType;
            charInfoSkillName.text = data.skillData.skillName;
            charInfoSkillDescription.text = data.skillData.skillDescription;
            return;
        }
        _rangeIndicatorObj.SetActive(false);

        currentState = State.Default;
    }
}
