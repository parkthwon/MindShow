using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerRotation : MonoBehaviour
{
    public Transform player; // 플레이어 객체의 Transform
    public Transform vrCamera; // VR 카메라 객체의 Transform

    // private Vector3 offset; // 플레이어와 카메라 간의 초기 위치 차이

    void Start()
    {
        // 초기 위치 차이 계산
       // offset = player.position - vrCamera.position;
    }

    void Update()
    {
        // VR 카메라의 위치와 회전을 플레이어에 적용
       // player.position = vrCamera.position + offset;
        player.rotation = vrCamera.rotation;
    }
}
