using UnityEngine;
using RockVR.Video;

namespace RockVR.Rift.Demo
{
    enum CameraState
    {
        Normal,
        Touched,
        Picked
    }

    public class CameraSampleCtrl : MonoBehaviour
    {
        public ControllerState controllerState = ControllerState.Normal;

        //여러 카메라를 사용해서 가능
        public CameraSetUpCtrl cameraSetUpCtrl;
        public RIFT_FollowCamera[] followCameras;
        public GameObject oneButtonTooltip;
        public GameObject twoButtonTooltip;
        public GameObject indexTriggerTooltip;
        public GameObject hangTriggerTooltip;
        public GameObject thumbstickTooltip;
        public GameObject startButtonTooltip;
        private GameObject cameraObject;
        private CameraState cameraState = CameraState.Normal;
        private bool enableRadialMenu = false;
        private RIFT_Interaction vrIteraction;
        private RIFT_EventCtrl eventCtrl;
        private RIFT_TooltipManager tooltipController;
        private RIFT_Teleport teleport;
        protected RIFT_RadialMenu radiaMenu;

        //카메라 활성화 하는 기능
        void Awake()
        {
            //인터렉션 
            //vrIteraction = this.transform.GetComponent<RIFT_Interaction>();
            
            //이벤트 발생
            //eventCtrl = this.GetComponent<RIFT_EventCtrl>();
            
            //메뉴 읽기? ray camera에 있음
            //radiaMenu = this.transform.GetComponentInChildren<RIFT_RadialMenu>();

            //툴킷 매니저
            //tooltipController = this.GetComponentInChildren<RIFT_TooltipManager>();
            
            //텔레포트
            //teleport = this.GetComponent<RIFT_Teleport>();
        }

        private void Start()
        {
            //툴킷 UI 나타나게 하는 것.
            /*if (controllerState == ControllerState.Ray)
            {
                tooltipController.handTriggerText = "Show Ray";
                tooltipController.indexTriggerText = "Pick Camera";
                tooltipController.thumbstickText = "Swicth Camera";
                tooltipController.buttonOneText = "Start/Stop Capture";
            }

            else if (controllerState == ControllerState.Touch)
            {
                tooltipController.indexTriggerText = "Grab Camera";
                tooltipController.thumbstickText = "Teleport";
                tooltipController.buttonOneText = "Start/Stop Capture";
                hangTriggerTooltip.SetActive(false);
            }*/

            //UI 구성을 위한
            //startButtonTooltip.SetActive(false);
            //twoButtonTooltip.SetActive(false);
        }

        //활성화 될때 호출되는 함수
        void OnEnable()
        {
            print("델리게이트 활성화");
            if (eventCtrl != null)
            {
                /*eventCtrl.eventDelegate.OnPressButtonPrimaryHandTrigger += OnPressButtonPrimaryHandTrigger;
                eventCtrl.eventDelegate.OnPressButtonPrimaryHandTriggerUp += OnPressButtonPrimaryHandTriggerUp;
                eventCtrl.eventDelegate.OnPressButtonOneDown += OnPressButtonOneDown;
                eventCtrl.eventDelegate.OnPressButtonPrimaryIndexTrigger += OnPressButtonPrimaryIndexTrigger;
                eventCtrl.eventDelegate.OnPressButtonPrimaryIndexTriggerUp += OnPressButtonPrimaryIndexTriggerUp;
                eventCtrl.eventDelegate.OnTouchPrimaryThumbstick += OnTouchPrimaryThumbstick;
                eventCtrl.eventDelegate.OnTouchPrimaryThumbstickUp += OnTouchPrimaryThumbstickUp;
                eventCtrl.eventDelegate.OnPressPrimaryThumbstick += OnPressPrimaryThumbstick;
                eventCtrl.eventDelegate.OnPressPrimaryThumbstickDown += OnPressPrimaryThumbstickDown;
                eventCtrl.eventDelegate.OnPressPrimaryThumbstickUp += OnPressPrimaryThumbstickUp;*/
            }
        }

        //비활성화 될때 호출되는 함수
        void OnDisable()
        {
            print("델리게이트 비활성화");
            if (eventCtrl != null)
            {
                /*eventCtrl.eventDelegate.OnPressButtonPrimaryHandTrigger -= OnPressButtonPrimaryHandTrigger;
                eventCtrl.eventDelegate.OnPressButtonPrimaryHandTriggerUp -= OnPressButtonPrimaryHandTriggerUp;
                eventCtrl.eventDelegate.OnPressButtonOneDown -= OnPressButtonOneDown;
                eventCtrl.eventDelegate.OnPressButtonPrimaryIndexTrigger -= OnPressButtonPrimaryIndexTrigger;
                eventCtrl.eventDelegate.OnPressButtonPrimaryIndexTriggerUp -= OnPressButtonPrimaryIndexTriggerUp;
                eventCtrl.eventDelegate.OnTouchPrimaryThumbstick -= OnTouchPrimaryThumbstick;
                eventCtrl.eventDelegate.OnTouchPrimaryThumbstickUp -= OnTouchPrimaryThumbstickUp;
                eventCtrl.eventDelegate.OnPressPrimaryThumbstick -= OnPressPrimaryThumbstick;
                eventCtrl.eventDelegate.OnPressPrimaryThumbstickDown -= OnPressPrimaryThumbstickDown;
                eventCtrl.eventDelegate.OnPressPrimaryThumbstickUp -= OnPressPrimaryThumbstickUp;*/
            }
        }

        #region Ray모드 일때
        private void OnPressButtonPrimaryHandTriggerUp()
        {
            if (controllerState == ControllerState.Ray)
            {
                vrIteraction.show = false;
                hangTriggerTooltip.SetActive(false);
            }
        }

        private void OnPressButtonPrimaryHandTrigger()
        {
            if (controllerState == ControllerState.Ray)
            {
                vrIteraction.show = true;
            }
        }
        #endregion

        //조이스틱 
        //텔레포트
        private void OnPressPrimaryThumbstick()
        {
            if (controllerState == ControllerState.Touch)
            {
                if (teleport != null)
                {
                    teleport.SearchDropPoint();
                }
                thumbstickTooltip.SetActive(false);
            }
        }

        private void OnPressPrimaryThumbstickDown()
        {
            if (controllerState == ControllerState.Ray)
            {
                if (!radiaMenu) return;
                if (eventCtrl.axisAngle != 0)
                {
                    radiaMenu.InteractButton(eventCtrl.axisAngle, ButtonEvent.click);
                }
            }
        }

        private void OnTouchPrimaryThumbstickUp()
        {
            if (controllerState == ControllerState.Ray)
            {
                radiaMenu.StopTouching();
                radiaMenu.DisableMenu(false);
            }

            else if (controllerState == ControllerState.Touch)
            {
                if (teleport != null)
                {
                    teleport.ConfirmDropPoint();
                }
            }
        }

        private void OnTouchPrimaryThumbstick()
        {
            if (controllerState == ControllerState.Ray)
            {
                if (cameraState == CameraState.Picked && enableRadialMenu)
                {
                    radiaMenu.EnableMenu();
                    if (eventCtrl.axisAngle != 0)
                    {
                        radiaMenu.InteractButton(eventCtrl.axisAngle, ButtonEvent.hoverOn);
                    }
                    thumbstickTooltip.SetActive(false);
                }
            }
        }

        private void OnPressPrimaryThumbstickUp()
        {
            if (controllerState == ControllerState.Ray)
            {
                if (eventCtrl.axisAngle != 0)
                {
                    radiaMenu.InteractButton(eventCtrl.axisAngle, ButtonEvent.unclick);
                }
                enableRadialMenu = false;
            }
        }

        //------------------------------------ 카메라 -----------------------------//
        //카메라 녹화 기능 우리가 필요한 것!
        //녹화
        private void OnPressButtonOneDown()
        {
            if (cameraState == CameraState.Picked || controllerState == ControllerState.Touch)
            {
                if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.NOT_START ||
                    VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH)
                {
                    VideoCaptureCtrl.instance.StartCapture();
                    oneButtonTooltip.SetActive(false);
                }
                else if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STARTED)
                {
                    VideoCaptureCtrl.instance.StopCapture();
                }
                else if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STOPPED)
                {
                    return;
                }
            }
        }

        //카메라 잡기
        //카메라 켜기
        private void OnPressButtonPrimaryIndexTrigger()
        {
            if (vrIteraction.selectedObject != null)
            {
                if (vrIteraction.selectedObject.GetComponent<CameraSetUpCtrl>() != null)
                {
                    cameraSetUpCtrl.EnableCamera();

                    /*if (controllerState == ControllerState.Ray)
                    {
                        foreach (var followCamera in followCameras)
                        {
                            followCamera.followCamera = cameraSetUpCtrl.GetComponent<Camera>();
                        }
                        followCameras[1].OnCameraPointChange();
                        cameraSetUpCtrl.SetCameraScreen();
                        cameraState = CameraState.Picked;
                        enableRadialMenu = true;
                    }*/

                    if (controllerState == ControllerState.Touch)
                    {
                        cameraObject = vrIteraction.selectedObject;
                        cameraObject.transform.parent = this.transform;

                        cameraState = CameraState.Touched;
                    }

                    indexTriggerTooltip.SetActive(false);
                }
            }
        }

        //카메라 놨을때
        //카메라 끄기
        private void OnPressButtonPrimaryIndexTriggerUp()
        {
            if (controllerState == ControllerState.Touch)
            {
                // 카메라 오브젝트가 Null이 아니라면 부모에서 나가게 -> 우리는 껐다 켰다 할 것임
                if (cameraObject != null)
                {
                    cameraObject.transform.parent = null;
                }
            }
        }
    }
}