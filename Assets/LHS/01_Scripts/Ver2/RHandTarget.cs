using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RHandTarget : MonoBehaviour
{
    public Transform target;
    public Transform idle;

    public bool isTargeting;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("RHand").transform;

        if (idle != null)
        {
            transform.position = idle.transform.position;
            transform.rotation = idle.transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //녹화중일때
        if (isTargeting)
        {
            //저장되어야 할 포지션
            transform.position = target.transform.position;
            transform.rotation = target.transform.rotation;
        }

        //녹화중 아닐때
        else
        {

        }
    }
}
