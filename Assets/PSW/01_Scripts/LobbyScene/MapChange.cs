using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 맵 1을 누르면 씬 변경
public class MapChange : MonoBehaviour
{
    /*public OVRInput.Button button;
    public OVRInput.Controller controller;*/
    
    public void OnClickMap(string name)
    {
        // Scene PSW_REC2 로 변경할 것
        //SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_BUTTON);

        if(name != null)
        {
            SceneManager.LoadScene(name);
            print("맵변경효과음");
        }
    }
}
