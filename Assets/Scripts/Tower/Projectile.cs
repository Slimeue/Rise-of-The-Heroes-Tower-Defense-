using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Transform target;

    public float speed;
    float dmgValue;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        IDamageable damageable = target.GetComponentInParent<IDamageable>();

        if (damageable != null)
        {
            damageable.Damage(dmgValue);
            Destroy(gameObject);
        }
        Destroy(gameObject);

    }

    public void SeekTarget(Transform target, CharacterData characterData, float amountDamage)
    {
        this.target = target;
        speed = characterData.projectileSpeed;
        dmgValue = amountDamage;
    }


}
