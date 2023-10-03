using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyEntity : EnemyEntity
{

    #region Checks
    public bool isInFront;
    #endregion

    public override void Awake()
    {
        base.Awake();
        target = Waypoints.points[0];
    }

    void Start()
    {

    }

    public bool OnEnemyFrontCheck()
    {
        return Physics.Raycast(transform.position, transform.forward, enemiesData.attackRange, enemiesData.whatIsTower);
    }

}
