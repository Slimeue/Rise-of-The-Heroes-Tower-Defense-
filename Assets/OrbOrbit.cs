using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbOrbit : MonoBehaviour
{
    public Transform center;
    public float duration;
    public float _rotationSpeed;



    private void Update()
    {
        Orbit();
        duration -= Time.deltaTime;
        if (duration <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void Orbit()
    {
        transform.RotateAround(center.position, Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}

