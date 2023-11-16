using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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
        IRangeSoundable rangeSoundable = target.GetComponentInParent<IRangeSoundable>();
        if (damageable != null)
        {
            rangeSoundable.PlayRangeHitSFX("RangeHit");

            damageable.Damage(dmgValue);
            Destroy(gameObject);
        }
        Destroy(gameObject);

    }

    public void EnemySeekTarget(Transform target, EnemiesData enemiesData)
    {
        this.target = target;
        speed = enemiesData.projectileSpeed;
        dmgValue = enemiesData.dmgValue;
    }

}
