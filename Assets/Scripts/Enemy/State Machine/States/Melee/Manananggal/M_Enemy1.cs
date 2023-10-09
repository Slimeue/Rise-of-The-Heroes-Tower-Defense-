using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Enemy1 : MeleeEnemyEntity, IDamageable
{


    public M_Enemy1_S_IdleState idleState { get; private set; }
    public M_Enemy1_S_MovingState movingState { get; private set; }
    public M_Enemy1_S_AttackState attackState { get; private set; }
    public M_Enemy1_S_DeathState deathState { get; private set; }


    public override void Awake()
    {
        base.Awake();

        idleState = new M_Enemy1_S_IdleState(stateMachine, "Idle", this, this);
        movingState = new M_Enemy1_S_MovingState(stateMachine, "walk", this, this);
        attackState = new M_Enemy1_S_AttackState(stateMachine, "attack", this, this);
        deathState = new M_Enemy1_S_DeathState(stateMachine, "death", this, this);
        animationHandler = GetComponent<AnimationHandler>();
        anim = GetComponent<Animator>();

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

    private void OnTriggerEnter(Collider other)
    {
        baseState = stateMachine.currentState;
        baseState.OnTriggerEnter(stateMachine, other);
    }

    private void OnTriggerExit(Collider collider)
    {
        baseState = stateMachine.currentState;
        baseState.OnTriggerExit(stateMachine, collider);
    }

    public void OnDisable()
    {
        animationHandler.OnDeathFinish -= DestroyGameObject;
    }


    #region METHODS


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 dir = transform.forward * enemiesData.attackRange;
        Gizmos.DrawRay(transform.position, dir);
    }

    public override void HealthBarTracker()
    {
        base.HealthBarTracker();
    }

    public void Damage(float damageAmount)
    {
        float totalDamage;

        totalDamage = damageAmount * (100 / (100 + baseArmor));
        Debug.Log(totalDamage);
        currentHealth -= totalDamage;
    }

    public void DestroyGameObject()
    {
        gameObject.SetActive(false);
    }

    #endregion

}
