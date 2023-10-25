using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPositionSelector : MonoBehaviour
{
    public Transform hand; // ��Ʈ�ѷ� �ڵ� ��ġ
    GameObject player1; // �ν��Ͻ�Ȱ ��ų ������1
    GameObject player2; // �ν��Ͻ�Ȱ ��ų ������2

    void Update()
    {
        // ��� �ִ� ���̶��
        if (null != player1)
        {
            // player1�� ������ ��� �̵���Ű�� �ʹ�.
            player1.transform.position = Vector3.Lerp(player1.transform.position, hand.position, Time.deltaTime * 5);
        }
        else if (null != player2) 
        {
            // player2�� ������ ��� �̵���Ű�� �ʹ�.
            player2.transform.position = Vector3.Lerp(player2.transform.position, hand.position, Time.deltaTime * 5);
        }

        // 1Ű�� ������
        // �÷��̾�1�� �տ� ��� �ʹ�.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (false == GripByOverlap())
            {
                GripByRay();
            }
        }

        // 1Ű�� ���� ��
        if (Input.GetKeyUp(KeyCode.Alpha1)) 
        { 
            // Player1�� ��������
            if (player1)
            {
                player1 = null;
            }
        }


        // 2Ű�� ������
        // �÷��̾� 2�� �տ� ��� �ʹ�.
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
        // hand ��ġ���� hand �չ������� Ray�� �����
        Ray ray = new Ray(hand.position, hand.forward);
        // �ٶ󺸰�
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo)) 
        {
            player1 = Instantiate(hitInfo.transform.gameObject);
            player2 = Instantiate(hitInfo.transform.gameObject); 
        }
    }

    void GirpByRay2()
    {
        // hand ��ġ���� hand �չ������� Ray�� �����
        Ray ray = new Ray(hand.position, hand.forward);
        // �ٶ󺸰�
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
