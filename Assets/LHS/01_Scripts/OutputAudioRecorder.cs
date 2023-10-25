using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class OutputAudioRecorder : MonoBehaviour
{
    private AudioClip clip;

    string[] micList;

    void Awake()
    {
        micList = Microphone.devices;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            clip = Microphone.Start(micList[0], true, 30, 44100);
            print("녹음시작");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Microphone.End(micList[0]);
            print("녹음중단"); // 중단시 바로 파일 저장 되게 해야함 .
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SaveAudioClipToWAV(Application.dataPath + "/test1.wav");
            print("파일저장");
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SaveMic();
        }
    }

    public void SaveAudioClipToWAV(string filePath)
    {
        if (clip == null)
        {
            Debug.LogError("No AudioClip assigned.");
            return;
        }

        // AudioClip의 오디오 데이터 가져오기
        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);

        // WAV 파일 헤더 작성
        using (FileStream fs = File.Create(filePath))
        {
            WriteWAVHeader(fs, clip.channels, clip.frequency, clip.samples);
            ConvertAndWrite(fs, samples);
        }

        Debug.Log("AudioClip saved as WAV: " + filePath);
    }

    // WAV 파일 헤더 작성
    private void WriteWAVHeader(FileStream fileStream, int channels, int frequency, int sampleCount)
    {
        var samples = sampleCount * channels;
        var fileSize = samples + 36;

        fileStream.Write(new byte[] { 82, 73, 70, 70 }, 0, 4); // "RIFF" 헤더
        fileStream.Write(BitConverter.GetBytes(fileSize), 0, 4);
        fileStream.Write(new byte[] { 87, 65, 86, 69 }, 0, 4); // "WAVE" 헤더
        fileStream.Write(new byte[] { 102, 109, 116, 32 }, 0, 4); // "fmt " 헤더
        fileStream.Write(BitConverter.GetBytes(16), 0, 4); // 16
        fileStream.Write(BitConverter.GetBytes(1), 0, 2); // 오디오 포맷 (PCM)
        fileStream.Write(BitConverter.GetBytes(channels), 0, 2); // 채널 수
        fileStream.Write(BitConverter.GetBytes(frequency), 0, 4); // 샘플 레이트
        fileStream.Write(BitConverter.GetBytes(frequency * channels * 2), 0, 4); // 바이트 레이트
        fileStream.Write(BitConverter.GetBytes(channels * 2), 0, 2); // 블록 크기
        fileStream.Write(BitConverter.GetBytes(16), 0, 2); // 비트 레이트
        fileStream.Write(new byte[] { 100, 97, 116, 97 }, 0, 4); // "data" 헤더
        fileStream.Write(BitConverter.GetBytes(samples), 0, 4);
    }

    // 오디오 데이터 변환 및 작성
    private void ConvertAndWrite(FileStream fileStream, float[] samples)
    {
        Int16[] intData = new Int16[samples.Length];
        // float -> Int16 변환
        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * 32767);
        }
        // Int16 데이터 작성
        Byte[] bytesData = new Byte[intData.Length * 2];
        Buffer.BlockCopy(intData, 0, bytesData, 0, bytesData.Length);
        fileStream.Write(bytesData, 0, bytesData.Length);
    }

    public void SaveMic()
    {
        //File.WriteAllBytes(Application.streamingAssetsPath + "/test1.wav", data);
        StartCoroutine(GetWav2AudioClip(Application.streamingAssetsPath + "/test1.wav"));
    }

    IEnumerator GetWav2AudioClip(string path)
    {
        Uri voiceURI = new Uri(path);
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(voiceURI, AudioType.WAV);

        yield return www.SendWebRequest();

        AudioClip clipData = ((DownloadHandlerAudioClip)www.downloadHandler).audioClip;

        AudioSource audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.clip = clipData;
            audio.Play();
        }
    }
}
