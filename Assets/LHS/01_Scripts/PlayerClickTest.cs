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

    //1번을 선택하면 1번 배열의 친구가 불러와진다.
    //얘랑 인덱스 번호가 같으면 된다.
    public void OnClick(int num)
    {
        GameObject obj = Instantiate(player[num]);
    }
}
