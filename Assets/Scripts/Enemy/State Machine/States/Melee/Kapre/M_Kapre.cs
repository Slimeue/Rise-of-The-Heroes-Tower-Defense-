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
        baseState = stateMachine.currentState;

    }

    private void OnEnable()
    {
        stateMachine.Initialize(idleState);
        currentHealth = enemiesData.maxHp;
    }

    private void OnDisable()
    {
        animationHandler.OnDeathFinish -= DestroyGameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        baseState = stateMachine.currentState;
        baseState.OnTriggerEnter(stateMachine, other);
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
        currentHealth -= damageAmount;
    }

    #endregion



}

