using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class TowerTesting : MonoBehaviour
{

    TowerManager towerManager;
    CoinsManager coinsManager;
    [SerializeField] public CharacterData characterData;
    [SerializeField] GameObject _preview;
    [SerializeField] GameObject _charObj;

    [SerializeField] GameObject[] _characterHolder;

    [SerializeField] public bool _placed;


    Image _image;
    [SerializeField] private Color _unavailColor;
    [SerializeField] private Color _availColor;

    int _characterCost;

    LayerMask layerMask;

    private void Awake()
    {
        towerManager = FindObjectOfType<TowerManager>();
        coinsManager = FindObjectOfType<CoinsManager>();
        _image = GetComponent<Image>();
        layerMask = characterData._layerMask;
        _characterHolder = GameObject.FindGameObjectsWithTag("TowerHolder");
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
    }

    public void Pressed()
    {

        if (!_placed)
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
        if (coinsManager._CurrentCoin < _characterCost || _placed)
        {
            _image.color = _unavailColor;
        }
        else
        {
            _image.color = _availColor;
        }
    }



}
