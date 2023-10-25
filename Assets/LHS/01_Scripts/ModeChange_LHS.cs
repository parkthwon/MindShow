using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Switch;

public static class UI
{
    public static PlayerState playerState;

    // UI 버튼에 따른 상태 전환
    public enum PlayerState
    {
        Normal,
        Move,
        Delete,
        Teleport,
        Camera,
        Hopin,
        Rec
    }

    public static PlayerState Player_State
    {
        get
        {
            return playerState;
        }

        set
        {
            playerState = value;
        }
    }
}

public class ModeChange_LHS : MonoBehaviour
{
    // UI GameObject array
    public GameObject[] modeUIImage;
    public OVRInput.Button buttonPlus;
    public OVRInput.Button buttonMinus;
    public OVRInput.Controller controller;

    public int mode_index = 0;
    public int modeListCount = 5;

    public GameObject uiTutorial;

    void Start()
    { 
        UI.Player_State = UI.PlayerState.Normal;
    }

    void Update()
    {
        //모드 별 UI
        if(UI.Player_State == UI.PlayerState.Normal)
        {
            //초기화 값
            ModeUI(5);
        }

        else if(UI.Player_State == UI.PlayerState.Delete)
        {
            ModeUI(0);
        }

        else if (UI.Player_State == UI.PlayerState.Move)
        {
            ModeUI(3);
        }

        else if (UI.Player_State == UI.PlayerState.Teleport)
        {
            ModeUI(2);
        }

        else if (UI.Player_State == UI.PlayerState.Hopin)
        {
            ModeUI(1);
        }

        else if (UI.Player_State == UI.PlayerState.Camera)
        {
            ModeUI(4);
        }

        //클릭하면 모드 전환하게 해야함
        /*if (OVRInput.GetDown(buttonPlus, controller))
        {
            print("R + hand 클릭");
            isCenter = !isCenter;

            if (isCenter)
            {
                enentSystem.rayTransform = centerEye;
                ovrGazePointer.rayTransform = centerEye;

                //UI도 꺼지면?
                modeUI.SetActive(true);
            }
            else
            {
                enentSystem.rayTransform = rightHand;
                ovrGazePointer.rayTransform = rightHand;

                modeUI.SetActive(false);
            }
        }*/

        if (OVRInput.GetDown(buttonPlus, controller))
        {
            plus = true;
            FadeOut();

            mode_index = (mode_index + 1) % modeListCount;
            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_BUTTON2);
        }
        
        //One 버튼을 누르면 왜 다시 실행되는지 해결 ... 하고 싶다...
        else if (OVRInput.GetDown(buttonMinus, controller))
        {
            minus = true;
            FadeOut();

            mode_index = (mode_index - 1 + modeListCount) % modeListCount;
            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_BUTTON2);
        }

        ModeSetting(mode_index);
        print("모드 변경 " + mode_index);
    }

    bool plus;
    bool minus;

    public void FadeOut()
    {
        if (uiTutorial)
        {
            if(plus == true && minus == true)
            {
                uiTutorial.GetComponent<FadeOutImage>().FadeOutStart();
            }
        }
    }

    public void ModeSetting(int num)
    {
        if(num == 0)
        {
            UI.Player_State = UI.PlayerState.Move;
        }

        else if(num == 1)
        {
            UI.Player_State = UI.PlayerState.Teleport;
        }

        else if( num == 2)
        {
            UI.Player_State = UI.PlayerState.Delete;
        }

        else if (num == 3)
        {
            UI.Player_State = UI.PlayerState.Hopin;
        }

        else if (num == 4)
        {
            UI.Player_State = UI.PlayerState.Camera;
        }
    }

    // -----------------------------------소원이 부분---------------------------------------// 
    // 기본 셋팅 값

    #region 모드 변경 버튼으로
    public void OnNormal()
    {
        print("기본 모드");
        UI.Player_State = UI.PlayerState.Normal;
    }

    // 플레이어 Delete 모드
    public void OnDelete()
    {
        print("삭제 모드 활성화");
        UI.Player_State = UI.PlayerState.Delete;
    }

    // 플레이어 Move 모드
    public void OnMove()
    {
        print("이동 모드 활성화");
        UI.Player_State = UI.PlayerState.Move;
    }

    // Player Teleport 모드
    public void OnTeleport()
    {
        print("텔레포트 모드 활성화");
        UI.Player_State = UI.PlayerState.Teleport;
    }

    // -----------------------------------현숙이 부분---------------------------------------// 

    // Player Hopin 모드
    public void OnHopIn()
    {
        print("들어가는 모드 활성화");
        UI.Player_State = UI.PlayerState.Hopin;
    }


    // Player Camera 모드
    public void OnCamera()
    {
        print("카메라 모드 활성화");
        UI.Player_State = UI.PlayerState.Camera;
    }
    #endregion

    // Input number = true
    // other number = false
    public void ModeUI(int num)
    {
        for (int i = 0; i < modeUIImage.Length; i++)
        {
            if (i == num)
            {
                modeUIImage[num].SetActive(true);
            }
            else 
            {
                modeUIImage[i].SetActive(false);
            }
        } 
    }
}