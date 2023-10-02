using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Sandayo : RangeCharacterEntity, IDamageable
{


    public AnimationHandler animationHandler;

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
        foreach (TowerTesting _towerTesting in towerTesting)
        {
            if (characterData == _towerTesting.characterData)
            {
                _towerTesting.isDead = false;
                _towerTesting._charCooldown = characterData.towerCost;
            }
        }
    }

    public override void Update()
    {
        base.Update();
        radius = characterData.range;




    }

    private void Start()
    {
        characterStateMachine.Initialize(idleState);
        isDead = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }



    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }



    private void OnDisable()
    {
        animationHandler.OnDeathFinish -= DestroyGameObject;
        animationHandler.OnDeathFinish -= TowerHolderEnabler;
    }

    public void TowerHolderEnabler()
    {
        foreach (TowerTesting _towerTesting in towerTesting)
        {
            if (characterData == _towerTesting.characterData)
            {
                Debug.Log("TowerEnabling");
                _towerTesting.gameObject.SetActive(true);
                _towerTesting.isDead = true;
                _towerTesting._placed = false;
            }
        }
    }


}
