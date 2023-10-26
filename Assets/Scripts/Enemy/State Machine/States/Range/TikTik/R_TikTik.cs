using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_TikTik : RangeEnemyEntity, IDamageable, IEnemyDataGetable
{
    public R_TikTik_S_attackState attackState { get; private set; }
    public R_TikTik_S_deathState deathState { get; private set; }
    public R_TikTik_S_moveState movingState { get; private set; }
    public R_TikTik_S_idleState idleState { get; private set; }

    const string CREATURE_IDLE = "idle";
    const string CREATURE_ATTACK = "attack";
    const string CREATURE_DEATH = "death";
    const string CREATURE_MOVE = "walk";

    public override void Awake()
    {
        base.Awake();

        attackState = new R_TikTik_S_attackState(stateMachine, CREATURE_ATTACK, this, this);
        idleState = new R_TikTik_S_idleState(stateMachine, CREATURE_IDLE, this, this);
        deathState = new R_TikTik_S_deathState();
        movingState = new R_TikTik_S_moveState(stateMachine, CREATURE_MOVE, this, this);
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
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Damage(float damageAmount)
    {
        float totalDamage;

        totalDamage = damageAmount * (100 / (100 + baseArmor));
        currentHealth -= totalDamage;
    }

    EnemiesData IEnemyDataGetable.GetEnemyData()
    {
        return enemiesData;
    }
    #endregion


}
