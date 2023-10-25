using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rec_PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float rotSpeed = 200.0f;
    public GameObject cameraRig;
    public Transform myCharacter;
    public Animator anim;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        // �޼� �潺ƽ�� ���� ���� ������ ĳ������ �̵������� ����
        Vector2 stickPos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        Vector3 dir = new Vector3(stickPos.x, 0, stickPos.y);
        dir.Normalize();

        //ĳ������ �̵����� ���͸� ī�޶� �ٶ󺸴� ������ �������� �ϵ��� ����
        dir = cameraRig.transform.TransformDirection(dir);
        transform.position += dir * moveSpeed * Time.deltaTime;

        //���� �޼� �潺ƽ�� ��︮�� �� �������� ĳ���͸� ȸ����Ų��.
        float magnitude = dir.magnitude;

        if(magnitude > 0)
        {
            myCharacter.rotation = Quaternion.LookRotation(dir);
        }

        //anim.SetFloat("Speed", magnitude);
    }

    void Rotate()
    {
        //�������� ���Ⱚ���� �¿� ���⸦ ������Ų��.
        float rotH = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;

        //ī�޶� ���� ������Ʈ�� ȸ����Ų��.
        cameraRig.transform.eulerAngles += new Vector3(0, rotH, 0) * rotSpeed * Time.deltaTime;
    }
}
