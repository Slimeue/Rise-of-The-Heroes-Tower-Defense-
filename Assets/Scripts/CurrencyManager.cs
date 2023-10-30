using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    IDataService dataService = new JsonDataService();
    DataPathClass dataPathClass;
    CurrencyModel currencyModel = new CurrencyModel();
    bool EncryptionEnabled;


    public float currentCurrency = 0;
    public float rewardValue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        dataPathClass = new DataPathClass();



    }

    private void Start()
    {
        LoadCurrency();
        currencyModel.coins = currentCurrency;
        dataService.SaveData(dataPathClass.coinPath, currencyModel, EncryptionEnabled);

    }



    public float GetCoin(float currency)
    {
        //TODO when get save it
        rewardValue = currency;
        currentCurrency += rewardValue;
        currencyModel.coins = currentCurrency;
        dataService.SaveData(dataPathClass.coinPath, currencyModel, EncryptionEnabled);
        return currency;
    }

    public void LoadCurrency()
    {
        string path = Application.persistentDataPath + dataPathClass.coinPath;
        if (File.Exists(path))
        {
            CurrencyModel data = dataService.LoadData<CurrencyModel>(dataPathClass.coinPath, EncryptionEnabled);
            currentCurrency = data.coins;
        }

    }


}
