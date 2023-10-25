using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//녹화된 프레임의 정보를 보유 -> ※ Json으로 저장 -> 불러오기?
//저장해야하는 값 : GameObject, Position, Rotation, Scale, Animation, + ※ Audio(오디오데이터?), rigidbody(재생시 꺼야함)
//팔 리깅 값. . .?
public class Frame
{
    GameObject gameObject;
    Vector3 pos, scale;
    Quaternion rot;

    List<AnimationRecord> animation_record;

    //data 저장 값 
    public Frame(GameObject go, Vector3 position, Quaternion rotation, Vector3 scale, List<AnimationRecord> anim_records)
    {
        gameObject = go;
        pos = position;
        rot = rotation;
        this.scale = scale;
        animation_record = anim_records;
    }

    #region 값 얻기
    //읽기전용 프로퍼티
    public Vector3 Position
    {
        get { return pos; }
    }

    public Vector3 Scale
    {
        get { return scale; }
    }

    public Quaternion Rotation
    {
        get { return rot; }
    }

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    //ReplayRecord 에서도 List가 있는데 ?
    public List<AnimationRecord> Animation_Records
    {
        get { return animation_record; }
    }
    #endregion
}
