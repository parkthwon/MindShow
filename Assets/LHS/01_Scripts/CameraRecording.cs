using UnityEngine;
using System.Collections;

using System.Linq;

//빌드셋팅 - Universal windows platform 설치
public class CameraRecording : MonoBehaviour
{
    private UnityEngine.Windows.WebCam.VideoCapture videoCapture;
    private bool isRecording = false;
    private string videoFileName = "MyVideo.mp4";

    // Start is called before the first frame update
    void Start()
    {
        StartRecording();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRecording)
        {
            // ���⿡�� ��ȭ ���� ������ �߰� ������ ������ �� �ֽ��ϴ�.
        }
    }

    public void StartRecording()
    {
        Resolution cameraResolution = UnityEngine.Windows.WebCam.VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        float cameraFramerate = UnityEngine.Windows.WebCam.VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();

        UnityEngine.Windows.WebCam.VideoCapture.CreateAsync(false, delegate (UnityEngine.Windows.WebCam.VideoCapture videoCapture)
        {
            if (videoCapture != null)
            {
                this.videoCapture = videoCapture;
                UnityEngine.Windows.WebCam.CameraParameters cameraParameters = new UnityEngine.Windows.WebCam.CameraParameters();
                cameraParameters.hologramOpacity = 0.0f;
                cameraParameters.frameRate = cameraFramerate;
                cameraParameters.cameraResolutionWidth = cameraResolution.width;
                cameraParameters.cameraResolutionHeight = cameraResolution.height;
                cameraParameters.pixelFormat = UnityEngine.Windows.WebCam.CapturePixelFormat.BGRA32;

                videoCapture.StartVideoModeAsync(cameraParameters, UnityEngine.Windows.WebCam.VideoCapture.AudioState.ApplicationAndMicAudio, OnStartedVideoCaptureMode);
            }
            else
            {
                Debug.LogError("Failed to create VideoCapture object.");
            }
        });
    }

    private void OnStartedVideoCaptureMode(UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult result)
    {
        if (result.success)
        {
            Debug.Log("Started Video Capture Mode.");
            print("녹화중");
            string videoFilePath = System.IO.Path.Combine(Application.persistentDataPath, videoFileName);

            videoCapture.StartRecordingAsync(videoFilePath, OnStartedRecording);
            isRecording = true;
        }
        else
        {
            Debug.LogError("Failed to start Video Capture Mode.");
        }
    }

    private void OnStartedRecording(UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult result)
    {
        if (result.success)
        {
            print("녹화성공");
            Debug.Log("Started Recording.");
        }
        else
        {
            Debug.LogError("Failed to start recording.");
        }
    }
}