using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerRotation : MonoBehaviour
{
    public Transform player; // �÷��̾� ��ü�� Transform
    public Transform vrCamera; // VR ī�޶� ��ü�� Transform

    // private Vector3 offset; // �÷��̾�� ī�޶� ���� �ʱ� ��ġ ����

    void Start()
    {
        // �ʱ� ��ġ ���� ���
       // offset = player.position - vrCamera.position;
    }

    void Update()
    {
        // VR ī�޶��� ��ġ�� ȸ���� �÷��̾ ����
       // player.position = vrCamera.position + offset;
        player.rotation = vrCamera.rotation;
    }
}
