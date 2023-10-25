using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Game
{
    public static Game_Modes Game_mode;

    public enum Game_Modes
    {
        PLAY, //���
        PAUSE, //����
        RECORD, //����
        REPLAY, //���÷���
        Slider, //�����̴�
        Exit, //������
        IDLE
    }

    public static Game_Modes Game_Mode
    {
        get
        {
            return Game_mode;
        }

        set
        {
            Game_mode = value;
        }
    }
}

//ī�޶�
//�÷��̵� ��ü�� ������ List�� ���� ��ƾ� �Ѵ�.
public class ReplayManager : MonoBehaviour
{
    //�÷��� �� ���۵��� ���ӿ�����Ʈ�� List�� ��Ƽ� ����
    List<ReplayRecord> replay_records;

    //��ȭ�� �ִ� ����
    public int max_length = 100;

    public Slider slider;
    bool slider_controlling;

    //ī�޶� ����Ʈ
    int camera_index = 0;
    public List<Camera> kameralar;

    //�� -> VR�� �ȵ� 
    public float maximum_zoom = 150f; 
    public float minumum_zoom = 10f;
    //��ŭ �پ�������
    public float zoom_index = 5f;
    public float rotation_index = 2f;

    public Canvas cnvs;

    //Start�ϸ� ���� �Ǽ� �����ڿ���? -> Awake
    public ReplayManager()
    {
        
    }

    public void Awake()
    {
        replay_records = new List<ReplayRecord>();

        //�����Ҷ� ��ȭ��� -> ��ư�� ������ ����� �� �ְ� ��
        Game.Game_Mode = Game.Game_Modes.IDLE;

        //ó���� ��Ʈ�ѷ� false;
        slider_controlling = false;
    }

    void Update()
    {
        #region Input �ý��� (ī�޶�)
        //ī�޶� ���� ����
        if (Input.GetKeyDown(KeyCode.V))
        {
            Camera_Change();
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            Zoom();
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            UnZoom();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Camera_rot_up();
        }

        if (Input.GetKey(KeyCode.E))
        {
            Camera_rot_down();
        }

        if (Input.GetKey(KeyCode.R))
        {
            Camera_rot_left();
        }

        if (Input.GetKey(KeyCode.T))
        {
            Camera_rot_right();
        }
        #endregion

        // ĵ���� -> �����ؼ� ���� �� �ְ�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Ȱ��ȭ �������� -> ��Ȱ��ȭ��
            if(cnvs.enabled)
            {
                cnvs.enabled = false;
                Exit(); //->��ȭ�� ��
            }

            //��Ȱ��ȭ�� -> Ȱ��ȭ��
            else
            {
                cnvs.enabled = true;
                Replay(); //-> ���÷��� ����
            }
        }

        //���Ӹ�尡 ���ڵ尡 �ƴ϶�� �÷��� ���� (��ȭ���� �ƴ� ��) Game.Game_Mode != Game.Game_Modes.RECORD
        if (Game.Game_Mode == Game.Game_Modes.REPLAY || Game.Game_Mode == Game.Game_Modes.PLAY)
        {
            // ���÷��� , �÷��� ��� �϶� 
            Debug.Log("���� ���� ���" + Game.Game_Mode);

            // ����� ��� replayrecord�� ������ ��� ��Ű�� 
            foreach (ReplayRecord item in replay_records)
            {
                //�����̴��� Ŭ������ ���� ���� �Ѱ��ش�. -> ���� �������� �ǵ���
                if (slider_controlling)
                {
                    //�����̴��� ���� int������ ����ȯ
                    item.SetFrame(Convert.ToInt32((slider.value)));
                }

                else
                {
                    //�� ��� �������� �ٸ� �ٵ� ? -> �ִ��� ���̷� ������ ��ü��
                    //item �������� �о�´�.
                    slider.value = item.GetFrame();
                    //�����̴��� �ִ���̴� ��ȭ�� ���̸�ŭ
                    slider.maxValue = item.Lenght;
                }

                if (Game.Game_Mode == Game.Game_Modes.REPLAY)
                {
                    Debug.Log("RM ���÷��� ���");

                    //��� �Ǵ� �߿� replay���� ó������ ������ �� �ְ�
                    item.SetFrame(-1);
                    //item.Play();
                }

                if (Game.Game_Mode == Game.Game_Modes.Exit)
                {
                    //��ȭ�� ���̿��� -2 �� ���� ����
                    //Get_Frame()���� frame_index++ ���ֱ� ������
                    item.SetFrame(item.Lenght - 2);
                    Debug.Log("RM Exit ���");
                }

                item.Play();
            }
        }

        //���÷��� ���� ������� 
        if (Game.Game_Mode == Game.Game_Modes.REPLAY)
        {
            Game.Game_Mode = Game.Game_Modes.PLAY;
        }
         
        if(Game.Game_Mode == Game.Game_Modes.Exit)
        {
            //Game.Game_Mode = Game.Game_Modes.RECORD;

            //���� ����
            Time.timeScale = 1;
        }

        //��ȭ��
        if(Game.Game_Mode == Game.Game_Modes.RECORD)
        {
            Time.timeScale = 1;
        }
    }

    public void Camera_Change()
    {
        //���������� ī�޶� �ε��� �߰� ��������
        camera_index = (camera_index + 1) % kameralar.Count;

        //ī�޶� ����Ʈ�� null�� �ƴ϶��
        if (kameralar != null)
        {
            for (int i = 0; i < kameralar.Count; i++)
            {
                //ī�޶� Ű��
                if (i == camera_index)
                {
                    kameralar[i].enabled = true;
                }

                else
                {
                    kameralar[i].enabled = false;
                }
            }
        }
    }

    #region ī�޶� -> ���� ������ ��� ��� ..? 
    //ī�޶� �ܱ�� VR�� �� �ƿ� �ȵ�!
    public void Zoom()
    {
        //min �ܺ��ٴ� Ŭ�� 
        if(kameralar[camera_index].fieldOfView > minumum_zoom)
        {
            kameralar[camera_index].fieldOfView -= zoom_index;
        }
    }

    public void UnZoom()
    {
        //min �ܺ��ٴ� Ŭ�� 
        if (kameralar[camera_index].fieldOfView < maximum_zoom)
        { 
            kameralar[camera_index].fieldOfView += zoom_index;
        }
    }

    //���Ѱ��� �ɾ������
    public void Camera_rot_up()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.x = (rot.x + rotation_index) % 360; //360 �� �� ��������
        kameralar[camera_index].transform.localEulerAngles = rot;
    }

    public void Camera_rot_down()
    { 
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.x = (rot.x - rotation_index) % 360; //360 �� �� ��������
        kameralar[camera_index].transform.localEulerAngles = rot;
    }

    public void Camera_rot_left()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.y = (rot.y - rotation_index) % 360; //360 �� �� ��������
        kameralar[camera_index].transform.localEulerAngles = rot;
    }

    public void Camera_rot_right()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.y = (rot.y + rotation_index) % 360; //360 �� �� ��������
        kameralar[camera_index].transform.localEulerAngles = rot;
    }
    #endregion

    //ReplayRecord ���� �� �� �ڱ� ����.
    public void AddRecord(ReplayRecord rec)
    {
        replay_records.Add(rec);
    }

    //����
    public void Pause()
    {
        Game.Game_Mode = Game.Game_Modes.PAUSE;

        //�ð� ���߱� -> ���� ���������� ���߱�?
        Time.timeScale = 0;
    }

    //�÷���
    public void Play()
    {
        Game.Game_Mode = Game.Game_Modes.PLAY;
        
        //���� ����
        Time.timeScale = 1;
    }

    //���÷���
    public void Replay()
    {
        Game.Game_Mode = Game.Game_Modes.REPLAY;
        
        //���� ����
        Time.timeScale = 1;
    }

    //��ȭ
    public void Exit()
    {
        Game.Game_Mode = Game.Game_Modes.Exit;
    }

    //��ȭ��
    public void Rec()
    {
        Game.Game_Mode = Game.Game_Modes.RECORD;
    }

    //�����̴� Ŭ�������� Pointer Down
    public void Click_Slider()
    {
        slider_controlling = true;
    }

    //Pointer Up
    public void Slider_Breack()
    {
        slider_controlling = false;
        //�ٽ� ����ؾ���.
        Play();
    }
}

