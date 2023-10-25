using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*//저장되야할 정보 : 오브젝트이름, 시간, 위치, 회전, 팔 위치, 음성파일
[System.Serializable]
public class PlayerInfo
{
    //이름
    GameObject gameObject;
    //시간
    public float time;
    //위치
    public Vector3 pos;
    //회전
    public Quaternion rot;

    //HandAnchor 팔 위치 (왼팔 / 오른팔)
    public LeftHandInfo leftHand;

    public LeftHandInfo rightHand;
}

//왼팔
[System.Serializable]
public class LeftHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//오른팔
[System.Serializable]

public class RightHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//※사운드

//객체의 녹화된 프레임을 담을 클래스
[System.Serializable]
public class PlayerJsonList<T>
{
    public List<T> playerJsonList;
}
*//*public class PlayerJsonList<PlayerInfo>
{
    public List<PlayerInfo> list;
}*//*

[Serializable]
public class UnitInfo
{
    public GameObject unit;
    public PlayerJsonList<PlayerInfo> loadInfo;
    public int unitLoadIndex;
    public float curTime;
    public bool play;
}*/

public class ReplayManager_Ver2 : MonoBehaviour
{
    //녹화여부
    bool isRecord;
    bool isReplay;
    //※재생될 객체의 갯수로 하면! -> 변경
    int count = 2;
    //어떤 객체를 실행 시킬것인지
    int who;
    // 로드 될 객체
    int loadIndex = 0;

    //시간
    float totalTime;
    float curTime = 0;
    float recordTime = 0.1f;

    //저장/재생 될 플레이어들 -> 녹화된 객체가 있으면 추가!
    public GameObject[] unit;

    //녹화된 객체들을 담을 List
    PlayerJsonList<PlayerInfo> saveList;

    //녹화된 객체들을 불러올 List
    List<PlayerJsonList<PlayerInfo>> loadList;

    public List<UnitInfo> unitList = new List<UnitInfo>();

    //손 타겟
    LHandTarget lt;
    RHandTarget rt;

    private void Start()
    {
        // 시작할 때 녹화중 아님
        isRecord = false;

        //List  생성
        //저장 될 List
        saveList = new PlayerJsonList<PlayerInfo>();
        saveList.playerJsonList = new List<PlayerInfo>();

        //불러올 List
        loadList = new List<PlayerJsonList<PlayerInfo>>();

        LHandTarget lt = transform.GetComponentInChildren<LHandTarget>();
        RHandTarget rt = transform.GetComponentInChildren<RHandTarget>();
    }

    private void Update()
    {
        //녹화중
        if(isRecord)
        {
            Recording(who);
        }

        //리플레이중
        else if(isReplay)
        {
            lt.isTargeting = true;
            rt.isTargeting = true;

            //나의 손을 가져와서 타겟팅 꺼주기
            //Replaying();
            ReplayingPlay1(who);
        }
    }

    float time;
    //리플레이
    private void Replaying()
    {
        //curTime += Time.deltaTime;

        //재생 될 객체의 수만큼 반복한다.
        for (int i = 0; i < unitList.Count; i++)
        {
            Debug.Log(unit.Length);

            unitList[i].curTime += Time.deltaTime;

            //저장된 리스트의 0번째부터
            //PlayerInfo info = loadList[i].playerJsonList[loadIndex];
            PlayerInfo info = unitList[i].loadInfo.playerJsonList[unitList[i].unitLoadIndex];

            //플레이 
            /*unitList[i].unit.transform.position = info.pos;
            unitList[i].unit.transform.rotation = info.rot;*/

            unit[i].transform.position = Vector3.Lerp(transform.position, info.pos, 1);
            unit[i].transform.rotation = Quaternion.Lerp(transform.rotation, info.rot, 1);

            if (unitList[i].curTime >= info.time) //i == unitList.Count && curTime >= info.time
            {
                print("시간" + info.time);
                //각 객체의 인덱스 길이만큼 추가해야 한다.
                //loadIndex++;
                unitList[i].unitLoadIndex++;

                Debug.Log($"객체 이름 {unitList[i].unit},  읽을인덱스 {unitList[i].unitLoadIndex} , list 카운트 수 {unitList[i].loadInfo.playerJsonList.Count}");

                if (unitList[i].unitLoadIndex >= unitList[i].loadInfo.playerJsonList.Count)
                {
                    isReplay = false;
                    print("Stop" + unitList[i].loadInfo.playerJsonList.Count);
                }
            }
        }
    }

    private void ReplayingPlay1(int who)
    {
        curTime += Time.deltaTime;

        //저장된 리스트의 0번째부터
        //PlayerInfo info = loadList[i].playerJsonList[loadIndex];
        PlayerInfo info = unitList[who].loadInfo.playerJsonList[unitList[who].unitLoadIndex];

        //플레이 
        /*unitList[i].unit.transform.position = info.pos;
        unitList[i].unit.transform.rotation = info.rot;*/

        unit[who].transform.position = Vector3.Lerp(transform.position, info.pos, 1);
        unit[who].transform.rotation = Quaternion.Lerp(transform.rotation, info.rot, 1);

        if (curTime >= info.time) //i == unitList.Count && curTime >= info.time
        {
            print("시간" + info.time);
            //각 객체의 인덱스 길이만큼 추가해야 한다.
            //loadIndex++;
            unitList[who].unitLoadIndex++;

            Debug.Log($"객체 이름 {unitList[who].unit},  읽을인덱스 {unitList[who].unitLoadIndex} , list 카운트 수 {unitList[who].loadInfo.playerJsonList.Count}");

            if (unitList[who].unitLoadIndex >= unitList[who].loadInfo.playerJsonList.Count)
            {
                isReplay = false;
                print("Stop" + unitList[who].loadInfo.playerJsonList.Count);
            }
        }
    }

    //녹화
    private void Recording(int who)
    {
        curTime += Time.deltaTime;

        totalTime += Time.deltaTime;

        //0.1초 간격으로 저장
        if(curTime > recordTime)
        {
            curTime -= recordTime;

            SavePlayerInfo(who);
        }
    }

    //저장
    private void SavePlayerInfo(int who)
    {
        PlayerInfo info = new PlayerInfo()
        {
            time = totalTime,
            pos = unit[who].transform.position,
            rot = unit[who].transform.rotation,
            leftHand = null,
            rightHand = null
        };

        saveList.playerJsonList.Add(info);
    }

    // 녹화 시작
    public void OnRecordStart(int who)
    {
        print("녹화시작" + who);
        this.who = who;
        isRecord = true;
        
        //처음부터 녹화
        saveList.playerJsonList.Clear();
    }

    public void OnRecordEnd()
    {
        print("녹화종료" + who);

        isRecord = false;

        //파일이 있으면 ?

        //파일 쓰기
        string json = JsonUtility.ToJson(saveList, true);
        File.WriteAllText(Application.dataPath + "/save" + who + ".txt", json);
    }

    public void OnRecordPlay()
    {
        print("리플레이");

        if (isRecord)
        {
            OnRecordEnd();
        }

        isReplay = true;

        for (int i = 0; i < unitList.Count; i++)
        {
            string json = File.ReadAllText(Application.dataPath + "/save" + i + ".txt");

            var jsonList = JsonUtility.FromJson<PlayerJsonList<PlayerInfo>>(json);
            
            loadList.Add(jsonList);

            unitList[i].loadInfo = jsonList;
            unitList[i].unitLoadIndex = 0;
        }

        //loadIndex = 0;
        curTime = 0;
    }

    public void OnRecordPlay2(int who)
    {
        print("리플레이");

        if (isRecord)
        {
            OnRecordEnd();
        }

        this.who = who;
        isReplay = true;

        string json = File.ReadAllText(Application.dataPath + "/save" + who + ".txt");

        var jsonList = JsonUtility.FromJson<PlayerJsonList<PlayerInfo>>(json);

        loadList.Add(jsonList);

        unitList[who].loadInfo = jsonList;
        unitList[who].unitLoadIndex = 0;

        //loadIndex = 0;
        curTime = 0;
    }
}
