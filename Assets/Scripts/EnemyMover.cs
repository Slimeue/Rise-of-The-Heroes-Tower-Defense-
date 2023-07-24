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
            Vector3 _startPosition = transform.position;
            Vector3 _endPosition = paths.transform.position;
            float _travelPercent = 0f;

            transform.LookAt(_endPosition);

            while (_travelPercent < 1f)
            {
                _travelPercent += Time.deltaTime;
                transform.position = Vector3.Lerp(_startPosition, _endPosition, _travelPercent);

                yield return new WaitForEndOfFrame();
            }

        }
    }
}
