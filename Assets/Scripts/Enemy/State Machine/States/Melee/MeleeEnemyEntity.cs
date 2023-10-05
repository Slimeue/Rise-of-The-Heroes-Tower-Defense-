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
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, enemiesData.attackRange))
        {
            TowerType towerType = hitInfo.collider.gameObject.GetComponent<TowerType>();
            currentTarget = hitInfo.collider.gameObject;
            if (towerType != null)
            {
                return towerType.charType == enemiesData.whatIsTower;
            }
        }
        return false;



    }

}
