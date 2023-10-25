using RockVR.Rift;
using RockVR.Rift.Demo;
using RockVR.Video;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandControl : MonoBehaviour
{
    //왼손 버튼
    public OVRInput.Button button;
    public OVRInput.Controller controller;

    public CameraSetUpCtrl cameraSetUpCtrl;
    private CameraState cameraState = CameraState.Normal;
    public ControllerState controllerState = ControllerState.Normal;

    public ReplaySet replaySet;
    RecSet recSet;

    public GameObject uiTutorial;
    public GameObject uiRec;

    void Start()
    {
        recSet = gameObject.GetComponentInParent<RecSet>();    
    }

    void Update()
    {
        if (OVRInput.GetDown(button, controller))
        {
            //카메라 , 녹화모드 일때 Ray 안그리고 싶음!
            if (UI.Player_State == UI.PlayerState.Camera)
            {
                print("카메라녹화모드 - 버튼 클릭");
                Cam();
            }

            else if (UI.Player_State == UI.PlayerState.Rec)
            { 
                if (recSet != null)
                {
                    Destroy(uiTutorial);
                    uiRec.SetActive(false);
                    print("플레이어녹화모드 - 버튼 클릭");
                    recSet.OnRecording();
                }
            }
        }
    }

    private void Cam()
    {
        print("카메라 활성화");
        //UI 끄고
        cameraSetUpCtrl.EnableCamera();

        //cameraState = CameraState.Touched;
        controllerState = ControllerState.Touch;

        CamRec();
    }

    private void CamRec()
    {
        if (controllerState == ControllerState.Touch)
        {
            if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.NOT_START ||
                VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH)
            {
                print("카메라 녹화시작");
                VideoCaptureCtrl.instance.StartCapture();
                replaySet.OnRecordPlay();
                //oneButtonTooltip.SetActive(false);
            }
            else if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STARTED)
            {
                VideoCaptureCtrl.instance.StopCapture();
                print("카메라 녹화종료");
            }
            else if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STOPPED)
            {
                print("다시 반복");
                return;
            }
        }
    }
}
