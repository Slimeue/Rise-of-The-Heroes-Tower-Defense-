using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    [SerializeField] bool isPlaceable;
    [SerializeField] GameObject tower;


    public static Transform[] points;

    #region 
    //Gizmos
    float radius = 2f;
    #endregion

    private void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    private void OnDrawGizmos()
    {

        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.blue;
            points[i] = transform.GetChild(i);
            Gizmos.DrawWireSphere(points[i].transform.position, radius);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }


    }



}
