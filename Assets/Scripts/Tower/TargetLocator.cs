using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    Transform target;
    [SerializeField] float radius = 5.0f;
    [SerializeField] float _rotationSpeed = 2.0f;

    Vector3 _targetDir;

    private void Update()
    {
        FindClosestTarget();
        FindTarget();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void FindClosestTarget()
    {
        EnemyMover[] enemies = FindObjectsOfType<EnemyMover>();
        Transform closestTarget = null;
        float maxDis = Mathf.Infinity;
        foreach (EnemyMover enemy in enemies)
        {
            _targetDir = enemy.transform.position - transform.position;
            float distance = _targetDir.magnitude;
            if (distance < radius && distance < maxDis)
            {
                maxDis = distance;
                closestTarget = enemy.transform;
            }
        }
        target = closestTarget;
    }

    private void FindTarget()
    {

        if (target != null)
        {
            Vector3 targetDir = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

}
