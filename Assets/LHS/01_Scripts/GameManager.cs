using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //�� ����
    //�÷��̾� ����
    //���� �÷��̾� ��

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
