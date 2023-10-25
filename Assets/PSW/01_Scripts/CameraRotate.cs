using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� ���콺 �Է¿� ���� x,y ���� ȸ���ϰ� �ʹ�.
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
        // 1. ������� ���콺 �Է��� �����ϰ� �ʹ�.
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        // => �������� �����ϴ� ���� �¿� mx ���ε� Y ���� ������������ x�� �������� y�� ��������
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;
        // ���� �����ϱ�
        rx = Mathf.Clamp(rx, -75, 75);
        // 2. �� ���������� x,y ���� ȸ���ϰ� �ʹ�.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
