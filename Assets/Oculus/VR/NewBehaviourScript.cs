using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Vector3 prev;
    void Start()
    {
        
    }

    void Update()
    {
        print(transform.eulerAngles - prev);

        prev = transform.eulerAngles;
    }
}
