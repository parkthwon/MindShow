using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ǥ
//��� �� ����Ʈ�� ��� �÷��� �ϸ� �� ��ü�� �÷��� �Լ��� ����
//���� ��ȭ ��ư�� ������ �� ����� ����� ��ü�� �ִٸ�
//�÷��� �� �� �ְ�
//���� ��ȭ //�ʴ� �÷��� �� !!!!

//��ȭ ���
public class RecSet : MonoBehaviour
{
    // ��� �� ����Ʈ 
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
        //�ڽĿ� �پ��ִ� �÷��̾��� ��ȭ������Ʈ�� �����´�.
        recrod = transform.GetComponentInChildren<PlayerRecord>();

        mic = transform.GetComponentInChildren<Mic_LHS>();

        //recrod�� null�� �ƴҶ���
        if (recrod != null)
        {
            //���� ����� �ִ� ���÷��� ��ü�� �ִٸ� �����Ű�鼭 
            //�߿�! ���÷��̰� �ɶ� ��ȭ�� ����Ǿ���.
            Debug.Log("RM" + recrod + "�� ��ȭ����");
            //��ȭ �÷��̸� ��� ��Ŵ!
            recrod.OnRecordStart();
            mic.OnStart();
        }

        else
        {
            print("��ȭ�Ұ��� �÷��̾�");
        }
    }

    public void OnRecordEnd()
    {
        if (recrod != null)
        {
            Debug.Log("RM" + recrod + "�� ��ȭ����");
            recrod.OnRecordEnd();

            //��ȭ ������ ������ main �÷��̾�� �ٲ�� ��!
            //pm.targetPlayer = mainPlayer;
            UI.Player_State = UI.PlayerState.Normal;
            pm.CharChange(mainPlayer.gameObject);
            mic.OnEnd();
        }

        else
        {
            print("��ȭ�����Ұ� ����");
        }
    }

    public void OnRecording()
    {
        isRec = !isRec;

        //��ȭ��
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