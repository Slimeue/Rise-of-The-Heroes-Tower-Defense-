using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCharacterEntity : CharEntity
{


    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Vector3 _targetDir;

    public bool _inRange;

    [SerializeField] public TowerHolder[] towerTesting;

    public bool isDead;


    public override void Awake()
    {
        base.Awake();
        towerTesting = FindObjectsOfType<TowerHolder>();

    }

    private void Start() { }

    public override void Update()
    {
        base.Update();
        FindClosestTarget();
    }

    private void FindClosestTarget()
    {
        EnemyType[] enemies = FindObjectsOfType<EnemyType>();

        Transform closestTarget = null;

        bool _anyEnemyInRange = false;

        float maxDis = Mathf.Infinity;
        foreach (EnemyType enemy in enemies)
        {

            _targetDir = enemy.transform.position - transform.position;
            float distance = _targetDir.magnitude;
            if (distance < radius && distance < maxDis && enemy.isGroundType)
            {
                maxDis = distance;
                closestTarget = enemy.transform;
                _anyEnemyInRange = true;
            }

        }

        target = closestTarget;
        _inRange = _anyEnemyInRange;
    }
}
