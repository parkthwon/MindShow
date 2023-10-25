using System;
using System.Collections.Generic;
using UnityEngine;

//저장되야할 정보 : 오브젝트이름, 시간, 위치, 회전, 팔 위치, ++ 음성파일 , 이벤트 발생시점
//캐릭터 번호
[Serializable]
public class PlayerInfo
{
    //이름
    public string name;
    //시간
    public float time;
    //위치
    public Vector3 pos;
    //회전
    public Quaternion rot;

    //HandAnchor 팔 위치 (왼팔 / 오른팔)
    public LeftHandInfo leftHand;

    public RightHandInfo rightHand;
}

//왼팔
[Serializable]
public class LeftHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//오른팔
[Serializable]
public class RightHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//객체의 녹화된 프레임을 담을 클래스
[Serializable]
public class PlayerJsonList<T>
{
    public List<T> playerJsonList;
}

//※ 추후 사운드

//리플레이 될 객체들
[Serializable]
public class UnitInfo
{
    //게임오브젝트
    public GameObject unit;
    //녹화된 정보
    public PlayerJsonList<PlayerInfo> loadInfo;
    //로드 될 인덱스 번호
    public int unitLoadIndex;
    //로드 될 시간
    public float curTime;
}
