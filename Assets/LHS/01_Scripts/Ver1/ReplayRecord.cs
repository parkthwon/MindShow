using System.Collections.Generic;
using UnityEngine;

//�÷��̾�
public class ReplayRecord : MonoBehaviour
{
    //���÷��� �Ŵ���
    public ReplayManager replayManager;
    [SerializeField] string replayManagerName = "ReplayManager";

    //�� ��� �� ��Ȱ��ȭ �� �ʿ䰡 ���� ��ũ��Ʈ - ������ �ʿ��� ����!

    //����� �ִϸ����� , �� ������ �ٵ�, ����� �ҽ� 
    [SerializeField] Animator anim;

    //������ ������ ��� (��ü, �ִϸ��̼�)
    List<Frame> frames;
    List<AnimationRecord> anim_records;

    //��������
    int max_lenght;
    //��ȭ�� ����
    int length;

    //�� �������� ������ -1�� ����, �����Ǹ� ������ �������� ���
    //���� ������
    int frame_index = -1;

    void Start()
    {
        //�������� �ֱ�
        if (replayManager == null)
        {
            replayManager = GameObject.Find(replayManagerName).GetComponent<ReplayManager>();
        }

        anim = GetComponentInChildren<Animator>();

        if (replayManager != null) //-> ���߿� ��ȭ ��ư ��������
        {
            //�����ϸ� replaymanager list�� ����
            replayManager.AddRecord(this);

            //ó�� ���̴� replayManager�� ������ max���̿� ����
            max_lenght = replayManager.max_length;

            //������ ���� list ����
            frames = new List<Frame>();
        }
    }
    float curTime;
    float saveTime = 1 / 10f;

    void Update()
    {
        /*curTime += Time.deltaTime;

        if (Game.Game_Mode == Game.Game_Modes.RECORD)
        // ��ȭ ����� ��
        {
            if (curTime > saveTime)
            {
                #region �ִϸ��̼�
                //�� ��� Update������ �ұ�?
                //anim_records = new List<AnimationRecord>();

                //�� �ִϸ��̼� ��������
                //�ִϸ��̼��� null�� �ƴ϶��
                *//*if (anim != null)
                {
                    //�� �迭�� �ݺ��� -> ���� �Ķ������ �̸��� �ִ´�.
                    foreach (AnimatorControllerParameter item in anim.parameters)
                    {
                        string name = item.name;

                        //���� ������ Ÿ���� �ִϸ��̼� ��Ʈ�ѷ� �Ķ���� bool�� ������ 
                        if (item.type == AnimatorControllerParameterType.Bool)
                        {
                            //�����ؼ� �־���� �Ѵ�. //item.defaultBool �⺻ bool ��?
                            anim_records.Add(new AnimationRecord(name, anim.GetBool(name), item.type));
                        }

                        if (item.type == AnimatorControllerParameterType.Float)
                        {
                            anim_records.Add(new AnimationRecord(name, anim.GetFloat(name), item.type));
                        }

                        if (item.type == AnimatorControllerParameterType.Int)
                        {
                            anim_records.Add(new AnimationRecord(name, anim.GetInteger(name), item.type));
                        }
                    }
                }*//*
                #endregion
                saveTime = curTime + 0.1f;

                Frame frame = new Frame(this.gameObject, transform.position, transform.rotation, transform.localScale, anim_records);
                AddFrame(frame);

                //Debug.Log("��ȭ���" + length);
            }
        }*/

        if (Game.Game_Mode == Game.Game_Modes.RECORD)
        // ��ȭ ����� ��
        {
            #region �ִϸ��̼�
            //�� ��� Update������ �ұ�?
            //anim_records = new List<AnimationRecord>();

            //�� �ִϸ��̼� ��������
            //�ִϸ��̼��� null�� �ƴ϶��
            /*if (anim != null)
            {
                //�� �迭�� �ݺ��� -> ���� �Ķ������ �̸��� �ִ´�.
                foreach (AnimatorControllerParameter item in anim.parameters)
                {
                    string name = item.name;

                    //���� ������ Ÿ���� �ִϸ��̼� ��Ʈ�ѷ� �Ķ���� bool�� ������ 
                    if (item.type == AnimatorControllerParameterType.Bool)
                    {
                        //�����ؼ� �־���� �Ѵ�. //item.defaultBool �⺻ bool ��?
                        anim_records.Add(new AnimationRecord(name, anim.GetBool(name), item.type));
                    }

                    if (item.type == AnimatorControllerParameterType.Float)
                    {
                        anim_records.Add(new AnimationRecord(name, anim.GetFloat(name), item.type));
                    }

                    if (item.type == AnimatorControllerParameterType.Int)
                    {
                        anim_records.Add(new AnimationRecord(name, anim.GetInteger(name), item.type));
                    }
                }
            }*/
            #endregion

            Frame frame = new Frame(this.gameObject, transform.position, transform.rotation, transform.localScale, anim_records);
            AddFrame(frame);

            //Debug.Log("��ȭ���" + length);

        }
    }

    //��ȭ���
    //���1. ���̰� �������� ��ȭ, �ƴ� ����
    //���2. ó�� ���� ���� �� length���̸� �ϳ� �پ��� ��
    void AddFrame(Frame frm)
    {
        //��ȭ ���� ��������
        if (length <= max_lenght)
        {
            //�������� �����ش�
            frames.Add(frm);

            //��ȭ�� ���̵� ������
            length++;

            Debug.Log("[��ȭ] ������ ADD");
        }

        //��ȭ ������ ���ٸ�
        else
        {
            Debug.Log("��ȭ ����");
            //�ٽ� 3��Ī �������� �ٲ��� ��.

            //���2
            //RemoveAt(index) �ش� �ε����� �ִ� ���� ����
            /*frames.RemoveAt(0);
            length = max_lenght - 1;*/
        }

        /*//���2 �������� �����ش� 
        frames.Add(frm);
        //�� ���̸� �����ش�.
        length++;*/
    }

    //Replay��� 2 >> 
    public void Play()
    {
        Frame frm;

        //frm�� Get_Frame()�϶� null�� �ƴ϶�� [frm = Get_Frame() -> ���� �������� ��ġ]
        if ((frm = Get_Frame()) != null)
        {
            Debug.Log("RR ���÷��� ���.");

            transform.position = frm.Position;
            transform.rotation = frm.Rotation;
            transform.localScale = frm.Scale;

            //transform.position = Vector3.Lerp(transform.position, frm.Position, Time.deltaTime * 10);
            //transform.rotation = Quaternion.Lerp(transform.rotation, frm.Rotation, Time.deltaTime * 10);
            //transform.localScale = Vector3.Lerp(transform.localScale, frm.Scale, Time.deltaTime * 10);

            #region �ִϸ��̼�
            /*foreach (var item in frm.Animation_Records)
            {
                string name = item.Name_;
                
                //����� �ִϸ��̼��� �÷��� �Ѵ�.
                if(item.Type == AnimatorControllerParameterType.Bool)
                {
                    anim.SetBool(name, item.Bool_);
                    //���� �ܰ踦 �ߴ�.
                    continue;
                }

                else if (item.Type == AnimatorControllerParameterType.Int)
                {
                    anim.SetInteger(name, item.Int_);
                    continue;
                }

                else if (item.Type == AnimatorControllerParameterType.Float)
                {
                    anim.SetFloat(name, item.Float_);
                    continue;
                }
            }*/
            #endregion
        }

        //����� �������� ���� ��
        else
        {
            Debug.Log("����� ������ �����ϴ�.");

            Game.Game_Mode = Game.Game_Modes.PAUSE;
        }
    }

    //Replay��� 1 >> 
    Frame Get_Frame()
    {
        Debug.Log("RR ���÷��� - getFrame");

        frame_index++;

        //������� �����̶��
        if (Game.Game_Mode == Game.Game_Modes.PAUSE)
        {
            //�� ���� �����ӿ��� ������ �� �ֵ��� --
            frame_index--;

            Debug.Log("�������");
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
            Debug.Log("���");
        }

        //������������ ��ȭ�� ���̺��� ũ�ų� ������
        if (frame_index >= length)
        {
            Debug.Log("��� �� ��");

            //����
            Game.Game_Mode = Game.Game_Modes.PAUSE;

            //���� ��ȭ �� �� ���������ӿ� ���� �� �� �ֵ���
            frame_index = length - 1;

            //null�� ��ȯ��Ų��
            return null;
        }

        //���� �������� -1 �̶��
        if (frame_index == -1)
        {
            Debug.Log("��� �غ�");

            //�̰� �Ⱦ��� ������. ��ȭ�� ���̵� 0���� ������ �� �ְ�
            frame_index = length - 1;
            return null;
        }

        Debug.Log($"{gameObject.name} ���� : ���� ������ {frame_index} / �ִ� ���� {max_lenght} / ��ȭ�� ���� {length}");

        return frames[frame_index];
    }

    //���� ������ ���� ���
    public void SetFrame(int value)
    {
        frame_index = value;
    }

    //�������ε��� �Ѱ��ֱ� -> ���� �������� ��ġ�� �Ѱ��ֱ� ����
    public int GetFrame()
    {
        return frame_index;
    }

    //�����̴��� ���� ��ȭ�� ���̸� �Ѱ���� �Ѵ�.
    public int Lenght
    {
        get { return length; }
    }
}
