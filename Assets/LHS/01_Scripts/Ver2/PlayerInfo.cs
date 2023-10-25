using System;
using System.Collections.Generic;
using UnityEngine;

//����Ǿ��� ���� : ������Ʈ�̸�, �ð�, ��ġ, ȸ��, �� ��ġ, ++ �������� , �̺�Ʈ �߻�����
//ĳ���� ��ȣ
[Serializable]
public class PlayerInfo
{
    //�̸�
    public string name;
    //�ð�
    public float time;
    //��ġ
    public Vector3 pos;
    //ȸ��
    public Quaternion rot;

    //HandAnchor �� ��ġ (���� / ������)
    public LeftHandInfo leftHand;

    public RightHandInfo rightHand;
}

//����
[Serializable]
public class LeftHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//������
[Serializable]
public class RightHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//��ü�� ��ȭ�� �������� ���� Ŭ����
[Serializable]
public class PlayerJsonList<T>
{
    public List<T> playerJsonList;
}

//�� ���� ����

//���÷��� �� ��ü��
[Serializable]
public class UnitInfo
{
    //���ӿ�����Ʈ
    public GameObject unit;
    //��ȭ�� ����
    public PlayerJsonList<PlayerInfo> loadInfo;
    //�ε� �� �ε��� ��ȣ
    public int unitLoadIndex;
    //�ε� �� �ð�
    public float curTime;
}
