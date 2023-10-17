using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCharacterEntity : CharEntity
{


    [HideInInspector]
    public Transform target;
    public GameObject currentTarget;
    [HideInInspector]
    public Vector3 _targetDir;

    public bool _inRange;

    [SerializeField] public TowerHolder[] towerTesting;

    public float enemiesInRange;

    public bool isDead;


    public override void Awake()
    {
        base.Awake();
        towerTesting = FindObjectsOfType<TowerHolder>();
    }

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
        enemiesInRange = 0;

        float maxDis = Mathf.Infinity;
        foreach (EnemyType enemy in enemies)
        {

            _targetDir = enemy.transform.position - transform.position;
            float distance = _targetDir.magnitude;

            if (distance < radius)
            {
                enemiesInRange++;
                if (distance < maxDis)
                {
                    maxDis = distance;
                    closestTarget = enemy.transform;
                    _anyEnemyInRange = true;
                }
            }



        }

        target = closestTarget;
        if (target != null)
        {
            currentTarget = target.gameObject;
        }

        _inRange = _anyEnemyInRange;
    }
}
