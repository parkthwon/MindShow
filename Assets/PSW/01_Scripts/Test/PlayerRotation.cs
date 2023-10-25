using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform vrCamera; // VR ī�޶� ��ü�� Transform
    public Transform player; // �÷��̾� ĳ������ Transform ������Ʈ�� �Ҵ��մϴ�.

    void Update()
    {
        //���ͺ����� �����.
        Vector3 vector3;
        //�� ������ ���� ��ġ�� �ִ´�.
        vector3 = transform.localPosition;
        ////���� ������ x ���� vrCamera �� x ������ �Ѵ�.
        //vector3.x = vrCamera.position.x;
        ////���� ������ z ���� vrCamera �� z ������ �Ѵ�.
        //vector3.z = vrCamera.position.z;
        //���� ��ġ�� ���纯���� �����Ѵ�.
        transform.position = vector3;

        Vector3 v = vrCamera.forward;
        v.y = 0;
        transform.forward = v;
    }
}
