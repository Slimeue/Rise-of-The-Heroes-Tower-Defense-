using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] public float maxBaseHp;
    public float currentBaseHp;

    private void Awake()
    {
        currentBaseHp = maxBaseHp;
    }
}
