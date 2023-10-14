using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    public static SquadManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
