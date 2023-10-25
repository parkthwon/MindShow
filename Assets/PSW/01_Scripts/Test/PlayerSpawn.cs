using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player; // 인스턴스화할 플레이어 프리팹
    public GameObject player2; // 인스턴스화할 플레이어 프리팹
    public void SpawnPlayer()
    {
        // 플레이어를 인스턴스화하고 원하는 위치와 회전으로 배치
        Instantiate(player, new Vector3(0f, 0f, 0f), Quaternion.identity);
        Instantiate(player2, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }
}
