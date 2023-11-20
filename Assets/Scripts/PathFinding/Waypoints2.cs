using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints2 : MonoBehaviour
{



    public static Transform[] points2;

    #region 
    //Gizmos
    float radius = 2f;
    #endregion

    private void Awake()
    {
        points2 = new Transform[transform.childCount];
        for (int i = 0; i < points2.Length; i++)
        {

            points2[i] = transform.GetChild(i);

        }
    }

    private void OnDrawGizmos()
    {

        points2 = new Transform[transform.childCount];
        for (int i = 0; i < points2.Length; i++)
        {
            Gizmos.color = Color.blue;
            points2[i] = transform.GetChild(i);
            Gizmos.DrawWireSphere(points2[i].transform.position, radius);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

    }



}
