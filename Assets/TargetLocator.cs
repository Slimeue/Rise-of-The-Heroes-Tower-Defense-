using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    Transform target;
    [SerializeField] float radius = 5.0f;
    [SerializeField] float _rotationSpeed = 2.0f;

    private void Awake()
    {

    }

    private void Update()
    {
        FindTarget();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void FindClosestTarget()
    {

    }

    private void FindTarget()
    {
        Vector3 _targetDir = target.position - transform.position;
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= radius)
        {
            Quaternion _targetRotation = Quaternion.LookRotation(_targetDir);

            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

}
