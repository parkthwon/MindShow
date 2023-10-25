using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//리플레이 기능
public class ReplaySet : MonoBehaviour
{
    public static ReplaySet instance;

    //게임오브젝트
    public GameObject[] unit;

    PlayerRecord playerRecored;
    Mic_LHS mic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Update()
    {
        //배치된 플레이어를 다 담기
        unit = GameObject.FindGameObjectsWithTag("Player");
    }

    // 담겨져 있는 리플레이를 모두 실행! -> 리플레이 클래스에서 
    public void OnRecordPlay()
    {
        if (unit.Length > 0)
        {
            //저장된 객체의 함수를 반복해서 부른다.!
            for (int i = 0; i < unit.Length; i++)
            {
                var playerRecored = unit[i].GetComponent<PlayerRecord>();
                var playerMic = unit[i].GetComponent<Mic_LHS>();

                playerRecored.OnRecordPlay();
                playerMic.OnReplay();
            }

            print(unit.Length);
        }

        //만약 없다면 재생될 플레이어가 없다고 표시
        else
        {
            print("재생할 게 없어.");
        }
    }

    public void OnAutoReplayForRecording(PlayerRecord who)
    {
        if (unit.Length > 0)
        {
            //저장된 객체의 함수를 반복해서 부른다.!
            for (int i = 0; i < unit.Length; i++)
            {
                var playerRecored = unit[i].GetComponent<PlayerRecord>();
                var playerMic = unit[i].GetComponent<Mic_LHS>();

                //나 빼고 반복
                if (who == playerRecored)
                { 
                    continue;
                }

                playerRecored.OnRecordPlay();
                playerMic.OnReplay();
            }

            print(unit.Length);
        }

        //만약 없다면 재생될 플레이어가 없다고 표시
        else
        {
            print("재생할 게 없어.");
        }
    }
}
