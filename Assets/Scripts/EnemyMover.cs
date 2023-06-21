using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Waypoints> waypoints = new List<Waypoints>();


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPaths());
    }

    IEnumerator FollowPaths()
    {
        foreach (Waypoints paths in waypoints)
        {
            transform.position = paths.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
