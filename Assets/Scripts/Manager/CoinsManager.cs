using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{

    [HideInInspector]
    [SerializeField] float _startingCoin;

    [SerializeField] float _currentCoin;
    public float _CurrentCoin { get { return _currentCoin; } }
    [SerializeField] TextMeshProUGUI _coins;


    private void Awake()
    {
        _currentCoin = _startingCoin;
    }

    private void Update()
    {

        IncrementCoin();
        UpdateCoinDisplay();
    }

    void UpdateCoinDisplay()
    {
        _coins.text = _currentCoin.ToString();
    }

    void IncrementCoin()
    {
        if (_startingCoin <= 99)
        {
            _startingCoin += Time.deltaTime;
            _currentCoin = Mathf.Round(_startingCoin);
        }

    }

    public void MinusCoin(int amount)
    {
        Debug.Log("Succesfuly Decrease Coin: " + amount);
        _startingCoin -= Mathf.Abs(amount); // dont know why but starting coin is the one we're decreasing. :>
    }

}
