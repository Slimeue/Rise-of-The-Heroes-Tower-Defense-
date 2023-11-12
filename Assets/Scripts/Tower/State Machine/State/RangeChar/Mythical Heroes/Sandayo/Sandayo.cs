using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sandayo : RangeCharacterEntity, IDamageable, IBuffable, IDeletable, IPointerClickHandler
{

    public GameObject abilityHolder;
    public float skillCd;
    public bool skillFinished;

    public R_Sandayo_attack attackState { get; private set; }
    public R_Sandayo_idle idleState { get; private set; }
    public R_Sandayo_death deathState { get; private set; }
    public R_Sandayo_skill skillState { get; private set; }



    const string TOWER_IDLE = "idle";
    const string TOWER_ATTACK = "attack";
    const string TOWER_DEATH = "death";
    const string TOWER_SKILL = "skill";

    public override void Awake()
    {
        base.Awake();
        skillCd = characterData.skillCooldown;
        attackState = new R_Sandayo_attack(characterStateMachine, TOWER_ATTACK, this, this);
        idleState = new R_Sandayo_idle(characterStateMachine, TOWER_IDLE, this, this);
        deathState = new R_Sandayo_death(characterStateMachine, TOWER_DEATH, this, this);
        skillState = new R_Sandayo_skill(characterStateMachine, TOWER_SKILL, this, this);
        anim = GetComponent<Animator>();
        animationHandler = GetComponent<AnimationHandler>();
        RefreshChar();
    }

    public override void Update()
    {
        base.Update();
        radius = characterData.range;
        HealthBarTracker();
        //skillCd
        if (skillFinished)
        {
            //startCooldown
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
        isDead = false;
        ground = towerManager.canvasEnabler.gameObject;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnDisable()
    {
        animationHandler.OnDeathFinish -= DestroyGameObject;
        animationHandler.OnDeathFinish -= TowerHolderEnabler;
        rangeCharAnimationHandler.OnRangeStartAttack -= Fire;
    }

    #region Methods


    public void Fire()
    {
        GameObject projectileGO = Instantiate(projectile, firePoint.position, firePoint.rotation);
        SandayoProjectile _projectile = projectileGO.GetComponent<SandayoProjectile>();

        if (_projectile != null)
        {
            _projectile.SeekTarget(target, characterData, damageValue);
        }
    }

    public override void HealthBarTracker()
    {
        base.HealthBarTracker();
    }



    public void Damage(float damageAmount)
    {
        float totalDamage;

        totalDamage = damageAmount * (100 / (100 + baseArmor));
        currentHealth -= totalDamage;
    }

    public void DestroyGameObject()
    {
        CanvasEnabler canvasEnabler = ground.GetComponent<CanvasEnabler>();
        canvasEnabler.isPlaceable = true;
        Destroy(gameObject);
    }

    private void RefreshChar()
    {
        foreach (TowerHolder _towerTesting in towerTesting)
        {
            if (characterData == _towerTesting.characterData)
            {
                _towerTesting.isDead = false;
                _towerTesting._charCooldown = characterData.towerCooldown;
            }
        }
    }

    public void TowerHolderEnabler()
    {
        foreach (TowerHolder _towerTesting in towerTesting)
        {
            if (characterData == _towerTesting.characterData)
            {
                _towerTesting.gameObject.SetActive(true);
                _towerTesting.isDead = true;
                _towerTesting._placed = false;
            }
        }
    }

    public void SkillAttack()
    {
        Instantiate(abilityHolder, firePoint.transform.position, transform.rotation);
    }

    public void Slowed(float slowAmount, float time) { }

    public void AttackSpeedBuff(float percentage, float duration)
    {
        float defaultAttackSpeed = anim.GetFloat(ATTACK_SPEED);

        float buffAttackSpeed = defaultAttackSpeed *= percentage;

        anim.SetFloat(ATTACK_SPEED, buffAttackSpeed);

        isBuffed = true;
        buffDuration = duration;
    }

    public void AttackBuff(float percentage, float duration)
    {
        damageValue *= percentage;

        isBuffed = true;
        buffDuration = duration;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        towerManager.DeleteState(gameObject, characterData, damageValue, baseArmor, currentHealth);
    }

    public void DeleteChar()
    {
        DestroyGameObject();
        TowerHolderEnabler();
    }


    #endregion




}
