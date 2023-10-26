using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCharacterEntity : CharEntity
{

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Vector3 _targetDir;

    public RangeCharAnimationHandler rangeCharAnimationHandler;

    public bool _inRange;

    [Space(5)]
    [SerializeField]
    public GameObject projectile;
    [SerializeField]
    public Transform firePoint;


    [SerializeField] public TowerHolder[] towerTesting;

    public bool isDead;


    public override void Awake()
    {
        base.Awake();
        towerTesting = FindObjectsOfType<TowerHolder>();
        projectile = characterData.projectile;
        rangeCharAnimationHandler = GetComponent<RangeCharAnimationHandler>();

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

            if (distance < radius)
            {
                if (distance < maxDis)
                {
                    if (enemy.isAerialType)
                    {
                        maxDis = distance;
                        closestTarget = enemy.transform;
                        _anyEnemyInRange = true;
                    }
                    else if (!closestTarget || !closestTarget.GetComponent<EnemyType>().isAerialType)
                    {
                        maxDis = distance;
                        closestTarget = enemy.transform;
                        _anyEnemyInRange = true;
                    }
                }
            }

        }

        target = closestTarget;
        _inRange = _anyEnemyInRange;
    }
}
