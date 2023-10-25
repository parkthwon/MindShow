using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPositionSelector : MonoBehaviour
{
    public Transform hand; // 컨트롤러 핸드 위치
    GameObject player1; // 인스턴스활 시킬 프리팹1
    GameObject player2; // 인스턴스활 시킬 프리팹2

    void Update()
    {
        // 잡고 있는 중이라면
        if (null != player1)
        {
            // player1을 손으로 계속 이동시키고 싶다.
            player1.transform.position = Vector3.Lerp(player1.transform.position, hand.position, Time.deltaTime * 5);
        }
        else if (null != player2) 
        {
            // player2을 손으로 계속 이동시키고 싶다.
            player2.transform.position = Vector3.Lerp(player2.transform.position, hand.position, Time.deltaTime * 5);
        }

        // 1키를 누르면
        // 플레이어1를 손에 쥐고 싶다.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (false == GripByOverlap())
            {
                GripByRay();
            }
        }

        // 1키를 뗏을 때
        if (Input.GetKeyUp(KeyCode.Alpha1)) 
        { 
            // Player1이 잡혔으면
            if (player1)
            {
                player1 = null;
            }
        }


        // 2키를 누르면
        // 플레이어 2를 손에 쥐고 싶다.
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        { 
            if (false == GripByOverlap2()) 
            { 
                GirpByRay2();
            }
        }

    }

    private void GripByRay()
    {
        // hand 위치에서 hand 앞방향으로 Ray를 만들고
        Ray ray = new Ray(hand.position, hand.forward);
        // 바라보고
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo)) 
        {
            player1 = Instantiate(hitInfo.transform.gameObject);
            player2 = Instantiate(hitInfo.transform.gameObject); 
        }
    }

    void GirpByRay2()
    {
        // hand 위치에서 hand 앞방향으로 Ray를 만들고
        Ray ray = new Ray(hand.position, hand.forward);
        // 바라보고
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            player2 = Instantiate(hitInfo.transform.gameObject);
        }
    }

    private bool GripByOverlap()
    {
        player1 = Instantiate(hand.transform.gameObject);
        return true;
    }

    private bool GripByOverlap2()
    {
        player2 = Instantiate(hand.transform.gameObject);
        return true;
    }
}
