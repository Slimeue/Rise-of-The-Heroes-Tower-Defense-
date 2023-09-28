using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Sandayo : RangeCharacterEntity
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
    }

    public override void Update()
    {
        base.Update();
        radius = characterData.range;
        FindClosestTarget();
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

    private void FindClosestTarget()
    {
        EnemyType[] enemies = FindObjectsOfType<EnemyType>();

        Transform closestTarget = null;

        float maxDis = Mathf.Infinity;
        foreach (EnemyType enemy in enemies)
        {

            _targetDir = enemy.transform.position - transform.position;
            float distance = _targetDir.magnitude;
            if (distance < radius && distance < maxDis)
            {
                maxDis = distance;
                closestTarget = enemy.transform;
                _inRange = true;
            }

        }
        target = closestTarget;
    }




}
