using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    // * Player 1 눈 위치
    public Transform trEye;
    // OVR Rig 
    public Transform trOvrRig;
    // CenterEye
    public Transform trCenterEye;

    // * Model -> Rig Builder
    public RigBuilder rigBuilder;

    // * 이동해야 하는 Player
    public Transform targetPlayer;

    public List<GameObject> playerList;
    int player_index = 0;

    LHandTarget lt;
    RHandTarget rt;

    public bool isRecMove;

    //기준점이 될 위치
    Transform mainPos;

    public GameObject[] uiSetting;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mainPos = this.transform;
    }

    void Update()
    {
        //※소원 카메라 위치 셋팅
        Vector3 offset = trEye.position - trCenterEye.position;
        trOvrRig.position += offset;

        //※현숙추가부분(고정이 아닌 y축만 고정 후 이동할 수 있게) 녹화 시작할때만!
        /*Vector3 offset = trEye.position - trCenterEye.position;
        Vector3 newPosition2 = trOvrRig.position + new Vector3(0, offset.y, 0);
        trOvrRig.position = newPosition2;
*/
        // Vr 카메라의 위치와 회전을 플레이어에 적용
        //나의 자식으로 있는 플레이어로 계속 바뀌어야 함.
        CharacterModel myPlayer = transform.GetComponentInChildren<CharacterModel>();

        Quaternion newRotation = Quaternion.Euler(0, trCenterEye.rotation.eulerAngles.y, 0);
        myPlayer.transform.rotation = newRotation;

        //※현숙추가부분(위치추가) -> 녹화 시작할때만!
        // 카메라는 내 눈 앞에 있고 싶다. 하지만 카메라의 이동의 따라 이동하고 싶다.
        // 플레이어는 카메라의 x z축을 따라가지만 카메라는 trEye의 y축을 따라간다.
        if (isRecMove)
        {
            //켰을때 내 위치가 달라지기 때문에 -> 자꾸 앞으로 가는 오류 발생
            //현재 내 위치와 trCenterEye의 위치의 차이 만큼만 이동시키면 내 위치에서 움직이지 않을까?
            Vector3 newPosition = new Vector3(trCenterEye.transform.position.x, myPlayer.transform.position.y, trCenterEye.transform.position.z);
            myPlayer.transform.position = newPosition;
        }

        //※현숙추가부분(팔리깅)
        lt = transform.GetComponentInChildren<LHandTarget>();
        rt = transform.GetComponentInChildren<RHandTarget>();

        //player에 있을때는 따라할 수 있게
        lt.isTargeting = true;
        rt.isTargeting = true;
    }

    CharacterModel cm;
    public void CharChange(GameObject target)
    {

        targetPlayer = target.transform;
        //rigBuilder 를 비활성화 -> 안꺼도 됨.
        //rigBuilder.enabled = false;

        //player 나갈때는 따라할 수 없게
        lt.isTargeting = false;
        rt.isTargeting = false;

        //rigBuilder 를 이용해서 부모로부터 나가자
        rigBuilder.transform.SetParent(null);

        //targetPlayer = target.transform;
        //나의 위치를 targetPlayer 의 위치로 하자
        transform.position = targetPlayer.position;
        //나의 각도를 targetPlayer 의 각도로 하자
        transform.rotation = targetPlayer.rotation;
        //targetPlayer 에서 CharacterModel 를 가져오자.
        cm = targetPlayer.GetComponent<CharacterModel>();

        UISetting();

        //trEye 에 가져온 컴포넌트의 trEye 를 셋팅
        trEye = cm.trEye;

        //targetPlayer 에서 RigBuilder 를 가져오자. (지역 변수로 받아라)
        RigBuilder rb = targetPlayer.GetComponent<RigBuilder>();

        //가져온 컴포넌트 를 활성화
        rb.enabled = true;

        //targetPlayer 의 부모를 나로 하자
        targetPlayer.SetParent(transform);
        //targetPlayer 에 rigBuilder 의 transform 을 넣자.
        targetPlayer = rigBuilder.transform;
        //rigBuilder 에 위에 지역변수로 받아놨던 rigBuilder를 셋팅
        rigBuilder = rb;
    }

    public void UISetting()
    {
        //UI
        if (cm.CompareTag("Player"))
        {
            UI.Player_State = UI.PlayerState.Rec;

            print("녹화플레이어");
            //녹화 오브젝트활성화
            uiSetting[0].SetActive(true);
            uiSetting[1].SetActive(false);
            uiSetting[2].SetActive(false);
        }
        else if (cm.CompareTag("MainPlayer"))
        {
            //UI.Player_State = UI.PlayerState.Normal;
            print("메인플레이어");
            //오른쪽 UI 왼쪽 UI 둘다 활성화
            uiSetting[0].SetActive(false);
            uiSetting[1].SetActive(true);
            uiSetting[2].SetActive(true);
        }
    }

    //플레이어 변경 (보류)
    public void PlayerChange()
    {
        player_index = (player_index + 1) % playerList.Count;

        if (playerList != null)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                //같은 것은
                if (i == player_index)
                {
                    targetPlayer = playerList[i].transform;
                }

                //같지 않다
                else
                {

                }
            }
        }
    }
}
