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

    }

    public virtual void OnEnable()
    {
        if (Waypoints.points != null && Waypoints.points.Length > 0 && Waypoints.points[0] != null)
        {
            target = Waypoints.points[0];
        }

        pointIndex = 0;
        Debug.Log("enabled");
        speed = enemiesData.moveSpeed;
    }

    public bool OnEnemyFrontCheck()
    {


        return Physics.Raycast(transform.position, transform.forward, enemiesData.attackRange, enemiesData.whatIsTower);

        // if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, enemiesData.attackRange))
        // {
        //     TowerType towerType = hitInfo.collider.gameObject.GetComponent<TowerType>();
        //     currentTarget = hitInfo.collider.gameObject;
        //     if (hitInfo.collider.isTrigger && towerType != null)
        //     {
        //         return towerType.charType == enemiesData.whatIsTower;
        //     }
        // }
        // return false;



    }

}
