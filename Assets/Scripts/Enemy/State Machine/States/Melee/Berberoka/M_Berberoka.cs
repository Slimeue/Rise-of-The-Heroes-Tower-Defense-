using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Berberoka : MeleeEnemyEntity, IDamageable, IEnemyDataGetable, IDebuffable, IRangeSoundable
{

    public M_Berberoka_S_AttackState attackState { get; private set; }
    public M_Berberoka_S_DeathState deathState { get; private set; }
    public M_Berberoka_S_MovingState movingState { get; private set; }
    public M_Berberoka_S_IdleState idleState { get; private set; }


    const string CREATURE_IDLE = "idle";
    const string CREATURE_ATTACK = "attack";
    const string CREATURE_DEATH = "death";
    const string CREATURE_MOVE = "walk";

    public override void Awake()
    {
        base.Awake();

        attackState = new M_Berberoka_S_AttackState(stateMachine, CREATURE_ATTACK, this, this);
        deathState = new M_Berberoka_S_DeathState(stateMachine, CREATURE_DEATH, this, this);
        movingState = new M_Berberoka_S_MovingState(stateMachine, CREATURE_MOVE, this, this);
        idleState = new M_Berberoka_S_IdleState(stateMachine, CREATURE_IDLE, this, this);
        animationHandler = GetComponent<AnimationHandler>();
        anim = GetComponent<Animator>();
        currentHealth = enemiesData.maxHp;
    }

    public override void Update()
    {
        base.Update();
        HealthBarTracker();

        //slowed
        SlowedChecked();

    }



    public override void OnEnable()
    {
        base.OnEnable();
        slowed = false;
        stateMachine.Initialize(idleState);
        currentHealth = enemiesData.maxHp;
    }

    public void OnDisable()
    {
        animationHandler.OnDeathFinish -= DestroyGameObject;
    }

    public override void HealthBarTracker()
    {
        base.HealthBarTracker();
    }

    public void OnTriggerEnter(Collider other)
    {
        baseState = stateMachine.currentState;
        baseState.OnTriggerEnter(stateMachine, other);
    }

    private void OnTriggerExit(Collider collider)
    {
        baseState = stateMachine.currentState;
        baseState.OnTriggerExit(stateMachine, collider);
    }


    #region 

    public void DestroyGameObject()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 dir = transform.forward * enemiesData.attackRange;
        Gizmos.DrawRay(transform.position, dir);
    }

    public bool IsDebuff()
    {
        return isDamageTakenIncrease;
    }

    public void IncreaseDamageTaken(float damageMultiplier, float duration)
    {
        isDamageTakenIncrease = true;
        this.damageMultiplier = damageMultiplier;
        debuffDuration = duration;
        Debug.Log("INCREASEDAMAGETAKEN");
    }

    public void Damage(float damageAmount)
    {
        if (!isDamageTakenIncrease)
        {
            float totalDamage;
            totalDamage = damageAmount * (100 / (100 + baseArmor));
            Debug.Log("No Debuff" + totalDamage);
            currentHealth -= totalDamage;
        }
        else
        {
            float damage;
            float damageToAdd;
            float newDamage;
            damage = damageAmount * (100 / (100 + baseArmor));
            damageToAdd = damage * damageMultiplier;
            newDamage = damageToAdd + damage;
            Debug.Log("With Debuff " + newDamage);
            currentHealth -= newDamage;
        }


    }


    EnemiesData IEnemyDataGetable.GetEnemyData()
    {
        return enemiesData;
    }

    public void Slowed(float slowAmount, float time)
    {

        if (!slowed)
        {
            speed /= slowAmount;
            anim.SetFloat("Speed", 0.5f);
        }


        timeSlow = time;

        slowed = true;
    }

    private void SlowedChecked()
    {
        if (slowed)
        {
            Debug.Log(anim.speed);
            timeSlow -= Time.deltaTime;
            if (timeSlow <= 0)
            {
                slowed = false;
                timeSlow = 5f;
                anim.SetFloat("Speed", 1f);
                anim.speed = 1f;
                speed = enemiesData.moveSpeed;
            }
        }
    }

    public void PlayRangeHitSFX(string sfx)
    {
        soundsPlayTrack.Play(sfx);
    }




    #endregion



}
