using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Sandayo : RangeCharacterEntity, IDamageable
{



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
        Projectile _projectile = projectileGO.GetComponent<Projectile>();

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

    #endregion




}
