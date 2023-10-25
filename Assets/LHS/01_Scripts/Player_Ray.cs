using UnityEngine;

public class Player_Ray : MonoBehaviour
{
    public OVRInput.Button button;
    //public OVRInput.Button uIButton;
    public OVRInput.Controller controller;

    //오른손에서 그려지는 ray
    public Transform hand;
    LineRenderer lr;

    // 플레이어를 배치 하기 위해 필요한 요소
    public float maxLineDistance = 3f; // Ray에 최대 길이
    public GameObject cube; // 플레이어의 부모가 될 게임 오브젝트

    // 플레이어를 Teleport할 때 필요한 요소
    public GameObject player;
    public Transform marker; // marker
    // 마커 크기 조절하는 공식 변수(?)
    public float kAdjust = 0.1f;

    //카메라
    //public CameraSetUpCtrl cameraSetUpCtrl;
    public GameObject cameraObj;
    public GameObject leftUI;

    RaycastHit hitInfo;
    //가지고 있는 플레이어
    GameObject inPlayer;
    //따라다니게 하는 코드
    bool isPlayerPut;

    // 커지게 될 UI
    public float uiScale = 2f;
    Transform objHit;

    public GameObject uiTutorial;

    Highlight playerHight;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        marker.localScale = Vector3.one * kAdjust;
    }

    void Update()
    {
        //카메라 , 녹화모드 일때 Ray 안그리고 싶음!
        if (UI.Player_State == UI.PlayerState.Camera)
        {
            print("Ray 그리지 않을 것임");

            //라인렌더러 . 마커 끄기
            lr.enabled = false;
            marker.gameObject.SetActive(false);

            cameraObj.SetActive(true);
            leftUI.SetActive(false);
        }

        else if (UI.Player_State == UI.PlayerState.Rec)
        {
            //라인렌더러 . 마커 끄기
            lr.enabled = false;
            marker.gameObject.SetActive(false);
        }

        //나머지 State
        else
        {
            cameraObj.SetActive(false);
            leftUI.SetActive(true);

            // 손위치에서 손의 앞방향으로 Ray를 만들고
            Ray ray = new Ray(hand.position, hand.forward);
            bool isHit = Physics.Raycast(ray, out hitInfo);

            //라인렌더러, 마커 켜기
            lr.enabled = true;
            lr.SetPosition(0, hand.position);
            marker.gameObject.SetActive(true);


            /*if (hitInfo.collider != null)
            {
                hitInfo.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            }*/


            //닿는 곳이 있다면
            if (isHit)
            {
                //hitInfo.collider.GetComponent<Highlight>()?.ToggleHighlight(true);

                lr.SetPosition(1, hitInfo.point);
                // 큐브의 위치가 레이에 닿은 위치이다.(큐브를 계속 따라다니게 하고 싶다.)
                cube.transform.position = hitInfo.point;

                marker.position = hitInfo.point;
                marker.up = hitInfo.normal;
                marker.localScale = Vector3.one * kAdjust * hitInfo.distance;

                if (hitInfo.collider.CompareTag("Ground"))
                {
                    //땅일때만 보이게
                    marker.gameObject.SetActive(true);
                }

                else
                {
                    marker.gameObject.SetActive(false);
                }

                //플레이어 배치
                PlayerPlace();


                /*if (playerHight == null)
                {
                    Collider collider = hitInfo.collider;
                    if (collider != null)
                    {
                        playerHight = collider.GetComponent<Highlight>();
                        if (playerHight != null)
                        {
                            playerHight.ToggleHighlight(true);
                        }
                    }

                }

               else
               {
                    //hitInfo.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
                    playerHight.ToggleHighlight(false);
                    //playerHight = null;
               }*/

                //--------------------------------------- UI -----------------------------------------//

                /*if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("RayUI"))
                {
                    objHit = hitInfo.transform;
                    objHit.localScale = new Vector3(uiScale, uiScale, uiScale);
                }

                else
                {
                    if (objHit != null)
                    {
                        objHit.localScale = Vector3.one;
                        objHit = null;
                        print("닿지 않는다." + objHit);
                    }
                }*/

                //--------------------------------------- 버튼 -----------------------------------------//

                // 부딪힌 곳이 있다면 클릭 //인덱스 트리거
                if (OVRInput.GetDown(button, controller))
                {
                    if(uiTutorial)
                    {
                        uiTutorial.GetComponent<FadeOutImage>().FadeOutStart();
                    }

                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("RayUI"))
                    {
                        print("RayUI");

                        // 버튼 스크립트를 가져온다
                        CustomButton btn = hitInfo.transform.GetComponent<CustomButton>();
                        // 만약 btn이 null이 아니라면
                        if (btn != null)
                        {
                            btn.onClick.Invoke();
                        }
                    }

                    if (isPlayerPut)
                    {
                        //땅일 때만 놓을 수 있게
                        if (hitInfo.collider.CompareTag("Ground"))
                        {
                            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_BUTTON3);

                            //생성될 때 추가 되어야 함.
                            GameManager.instance.gamePlayerList.Add(inPlayer);
                            GameManager.instance.playerNum++;

                            //플레이어의 번호를 추가해서 만난다.mm,
                            PlayerRecord pr = inPlayer.GetComponent<PlayerRecord>();
                            pr.myNum = GameManager.instance.playerNum;

                            //플레이어 번호를 추가
                            inPlayer.transform.SetParent(null);
                            inPlayer.GetComponent<Collider>().enabled = true;

                            //초기화 셋팅
                            isPlayerPut = false;
                            inPlayer = null;
                        }
                    }

                    //---------- 모드 ---------//
                    if (UI.Player_State == UI.PlayerState.Move)
                    {
                        Debug.Log("Player Move 모드");
                        Move();
                    }

                    if (UI.Player_State == UI.PlayerState.Delete)
                    {
                        Debug.Log("Player Delete 모드");
                        Delete();
                    }

                    if (UI.Player_State == UI.PlayerState.Teleport)
                    {
                        Debug.Log("Player Teleport 모드");
                        TelePort();
                    }

                    // Player Hopin 모드
                    if (UI.Player_State == UI.PlayerState.Hopin)
                    {
                        Debug.Log("Player Hopin 모드");
                        HopIn();
                    }
                }
            }

            else
            {
                lr.SetPosition(1, ray.origin + ray.direction * 10);
                /*marker.position = ray.origin + ray.direction * 100;
                marker.up = -ray.direction;
                marker.localScale = Vector3.one * kAdjust * 100;*/
            }
        }
    }

    void PlayerPlace()
    {
        //플레이어 배치 시
        if (isPlayerPut)
        {
            inPlayer.transform.position = hitInfo.point;
            inPlayer.transform.SetParent(cube.transform);
        }

        //안보이게 하기 위해
        if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            if (inPlayer != null)
            {
                inPlayer.SetActive(false);
            }

            #region UI 색변경 하기 위해
            /*if (hitInfo.transform.GetComponent<Button>())
            {
                print("누를 수 있는 버튼이다");
                btn = hitInfo.transform.GetComponent<Button>();
                originalColors = btn.colors;
                ColorBlock col = btn.colors;
                col.normalColor = new Color32(191, 192, 192, 255);
                btn.colors = col;
            }*/
            #endregion
        }

        else
        {
            if (inPlayer != null)
            {
                inPlayer.SetActive(true);
            }
        }
    }

    void TelePort()
    {
        if (hitInfo.collider.CompareTag("Ground"))
        {
            print("되고있니?");
            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_Teleport);
            Debug.Log(hitInfo.collider.name);
            //  Ray 닿는 곳으로 이동하고 싶다.
            player.transform.position = hitInfo.point;
        }
    }

    void Delete()
    {
        //모드 별로 하면 될 것 같음
        //만약 닿은곳이 Enemy라면
        if (hitInfo.collider.CompareTag("Player"))
        {
            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_Delete);
            Debug.Log(hitInfo.collider.name);

            //게임삭제
            GameManager.instance.gamePlayerList.Remove(hitInfo.collider.gameObject);
            Destroy(hitInfo.collider.gameObject);
        }
    }

    void Move()
    {
        if (hitInfo.collider.CompareTag("Player"))
        {
            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_Delete);
            inPlayer = hitInfo.collider.gameObject;

            inPlayer.GetComponent<Collider>().enabled = false;
            isPlayerPut = true;
        }
    }

    void HopIn()
    {
        if (hitInfo.collider.CompareTag("Player"))
        {
            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_HopIn);
            Debug.Log(hitInfo.collider.name);
            PlayerMove.instance.CharChange(hitInfo.collider.gameObject);
        }
    }

    //플레이어 클릭 
    public void Player(string name)
    {
        // 플레이어 모드일때만 클릭하면 생겨야 함  -> UI모드로 바꿔야 할 거 같음
        if (inPlayer == null)
        {
            // 플레이어 활성화 모드!
            print("캐릭터 생김");

            //플레이어를 불러올때 이름을 
            GameObject tmp = Resources.Load(name) as GameObject;
            GameObject obj = Instantiate(tmp);

            // 플레이어 셋팅
            inPlayer = obj;
            isPlayerPut = true;

        }
    }
}
