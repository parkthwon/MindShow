using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //컨트롤러의 Thumbstick을 밀면 오른쪽 이동, 당기면 왼쪽이동.

    public GameObject home;
    public GameObject player;

    void Start()
    {
       
    }

    void Update()
    {
        ZoomProcess();
    }

    private void ZoomProcess()
    {
        // 컨트롤러의 Thumbstick을 밀면 축소, 당기면 확대하고싶다.(CameraScope의 FOV를 건드리겠다)

        Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);

        print(axis.y);
    }

    public void OnHome()
    {
        player.SetActive(false);
        home.SetActive(true);
    }

    public void OnPlayer()
    {
        home.SetActive(false);
        player.SetActive(true);
    }
}
