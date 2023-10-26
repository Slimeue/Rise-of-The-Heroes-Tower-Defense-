using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyEntity : EnemyEntity
{
    [HideInInspector]
    public Transform enemyTarget;
    [HideInInspector]
    public Vector3 _targetDir;

    public RangeCharAnimationHandler rangeCharAnimationHandler;

    public bool _inRange;

    [Space(5)]
    [SerializeField]
    public GameObject projectile;
    [SerializeField]
    public Transform firePoint;
    [HideInInspector]
    public float rotationSpeed;

    public override void Awake()
    {
        base.Awake();
        rotationSpeed = enemiesData.rotationsSpeed;
        Debug.Log(TowerType.RANGE_CHAR);
        projectile = enemiesData.projectile;
        target = Waypoints.points[0];
        rangeCharAnimationHandler = GetComponent<RangeCharAnimationHandler>();
    }

    public override void Update()
    {
        base.Update();
        FindClosestTarget();
    }

    public virtual void OnEnable()
    {
        target = Waypoints.points[0];
        pointIndex = 0;
        Debug.Log("enabled");
    }


    private void FindClosestTarget()
    {
        TowerType[] enemies = FindObjectsOfType<TowerType>();
        Transform closestTarget = null;
        bool _anyEnemyInRange = false;
        float maxDis = Mathf.Infinity;

        foreach (TowerType enemy in enemies)
        {

            _targetDir = enemy.transform.position - transform.position;
            float distance = _targetDir.magnitude;

            if (distance < radius)
            {
                if (distance < maxDis)
                {
                    if (enemy.characterData._platformTag == TowerType.RANGE_CHAR)
                    {
                        maxDis = distance;
                        closestTarget = enemy.transform;
                        _anyEnemyInRange = true;
                    }
                    else if (!closestTarget || closestTarget.GetComponent<TowerType>().characterData._platformTag != TowerType.RANGE_CHAR)
                    {
                        maxDis = distance;
                        closestTarget = enemy.transform;
                        _anyEnemyInRange = true;
                    }
                }
            }

        }

        enemyTarget = closestTarget;
        _inRange = _anyEnemyInRange;
    }


}
