using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� 1�� ������ �� ����
public class MapChange : MonoBehaviour
{
    /*public OVRInput.Button button;
    public OVRInput.Controller controller;*/
    
    public void OnClickMap(string name)
    {
        // Scene PSW_REC2 �� ������ ��
        //SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_BUTTON);

        if(name != null)
        {
            SceneManager.LoadScene(name);
            print("�ʺ���ȿ����");
        }
    }
}
