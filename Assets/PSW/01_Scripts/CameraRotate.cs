using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자의 마우스 입력에 따라 x,y 축을 회전하고 싶다.
public class CameraRotate : MonoBehaviour
{
    float rx, ry;
    public float rotSpeed = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 사용자의 마우스 입력을 누적하고 싶다.
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        // => 엇갈려서 누적하는 이유 좌우 mx 값인데 Y 축을 돌려야함으로 x는 끄덕끄덕 y는 도리도리
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;
        // 각도 제한하기
        rx = Mathf.Clamp(rx, -75, 75);
        // 2. 그 누적값으로 x,y 축을 회전하고 싶다.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
