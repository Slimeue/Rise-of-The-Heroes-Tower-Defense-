using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class TowerTesting : MonoBehaviour
{

    TowerManager towerManager;
    [SerializeField] GameObject _preview;
    [SerializeField] GameObject _charObj;

    private void Awake()
    {
        towerManager = FindAnyObjectByType<TowerManager>();
    }

    public void Pressed()
    {
        towerManager.StartPlacing(_preview, _charObj);
    }

}
