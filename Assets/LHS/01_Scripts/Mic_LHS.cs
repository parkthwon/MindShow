using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

// 녹음해서 파일로 저장 // 게임오브젝트이름 , 녹음 (시간은?)
// 해당 게임오브젝트로 불러와야함 -> 리플레이 실행시 같이 재생 -> 오디오 소스로 생성
// 목소리 파형에 맞춰서 얼굴 움직임 가능하게 
public class Mic_LHS : MonoBehaviour
{
    //녹음파일
    AudioClip recording = null;
    //녹음파일이름
    string microphoneID = null;
    //생성된 길이
    int recordingLengthSec = 15;
    //샘플 속도
    int recordingHZ = 22050;

    PlayerRecord playerNum;

    void Update()
    {
        //PC 테스트
        /*if (Input.GetKeyDown(KeyCode.V))
        {
            print("음성 녹음 중");
            PointerDown();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            print("음성 녹음 종료");
            PointerUp();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            print("음성 리플레이");
            StartCoroutine(GetWav2AudioClip(Application.dataPath + "/StreamingAssets/Mic.wav"));
        }*/

        playerNum = this.GetComponent<PlayerRecord>();
    }

    public void OnStart()
    {
        print("음성 녹음 중");
        PointerDown();
    }

    public void OnEnd()
    {
        print("음성 녹음 종료");
        PointerUp();
    }

    public void OnReplay()
    {
        print("음성 리플레이");
        StartCoroutine(GetWav2AudioClip(Application.dataPath + "/StreamingAssets/" + gameObject.name + playerNum.myNum + ".wav"));
    }

    //로드하는 파일을 한번에 불러와서 들리게 해야한다. (액션 or 리스트로 담고 한번에 PlayOneShot ?)
    IEnumerator GetWav2AudioClip(string path)
    {
        Uri voiceURI = new Uri(path); //저장한 경로로

        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(voiceURI, AudioType.WAV);

        yield return www.SendWebRequest();

        // 응답 실패! -> 이거 안됨...
        /*if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            print(www.error);
        }*/

        //요청이 성공했는지 확인 -> 오류 해결!!!!
        if(www.result == UnityWebRequest.Result.Success)
        {
            AudioClip clipData = ((DownloadHandlerAudioClip)www.downloadHandler).audioClip;

            AudioSource audio = GetComponent<AudioSource>();

            if (audio)
            {
                audio.clip = clipData;
                audio.Play();
            }
        }

        else
        {
            //Debug.LogError("오디오 클립을 가져오는데 실패했습니다" + www.error);
            print("가져올 오디오 없음");
        }
    }

    public void PointerDown()
    { 
        StartRecording();
    }

    public void PointerUp()
    {
        //recorder.TransmitEnabled = false;
        //this.recorder.TransmitEnabled = false;
        StopRecording();
    }

    //녹화 시작
    public void StartRecording()
    {
        Debug.Log("녹화 시작");

        //장치 이름, lengthSec에 도달한 경우 녹음을 계속하고 AudioClip의 시작 부분부터 순환하여 녹음해야하는 여부 나타암
        recording = Microphone.Start(microphoneID, false, recordingLengthSec, recordingHZ);
    }

    //녹화 종료
    public void StopRecording()
    {
        //녹화파일이 레코딩 중이면 
        if(Microphone.IsRecording(microphoneID))
        {
            if(recording == null)
            {
                Debug.LogError("녹음 파일이 없다");
                return;
            }

            // 마지막 녹음 시점 가져오기
            int lastTime = Microphone.GetPosition(null);

            Microphone.End(microphoneID);
            Debug.Log("녹음 종료");

            //녹음한 샘플링 배열에 담기
            float[] sampleData = new float[recording.samples];
            recording.GetData(sampleData, 0);

            float[] cutData = new float[lastTime];

            //sampleData에 담긴 값을 lasTime 만큼만 cutData에 복사하기
            //array.copy(cutData, sampleData, lastTime -1);
            for(int i = 0; i < cutData.Length; i++)
            {
                cutData[i] = sampleData[i];
            }

            AudioClip newClip = AudioClip.Create("temp", lastTime, recording.channels, recording.frequency, false);
            newClip.SetData(cutData, 0);

            // 현재 시간을 구하기
            DateTime dt = DateTime.Now;

            //저장된 파일
            //SavWav_LHS.Save(Application.streamingAssetsPath + "/" + gameObject.name, newClip);
            SavWav_LHS.Save(Application.streamingAssetsPath + "/" + gameObject.name + playerNum.myNum, newClip);
        }
    }
}
