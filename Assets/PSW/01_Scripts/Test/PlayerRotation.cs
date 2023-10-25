using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform vrCamera; // VR 카메라 객체의 Transform
    public Transform player; // 플레이어 캐릭터의 Transform 컴포넌트를 할당합니다.

    void Update()
    {
        //벡터변수를 만든다.
        Vector3 vector3;
        //그 변수에 나의 위치를 넣는다.
        vector3 = transform.localPosition;
        ////만든 변수의 x 값은 vrCamera 의 x 값으로 한다.
        //vector3.x = vrCamera.position.x;
        ////만든 변수의 z 값은 vrCamera 의 z 값으로 한다.
        //vector3.z = vrCamera.position.z;
        //나의 위치를 만든변수로 셋팅한다.
        transform.position = vector3;

        Vector3 v = vrCamera.forward;
        v.y = 0;
        transform.forward = v;
    }
}
