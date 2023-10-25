using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // �̱���
    public static SoundManager instance;

    // SFX
    public enum ESfx
    {
        SFX_BUTTON,
        SFX_BUTTON2,
        SFX_BUTTON3,
        SFX_HopIn,
        SFX_Delete,
        SFX_Teleport
    }

    // ȿ���� �迭
    public AudioClip[] sfxs;

    // ������ҽ�
    public AudioSource audioSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    //SFX Play ���콺 Ŭ�� �� ������ �� �ڵ�
    public void PlaySFX(ESfx sfxIdx)
    {
        // ȿ������ ������ �ʰ�
        audioSfx.PlayOneShot(sfxs[(int)sfxIdx]);
    }
    //SoundManager.instance.PlaySF(SoundManager.ESfx.SFX_BUTTON);
}
