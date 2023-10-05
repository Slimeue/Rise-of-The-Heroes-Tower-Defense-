using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
using TMPro;

public class TowerHolder : MonoBehaviour
{

    TowerManager towerManager;
    CoinsManager coinsManager;

    [SerializeField] public CharacterData characterData;
    //Gameobjects
    GameObject _preview;
    GameObject _charObj;

    [HideInInspector]
    [SerializeField] GameObject[] _characterHolder;
    [HideInInspector]
    [SerializeField] public bool _placed;

    [HideInInspector]
    public float _charCooldown;
    float _charCooldownNormalized;


    [HideInInspector]
    public bool cooldownFinished;
    [HideInInspector]
    public bool isDead;

    [Space(5)]
    [Header("UI Components")]
    public GameObject cooldownSlider;
    Image _image;

    [SerializeField] public Image _charArtWork;
    [SerializeField] public Color _unavailColor;
    [SerializeField] public Color _availColor;
    [SerializeField] public TextMeshProUGUI cooldownText;
    [SerializeField] public TextMeshProUGUI costText;
    [SerializeField] public Image cooldownBGImg;

    int _characterCost;

    LayerMask layerMask;

    private void Awake()
    {

        towerManager = FindObjectOfType<TowerManager>();
        coinsManager = FindObjectOfType<CoinsManager>();
        _image = GetComponent<Image>();

        _characterHolder = GameObject.FindGameObjectsWithTag("TowerHolder");
        cooldownFinished = true;

        //Data Setters
        layerMask = characterData._layerMask;
        costText.text = characterData.towerCost.ToString();
        _charArtWork.sprite = characterData.charArtWork;
        _preview = characterData.previewChar;
        _charObj = characterData.charObject;


    }

    private void Start()
    {

        _unavailColor.a = 1;
        _availColor.a = 1;
        _characterCost = characterData.towerCost;

    }

    private void Update()
    {
        Unavailable();
        CheckDead();
    }

    public void Pressed()
    {

        if (!_placed || cooldownFinished)
        {
            StartPlace();
        }
        else
        {
            return;
        }

    }

    private void StartPlace()
    {
        if (coinsManager._CurrentCoin >= _characterCost)
        {
            towerManager.StartPlacing(_preview, _charObj, _characterCost, layerMask, characterData._platformTag, gameObject);

        }
    }

    public void Unavailable()
    {
        if (coinsManager._CurrentCoin < _characterCost || _placed || !cooldownFinished)
        {
            _image.color = _unavailColor;
        }
        else
        {
            _image.color = _availColor;
        }
    }

    void CheckDead()
    {
        if (!isDead)
        {
            return;
        }

        StartCooldown();
    }

    private void StartCooldown()
    {
        cooldownSlider.SetActive(true);
        Slider slider = cooldownSlider.GetComponent<Slider>();
        if (_charCooldown > 0)
        {

            _charCooldown -= Time.deltaTime;
            _charCooldownNormalized = (_charCooldown / characterData.towerCooldown);
            slider.value = 1.0f - _charCooldownNormalized;
            UpdateCooldownText();
        }
        else
        {
            cooldownFinished = true;
            cooldownSlider.SetActive(false);
            cooldownText.gameObject.SetActive(false);
            cooldownBGImg.gameObject.SetActive(false);
        }
    }

    void UpdateCooldownText()
    {
        cooldownText.gameObject.SetActive(true);
        cooldownBGImg.gameObject.SetActive(true);
        cooldownText.text = _charCooldown.ToString("F1");
    }

}
