using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구현목표
//재생 될 리스트를 담고 플레이 하면 그 객체의 플레이 함수가 실행
//내가 녹화 버튼을 눌렀을 때 재생이 저장된 객체가 있다면
//플레이 될 수 있게
//나는 녹화 //너는 플레이 해 !!!!

//녹화 기능
public class RecSet : MonoBehaviour
{
    // 재생 될 리스트 
    [SerializeField]
    PlayerRecord recrod;

    PlayerMove pm;

    public Transform mainPlayer;

    Mic_LHS mic;

    public bool isRec = false;
    public void Start()
    {
        pm = this.GetComponent<PlayerMove>();
    }

    public void OnRecordStart()
    {
        //자식에 붙어있는 플레이어의 녹화컴포넌트를 가져온다.
        recrod = transform.GetComponentInChildren<PlayerRecord>();

        mic = transform.GetComponentInChildren<Mic_LHS>();

        //recrod가 null이 아닐때만
        if (recrod != null)
        {
            //만약 담겨져 있는 리플레이 객체가 있다면 재생시키면서 
            //중요! 리플레이가 될때 녹화가 진행되야함.
            Debug.Log("RM" + recrod + "의 녹화시작");
            //녹화 플레이를 재생 시킴!
            recrod.OnRecordStart();
            mic.OnStart();
        }

        else
        {
            print("녹화불가능 플레이어");
        }
    }

    public void OnRecordEnd()
    {
        if (recrod != null)
        {
            Debug.Log("RM" + recrod + "의 녹화종료");
            recrod.OnRecordEnd();

            //녹화 정지를 누르면 main 플레이어로 바꿔야 함!
            //pm.targetPlayer = mainPlayer;
            UI.Player_State = UI.PlayerState.Normal;
            pm.CharChange(mainPlayer.gameObject);
            mic.OnEnd();
        }

        else
        {
            print("녹화종료할게 없음");
        }
    }

    public void OnRecording()
    {
        isRec = !isRec;

        //녹화중
        if(isRec)
        {
            OnRecordStart();
        }

        else
        {
            OnRecordEnd();
        }
    }
}