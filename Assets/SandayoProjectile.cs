using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandayoProjectile : MonoBehaviour
{
    public Transform target;
    public GameObject skillObj;

    public float appliedSkillCounter;
    public float skillDuration = 5f;

    public float damageMultiplier;
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
        float yOffset = 10f;
        IDamageable damageable = target.GetComponentInParent<IDamageable>();
        IDebuffable debuffable = target.GetComponentInParent<IDebuffable>();
        IRangeSoundable rangeSoundable = target.GetComponentInParent<IRangeSoundable>();


        if (debuffable != null)
        {
            if (!debuffable.IsDebuff())
            {
                damageMultiplier = 0.5f;
                debuffable.IncreaseDamageTaken(damageMultiplier, skillDuration);
                ApplySkillOrb(yOffset);
            }


        }


        if (damageable != null)
        {
            rangeSoundable.PlayRangeHitSFX("RangeHit");
            damageable.Damage(dmgValue);
            Destroy(gameObject);
        }
        Destroy(gameObject);

    }

    private void ApplySkillOrb(float yOffset)
    {
        Vector3 targetPosition = target.position;
        targetPosition.y += yOffset;
        GameObject skillOb = Instantiate(skillObj, targetPosition, Quaternion.identity);
        skillOb.transform.SetParent(target);
        OrbOrbit orbOrbit = skillOb.GetComponent<OrbOrbit>();
        orbOrbit.center = target;
        orbOrbit.duration = skillDuration;
    }

    public void SeekTarget(Transform target, CharacterData characterData, float amountDamage)
    {
        this.target = target;
        speed = characterData.projectileSpeed;
        dmgValue = amountDamage;
    }


}
