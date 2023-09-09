using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Waypoints> paths = new List<Waypoints>();
    [SerializeField] float speed = 1f;


    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        StartCoroutine(FollowPaths());
    }

    void FindPath()
    {
        paths.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Waypoints waypoints = child.GetComponent<Waypoints>();
            if (waypoints != null)
            {
                paths.Add(waypoints);
            }

        }
    }

    IEnumerator FollowPaths()
    {
        foreach (Waypoints paths in paths)
        {
            Vector3 _startPosition = transform.position;
            Vector3 _endPosition = paths.transform.position;
            float _travelPercent = 0f;

            transform.LookAt(_endPosition);

            while (_travelPercent < 1f)
            {
                _travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(_startPosition, _endPosition, _travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        gameObject.SetActive(false);
    }
}
