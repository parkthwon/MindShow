using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player; // �ν��Ͻ�ȭ�� �÷��̾� ������
    public GameObject player2; // �ν��Ͻ�ȭ�� �÷��̾� ������
    public void SpawnPlayer()
    {
        // �÷��̾ �ν��Ͻ�ȭ�ϰ� ���ϴ� ��ġ�� ȸ������ ��ġ
        Instantiate(player, new Vector3(0f, 0f, 0f), Quaternion.identity);
        Instantiate(player2, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }
}
