using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickTest : MonoBehaviour
{
    public GameObject[] player;
    
    void Start()
    {
      
    }

    void Update()
    {
        
    }

    //1���� �����ϸ� 1�� �迭�� ģ���� �ҷ�������.
    //��� �ε��� ��ȣ�� ������ �ȴ�.
    public void OnClick(int num)
    {
        GameObject obj = Instantiate(player[num]);
    }
}
