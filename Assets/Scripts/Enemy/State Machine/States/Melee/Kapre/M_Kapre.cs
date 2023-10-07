using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Kapre : MeleeEnemyEntity, IDamageable
{
    public M_Kapre_S_AttackState attackState { get; private set; }
    public M_Kapre_S_DeathState deathState { get; private set; }
    public M_Kapre_S_MovingState movingState { get; private set; }
    public M_Kapre_S_IdleState idleState { get; private set; }


    const string CREATURE_IDLE = "idle";
    const string CREATURE_ATTACK = "attack";
    const string CREATURE_DEATH = "death";
    const string CREATURE_MOVE = "walk";

    public override void Awake()
    {
        base.Awake();

        attackState = new M_Kapre_S_AttackState(stateMachine, CREATURE_ATTACK, this, this);
        deathState = new M_Kapre_S_DeathState(stateMachine, CREATURE_DEATH, this, this);
        movingState = new M_Kapre_S_MovingState(stateMachine, CREATURE_MOVE, this, this);
        idleState = new M_Kapre_S_IdleState(stateMachine, CREATURE_IDLE, this, this);
        animationHandler = GetComponent<AnimationHandler>();
        anim = GetComponent<Animator>();
        currentHealth = enemiesData.maxHp;
    }

    public override void Update()
    {
        base.Update();
        HealthBarTracker();
    }

    public override void OnEnable()
    {
        base.OnEnable();
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
        Debug.Log(other.name);
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

    public void Damage(float damageAmount)
    {
        float totalDamage;

        totalDamage = damageAmount * (100 / (100 + baseArmor));
        Debug.Log(totalDamage);
        currentHealth -= totalDamage;
    }

    #endregion



}

