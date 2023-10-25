using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //맵 정보
    //플레이어 정보
    //들어온 플레이어 수

    public List<GameObject> gamePlayerList;

    public int playerNum;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
