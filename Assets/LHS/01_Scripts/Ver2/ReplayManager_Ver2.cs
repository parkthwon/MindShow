using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*//����Ǿ��� ���� : ������Ʈ�̸�, �ð�, ��ġ, ȸ��, �� ��ġ, ��������
[System.Serializable]
public class PlayerInfo
{
    //�̸�
    GameObject gameObject;
    //�ð�
    public float time;
    //��ġ
    public Vector3 pos;
    //ȸ��
    public Quaternion rot;

    //HandAnchor �� ��ġ (���� / ������)
    public LeftHandInfo leftHand;

    public LeftHandInfo rightHand;
}

//����
[System.Serializable]
public class LeftHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//������
[System.Serializable]

public class RightHandInfo
{
    public Vector3 pos;
    public Quaternion rot;
}

//�ػ���

//��ü�� ��ȭ�� �������� ���� Ŭ����
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
    //��ȭ����
    bool isRecord;
    bool isReplay;
    //������� ��ü�� ������ �ϸ�! -> ����
    int count = 2;
    //� ��ü�� ���� ��ų������
    int who;
    // �ε� �� ��ü
    int loadIndex = 0;

    //�ð�
    float totalTime;
    float curTime = 0;
    float recordTime = 0.1f;

    //����/��� �� �÷��̾�� -> ��ȭ�� ��ü�� ������ �߰�!
    public GameObject[] unit;

    //��ȭ�� ��ü���� ���� List
    PlayerJsonList<PlayerInfo> saveList;

    //��ȭ�� ��ü���� �ҷ��� List
    List<PlayerJsonList<PlayerInfo>> loadList;

    public List<UnitInfo> unitList = new List<UnitInfo>();

    //�� Ÿ��
    LHandTarget lt;
    RHandTarget rt;

    private void Start()
    {
        // ������ �� ��ȭ�� �ƴ�
        isRecord = false;

        //List  ����
        //���� �� List
        saveList = new PlayerJsonList<PlayerInfo>();
        saveList.playerJsonList = new List<PlayerInfo>();

        //�ҷ��� List
        loadList = new List<PlayerJsonList<PlayerInfo>>();

        LHandTarget lt = transform.GetComponentInChildren<LHandTarget>();
        RHandTarget rt = transform.GetComponentInChildren<RHandTarget>();
    }

    private void Update()
    {
        //��ȭ��
        if(isRecord)
        {
            Recording(who);
        }

        //���÷�����
        else if(isReplay)
        {
            lt.isTargeting = true;
            rt.isTargeting = true;

            //���� ���� �����ͼ� Ÿ���� ���ֱ�
            //Replaying();
            ReplayingPlay1(who);
        }
    }

    float time;
    //���÷���
    private void Replaying()
    {
        //curTime += Time.deltaTime;

        //��� �� ��ü�� ����ŭ �ݺ��Ѵ�.
        for (int i = 0; i < unitList.Count; i++)
        {
            Debug.Log(unit.Length);

            unitList[i].curTime += Time.deltaTime;

            //����� ����Ʈ�� 0��°����
            //PlayerInfo info = loadList[i].playerJsonList[loadIndex];
            PlayerInfo info = unitList[i].loadInfo.playerJsonList[unitList[i].unitLoadIndex];

            //�÷��� 
            /*unitList[i].unit.transform.position = info.pos;
            unitList[i].unit.transform.rotation = info.rot;*/

            unit[i].transform.position = Vector3.Lerp(transform.position, info.pos, 1);
            unit[i].transform.rotation = Quaternion.Lerp(transform.rotation, info.rot, 1);

            if (unitList[i].curTime >= info.time) //i == unitList.Count && curTime >= info.time
            {
                print("�ð�" + info.time);
                //�� ��ü�� �ε��� ���̸�ŭ �߰��ؾ� �Ѵ�.
                //loadIndex++;
                unitList[i].unitLoadIndex++;

                Debug.Log($"��ü �̸� {unitList[i].unit},  �����ε��� {unitList[i].unitLoadIndex} , list ī��Ʈ �� {unitList[i].loadInfo.playerJsonList.Count}");

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

        //����� ����Ʈ�� 0��°����
        //PlayerInfo info = loadList[i].playerJsonList[loadIndex];
        PlayerInfo info = unitList[who].loadInfo.playerJsonList[unitList[who].unitLoadIndex];

        //�÷��� 
        /*unitList[i].unit.transform.position = info.pos;
        unitList[i].unit.transform.rotation = info.rot;*/

        unit[who].transform.position = Vector3.Lerp(transform.position, info.pos, 1);
        unit[who].transform.rotation = Quaternion.Lerp(transform.rotation, info.rot, 1);

        if (curTime >= info.time) //i == unitList.Count && curTime >= info.time
        {
            print("�ð�" + info.time);
            //�� ��ü�� �ε��� ���̸�ŭ �߰��ؾ� �Ѵ�.
            //loadIndex++;
            unitList[who].unitLoadIndex++;

            Debug.Log($"��ü �̸� {unitList[who].unit},  �����ε��� {unitList[who].unitLoadIndex} , list ī��Ʈ �� {unitList[who].loadInfo.playerJsonList.Count}");

            if (unitList[who].unitLoadIndex >= unitList[who].loadInfo.playerJsonList.Count)
            {
                isReplay = false;
                print("Stop" + unitList[who].loadInfo.playerJsonList.Count);
            }
        }
    }

    //��ȭ
    private void Recording(int who)
    {
        curTime += Time.deltaTime;

        totalTime += Time.deltaTime;

        //0.1�� �������� ����
        if(curTime > recordTime)
        {
            curTime -= recordTime;

            SavePlayerInfo(who);
        }
    }

    //����
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

    // ��ȭ ����
    public void OnRecordStart(int who)
    {
        print("��ȭ����" + who);
        this.who = who;
        isRecord = true;
        
        //ó������ ��ȭ
        saveList.playerJsonList.Clear();
    }

    public void OnRecordEnd()
    {
        print("��ȭ����" + who);

        isRecord = false;

        //������ ������ ?

        //���� ����
        string json = JsonUtility.ToJson(saveList, true);
        File.WriteAllText(Application.dataPath + "/save" + who + ".txt", json);
    }

    public void OnRecordPlay()
    {
        print("���÷���");

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
        print("���÷���");

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
