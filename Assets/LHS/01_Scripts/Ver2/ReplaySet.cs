using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���÷��� ���
public class ReplaySet : MonoBehaviour
{
    public static ReplaySet instance;

    //���ӿ�����Ʈ
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
        //��ġ�� �÷��̾ �� ���
        unit = GameObject.FindGameObjectsWithTag("Player");
    }

    // ����� �ִ� ���÷��̸� ��� ����! -> ���÷��� Ŭ�������� 
    public void OnRecordPlay()
    {
        if (unit.Length > 0)
        {
            //����� ��ü�� �Լ��� �ݺ��ؼ� �θ���.!
            for (int i = 0; i < unit.Length; i++)
            {
                var playerRecored = unit[i].GetComponent<PlayerRecord>();
                var playerMic = unit[i].GetComponent<Mic_LHS>();

                playerRecored.OnRecordPlay();
                playerMic.OnReplay();
            }

            print(unit.Length);
        }

        //���� ���ٸ� ����� �÷��̾ ���ٰ� ǥ��
        else
        {
            print("����� �� ����.");
        }
    }

    public void OnAutoReplayForRecording(PlayerRecord who)
    {
        if (unit.Length > 0)
        {
            //����� ��ü�� �Լ��� �ݺ��ؼ� �θ���.!
            for (int i = 0; i < unit.Length; i++)
            {
                var playerRecored = unit[i].GetComponent<PlayerRecord>();
                var playerMic = unit[i].GetComponent<Mic_LHS>();

                //�� ���� �ݺ�
                if (who == playerRecored)
                { 
                    continue;
                }

                playerRecored.OnRecordPlay();
                playerMic.OnReplay();
            }

            print(unit.Length);
        }

        //���� ���ٸ� ����� �÷��̾ ���ٰ� ǥ��
        else
        {
            print("����� �� ����.");
        }
    }
}
