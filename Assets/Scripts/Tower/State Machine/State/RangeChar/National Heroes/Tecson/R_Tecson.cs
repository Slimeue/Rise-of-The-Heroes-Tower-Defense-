using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Tecson : RangeCharacterEntity, IDamageable
{

    BaseManager baseManager;

    public R_Tecson_State_attackState attackState { get; private set; }
    public R_Tecson_State_idleState idleState { get; private set; }
    public R_Tecson_State_deathState deathState { get; private set; }
    public R_Tecson_State_recoveryState recoveryState { get; private set; }
    public R_Tecson_State_skillState skillState { get; private set; }

    const string HERO_IDLE = "idle";
    const string HERO_ATTACK = "attack";
    const string HERO_RECOVERY = "recovery";
    const string HERO_SKILL = "skill";
    const string HERO_DEATH = "death";

    public override void Awake()
    {
        base.Awake();

        attackState = new R_Tecson_State_attackState(characterStateMachine, HERO_ATTACK, this, this);
        idleState = new R_Tecson_State_idleState(characterStateMachine, HERO_IDLE, this, this);
        deathState = new R_Tecson_State_deathState(characterStateMachine, HERO_DEATH, this, this);
        recoveryState = new R_Tecson_State_recoveryState(characterStateMachine, HERO_RECOVERY, this, this);
        skillState = new R_Tecson_State_skillState(characterStateMachine, HERO_SKILL, this, this);
        baseManager = FindObjectOfType<BaseManager>();
        anim = GetComponent<Animator>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    public override void Update()
    {
        base.Update();
        radius = characterData.range;
        HealthBarTracker();
    }

    private void Start()
    {
        characterStateMachine.Initialize(idleState);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider collider)
    {
        characterBaseState = characterStateMachine.currentState;
        characterBaseState.OnTriggerEnter(collider);
    }

    private void OnTriggerExit(Collider other)
    {
        characterBaseState = characterStateMachine.currentState;
        characterBaseState.OnTriggerExit(other);
    }

    #region METHODS

    public void Fire()
    {
        GameObject projectileGO = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Projectile _projectile = projectileGO.GetComponent<Projectile>();

        if (_projectile != null)
        {
            _projectile.SeekTarget(target, characterData);
        }
    }

    public void Damage(float damageAmount)
    {
        float totalDamage;
        totalDamage = damageAmount * (100 / (100 + baseArmor));


        if (characterStateMachine.currentState == recoveryState || characterStateMachine.currentState == deathState)
        {
            baseManager.ShakeEffect();
            baseManager.currentBaseHp -= damageAmount;
            return;
        }

        currentHealth -= totalDamage;
    }

    public void ToRecovery()
    {
        characterStateMachine.ChangeState(recoveryState);
    }

    #endregion

}