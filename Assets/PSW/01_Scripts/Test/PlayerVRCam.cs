using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayerVRCam : MonoBehaviour
{
    // �÷��̾� 1,2 ����
    public Transform tr1;
    public Transform tr2;

    // Ȱ��ȭ �Ұ����� �ƴ����� ���� ����
    bool isPlayerActive = true;
    void Start()
    {
        // tr�� �θ�� ����
        transform.SetParent(tr1); // false�� �����Ͽ� �θ��� ���� ��ȯ�� �����մϴ�.

        // �θ� ������Ʈ�� Ʈ������ ������Ʈ ��������
        tr2 = transform.parent;

        // �θ�� �ڽ��� x ��ǥ�� �����ϰ� ����
        Vector3 childPosition = transform.localPosition;
        childPosition.x = tr2.localPosition.x;
        transform.localPosition = childPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // GŰ�� ������ �÷��̾� ��ȯ
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isPlayerActive)
            {
                SetActivePlayer(tr1.transform);
            }
            else
            {
                SetActivePlayer(tr2.transform);
            }

            // �÷��̾� ��ȯ �� ���� ����
            isPlayerActive = !isPlayerActive;
        }
    }

    // �־��� �÷��̾ Ȱ��ȭ�ϰ� ī�޶� �ش� ��ġ�� �̵�
    private void SetActivePlayer(Transform playerTransform)
    {
        transform.SetParent(playerTransform);
        Transform playerParent = transform.parent;
        Vector3 childPosition = transform.localPosition;
        childPosition.x = playerParent.localPosition.x;
        transform.localPosition = childPosition;
    }
}
