using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseManager : MonoBehaviour
{
    [SerializeField] public float maxBaseHp;
    public float currentBaseHp;
    [SerializeField] Slider baseHpBar;

    Vector3 initialPosition;
    [SerializeField] float shakeMagnitude = 1f;
    [SerializeField] float shakeDuration;

    private void Awake()
    {
        currentBaseHp = maxBaseHp;
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeDuration = 0;
            transform.localPosition = initialPosition;
        }
    }

    void HealthBarTracker()
    {

    }



    public void ShakeEffect()
    {
        initialPosition = transform.localPosition;
        shakeDuration = 0.5f;
    }
}
