using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManggobAbility : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable.Slowed(2f, 10f);

        }
    }


}
