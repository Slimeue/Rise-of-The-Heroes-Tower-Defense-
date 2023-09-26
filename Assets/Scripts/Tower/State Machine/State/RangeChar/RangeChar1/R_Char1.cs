using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Char1 : RangeCharacterEntity
{
    [HideInInspector]
    public float radius;
    [HideInInspector]
    public float _rotationSpeed;

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Vector3 _targetDir;

    public bool _inRange;

    [SerializeField] List<string> _priority;


    public S_R_Char1_IdleState idleState { get; private set; }
    public S_R_Char1_SkillState skillState { get; private set; }
    public S_R_Char1_AttackState attackState { get; private set; }

    public override void Awake()
    {
        base.Awake();

        idleState = new S_R_Char1_IdleState(characterStateMachine, "idle", this, this);
        skillState = new S_R_Char1_SkillState(characterStateMachine, "skill", this, this);
        attackState = new S_R_Char1_AttackState(characterStateMachine, "attack", this, this);

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
