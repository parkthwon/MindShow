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
        //��ȭ���϶�
        if (isTargeting)
        {
            //����Ǿ�� �� ������
            transform.position = target.transform.position;
            transform.rotation = target.transform.rotation;
        }

        //��ȭ�� �ƴҶ�
        else
        {

        }
    }
}
