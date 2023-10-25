using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ȭ�� �������� ������ ���� -> �� Json���� ���� -> �ҷ�����?
//�����ؾ��ϴ� �� : GameObject, Position, Rotation, Scale, Animation, + �� Audio(�����������?), rigidbody(����� ������)
//�� ���� ��. . .?
public class Frame
{
    GameObject gameObject;
    Vector3 pos, scale;
    Quaternion rot;

    List<AnimationRecord> animation_record;

    //data ���� �� 
    public Frame(GameObject go, Vector3 position, Quaternion rotation, Vector3 scale, List<AnimationRecord> anim_records)
    {
        gameObject = go;
        pos = position;
        rot = rotation;
        this.scale = scale;
        animation_record = anim_records;
    }

    #region �� ���
    //�б����� ������Ƽ
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

    //ReplayRecord ������ List�� �ִµ� ?
    public List<AnimationRecord> Animation_Records
    {
        get { return animation_record; }
    }
    #endregion
}
