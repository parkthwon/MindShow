using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어가 따라다닐 타겟
public class LHandTarget : MonoBehaviour
{
    public Transform target;
    public Transform idle;

    public bool isTargeting;

    //타겟 동적으로 찾아야 함.
    void Start()
    {
        //생성될때 타겟 팔을 찾아서 셋팅 해야함
        target = GameObject.FindGameObjectWithTag("LHand").transform;

        //시작할때 손 셋팅
        if(idle != null)
        {
            transform.position = idle.transform.position;
            transform.rotation = idle.transform.rotation;
        }
    }

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


