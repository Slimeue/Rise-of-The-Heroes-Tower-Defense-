using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookAtPlayer : MonoBehaviour
{

    public GameObject cam;
    // Update is called once per frame

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("NewCam");
    }

    void Update()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }


}
