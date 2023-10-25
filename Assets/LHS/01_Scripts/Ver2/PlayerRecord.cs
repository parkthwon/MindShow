using System.Collections.Generic;
using System.IO;
using UnityEngine;

//��ü���� ������ ���� ��ȭ ���
public class PlayerRecord : MonoBehaviour
{
    //��ȭ����
    bool isRecord;
    bool isReplay;

    // �ε� �� ��ü
    int loadIndex = 0;

    //�ð�
    float totalTime;
    float curTime = 0;
    float recordTime = 0.1f;

    //��ȭ�� ��ü���� ���� List
    PlayerJsonList<PlayerInfo> saveList;

    //�� ������Ʈ
    LHandTarget lHand;
    RHandTarget rHand;

    //������ ��ȣ�� ��!
    public int myNum;

    private void Start()
    {
        // ������ �� ��ȭ�� �ƴ�
        isRecord = false;

        //List  ����
        //���� �� List
        saveList = new PlayerJsonList<PlayerInfo>();
        saveList.playerJsonList = new List<PlayerInfo>();

        lHand = transform.GetComponentInChildren<LHandTarget>();
        rHand = transform.GetComponentInChildren<RHandTarget>();
    }

    private void Update()
    {
        //��ȭ��
        if (isRecord)
        {
            Recording();
        }

        //���÷�����
        else if (isReplay)
        {
            //��� �ÿ��� �� Ÿ�� ����� ��!
            lHand.isTargeting = false;
            rHand.isTargeting = false;

            Replaying();
        }
    }

    //���� ����� ������ �ҷ����� ��.
    private void Replaying()
    {
        curTime += Time.deltaTime;

        //0���� ���� 
        PlayerInfo info = saveList.playerJsonList[loadIndex];

        transform.position = Vector3.Lerp(transform.position, info.pos, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, info.rot, 1);

        lHand.transform.position = info.leftHand.pos;
        lHand.transform.rotation = info.leftHand.rot;

        rHand.transform.position = info.rightHand.pos;
        rHand.transform.rotation = info.rightHand.rot;

        if (curTime >= info.time) //i == unitList.Count && curTime >= info.time
        {
            print("�ð�" + info.time);
            //�� ��ü�� �ε��� ���̸�ŭ �߰��ؾ� �Ѵ�.
            loadIndex++;

            Debug.Log($"��ü �̸� {info.name},  �����ε��� {loadIndex} , list ī��Ʈ �� {saveList.playerJsonList.Count}");

            if (loadIndex >= saveList.playerJsonList.Count)
            {
                isReplay = false;
                print("Stop" + saveList.playerJsonList.Count);
            }
        }
    }

    //��ȭ
    private void Recording()
    {
        curTime += Time.deltaTime;

        totalTime += Time.deltaTime;

        //0.1�� �������� ����
        if (curTime > recordTime)
        {
            curTime -= recordTime;

            SavePlayerInfo();
        }
    }

    //����
    private void SavePlayerInfo()
    {
        LeftHandInfo leftHandInfo = new LeftHandInfo()
        {
            pos = lHand.transform.position,
            rot = lHand.transform.rotation
        };

        RightHandInfo rightHandInfo = new RightHandInfo()
        {
            pos = rHand.transform.position,
            rot = rHand.transform.rotation
        };

        PlayerInfo info = new PlayerInfo()
        {
            name = gameObject.name,
            time = totalTime,
            pos = transform.position,
            rot = transform.rotation,

            leftHand = leftHandInfo,
            rightHand = rightHandInfo
        };

        saveList.playerJsonList.Add(info);
    }


    // ��ȭ ����
    public void OnRecordStart()
    {
        //ó������ ��ȭ
        saveList.playerJsonList.Clear();

        Debug.Log(gameObject.name + "��ȭ����");
        isRecord = true;

        //�� ��ȭ�� �� ����� �÷��̾ ������ 
        if (ReplaySet.instance.unit.Length > 0)
        {
            //���� ���ӿ�����Ʈ ����
            ReplaySet.instance.OnAutoReplayForRecording(this);
            print("��ȭ�� ����÷��̾ �ִ�");
        }

        else
        {
            print("��ȭ�� ����÷��̾ ����");
        }
    }


    public void OnRecordEnd()
    {
        Debug.Log(gameObject.name + "��ȭ����");

        isRecord = false;

        //���� ����
        string json = JsonUtility.ToJson(saveList, true);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/" + gameObject.name + myNum + ".txt", json);
    }

    public void OnRecordPlay()
    {
        print("���÷���");

        if (isRecord)
        {
            OnRecordEnd();
        }

        //����� ������ ���� �ô� ���� ���о��...!
        //string json = File.ReadAllText(Application.dataPath + "/save" + gameObject.name + ".txt");

        string filePath = Application.dataPath + "/StreamingAssets/" + gameObject.name + myNum + ".txt";

        //������ �ִٸ�
        if (File.Exists(filePath))
        {
            //���÷��� ����
            isReplay = true;
            isRecord = false;

            string json = File.ReadAllText(filePath);
            saveList = JsonUtility.FromJson<PlayerJsonList<PlayerInfo>>(json);
            loadIndex = 0;
            curTime = 0;
            totalTime = 0;
        }

        else
        {
            Debug.Log($"{gameObject.name} �о�� ������ �����ϴ�.");
        }
    }
}
