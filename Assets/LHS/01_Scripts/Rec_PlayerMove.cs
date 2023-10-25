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
        // 왼속 썸스틱의 방향 값을 가져와 캐릭터의 이동방향을 정함
        Vector2 stickPos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        Vector3 dir = new Vector3(stickPos.x, 0, stickPos.y);
        dir.Normalize();

        //캐릭터의 이동방향 벡터를 카메라가 바라보는 방향을 정면으로 하도록 변경
        dir = cameraRig.transform.TransformDirection(dir);
        transform.position += dir * moveSpeed * Time.deltaTime;

        //만일 왼속 썸스틱을 기울리면 그 방향으로 캐릭터를 회전시킨다.
        float magnitude = dir.magnitude;

        if(magnitude > 0)
        {
            myCharacter.rotation = Quaternion.LookRotation(dir);
        }

        //anim.SetFloat("Speed", magnitude);
    }

    void Rotate()
    {
        //오른손의 방향값에서 좌우 기울기를 누적시킨다.
        float rotH = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;

        //카메라 리그 오브젝트를 회전시킨다.
        cameraRig.transform.eulerAngles += new Vector3(0, rotH, 0) * rotSpeed * Time.deltaTime;
    }
}
