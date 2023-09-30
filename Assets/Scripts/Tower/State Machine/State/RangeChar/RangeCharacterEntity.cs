using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCharacterEntity : CharEntity
{
    [HideInInspector]
    public float radius;

    public float _rotationSpeed;

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Vector3 _targetDir;


    public bool _inRange;


    [SerializeField] public TowerTesting[] towerTesting;


    public override void Awake()
    {
        base.Awake();
        towerTesting = FindObjectsOfType<TowerTesting>();

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
            if (distance < radius && distance < maxDis)
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
