using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    //TextMeshPro label;
    Vector2Int coordinate = new Vector2Int();

    void Awake()
    {
        //label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
    }

    private void DisplayCoordinates()
    {
        coordinate.x = Mathf.RoundToInt(transform.parent.position.x / 10f);
        coordinate.y = Mathf.RoundToInt(transform.parent.position.z / 10f);

        //label.text = coordinate.x + "," + coordinate.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinate.ToString();
    }
}
