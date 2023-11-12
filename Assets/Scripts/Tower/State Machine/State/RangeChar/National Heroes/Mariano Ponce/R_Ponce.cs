using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class R_Ponce : RangeCharacterEntity, IDamageable, ISkillable, IBuffable, IPointerClickHandler
{


    public GameObject skillHolder;
    public float skillCd;
    public float skillDuration;
    public bool skillFinished;

    BaseManager baseManager;

    public R_Ponce_State_attackState attackState { get; private set; }
    public R_Ponce_State_idleState idleState { get; private set; }
    public R_Ponce_State_deathState deathState { get; private set; }
    public R_Ponce_State_recoveryState recoveryState { get; private set; }
    public R_Ponce_State_skillState skillState { get; private set; }

    const string HERO_IDLE = "idle";
    const string HERO_ATTACK = "attack";
    const string HERO_RECOVERY = "recovery";
    const string HERO_SKILL = "skill";
    const string HERO_DEATH = "death";

    public GameObject[] activeTowers;


    public override void Awake()
    {
        base.Awake();
        skillCd = characterData.skillCooldown;
        attackState = new R_Ponce_State_attackState(characterStateMachine, HERO_ATTACK, this, this);
        idleState = new R_Ponce_State_idleState(characterStateMachine, HERO_IDLE, this, this);
        deathState = new R_Ponce_State_deathState(characterStateMachine, HERO_DEATH, this, this);
        recoveryState = new R_Ponce_State_recoveryState(characterStateMachine, HERO_RECOVERY, this, this);
        skillState = new R_Ponce_State_skillState(characterStateMachine, HERO_SKILL, this, this);
        baseManager = FindObjectOfType<BaseManager>();
        anim = GetComponent<Animator>();
        animationHandler = GetComponent<AnimationHandler>();
        healthBar = baseManager.specialCharHpBar;
        baseManager.baseCharIcon.sprite = characterData.charArtWork;

    }

    public override void Update()
    {
        base.Update();

        activeTowers = GameObject.FindGameObjectsWithTag("Player");

        radius = characterData.range;
        HealthBarTracker();

        //skillcd
        if (skillFinished)
        {
            skillCd -= Time.deltaTime;
            if (skillCd <= 0f)
            {
                skillFinished = false;
                skillCd = characterData.skillCooldown;
            }
        }

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
            _projectile.SeekTarget(target, characterData, damageValue);
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

    public void Slowed(float slowAmount, float time) { }

    public void Skill()
    {
        if (!skillFinished && characterStateMachine.currentState != recoveryState)
        {
            Debug.Log(gameObject.name + " Skill Click");
            characterStateMachine.ChangeState(skillState);
            skillFinished = true;
        }
    }

    public void AttackSpeedBuff(float percentage, float duration)
    {
        throw new System.NotImplementedException();
    }

    public void AttackBuff(float percentage, float duration)
    {
        damageValue *= percentage;

        isBuffed = true;
        buffDuration = duration;

    }

    public CharacterData SkillData()
    {
        return characterData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        towerManager.SpecialCharacterClick(gameObject, characterData, damageValue, baseArmor, currentHealth);
    }

    #endregion


}
