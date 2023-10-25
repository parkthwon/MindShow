using RockVR.Rift;
using RockVR.Rift.Demo;
using RockVR.Video;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandControl : MonoBehaviour
{
    //�޼� ��ư
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
            //ī�޶� , ��ȭ��� �϶� Ray �ȱ׸��� ����!
            if (UI.Player_State == UI.PlayerState.Camera)
            {
                print("ī�޶��ȭ��� - ��ư Ŭ��");
                Cam();
            }

            else if (UI.Player_State == UI.PlayerState.Rec)
            { 
                if (recSet != null)
                {
                    Destroy(uiTutorial);
                    uiRec.SetActive(false);
                    print("�÷��̾��ȭ��� - ��ư Ŭ��");
                    recSet.OnRecording();
                }
            }
        }
    }

    private void Cam()
    {
        print("ī�޶� Ȱ��ȭ");
        //UI ����
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
                print("ī�޶� ��ȭ����");
                VideoCaptureCtrl.instance.StartCapture();
                replaySet.OnRecordPlay();
                //oneButtonTooltip.SetActive(false);
            }
            else if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STARTED)
            {
                VideoCaptureCtrl.instance.StopCapture();
                print("ī�޶� ��ȭ����");
            }
            else if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STOPPED)
            {
                print("�ٽ� �ݺ�");
                return;
            }
        }
    }
}
