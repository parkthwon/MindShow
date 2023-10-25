using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    // * Player 1 �� ��ġ
    public Transform trEye;
    // OVR Rig 
    public Transform trOvrRig;
    // CenterEye
    public Transform trCenterEye;

    // * Model -> Rig Builder
    public RigBuilder rigBuilder;

    // * �̵��ؾ� �ϴ� Player
    public Transform targetPlayer;

    public List<GameObject> playerList;
    int player_index = 0;

    LHandTarget lt;
    RHandTarget rt;

    public bool isRecMove;

    //�������� �� ��ġ
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
        //�ؼҿ� ī�޶� ��ġ ����
        Vector3 offset = trEye.position - trCenterEye.position;
        trOvrRig.position += offset;

        //�������߰��κ�(������ �ƴ� y�ุ ���� �� �̵��� �� �ְ�) ��ȭ �����Ҷ���!
        /*Vector3 offset = trEye.position - trCenterEye.position;
        Vector3 newPosition2 = trOvrRig.position + new Vector3(0, offset.y, 0);
        trOvrRig.position = newPosition2;
*/
        // Vr ī�޶��� ��ġ�� ȸ���� �÷��̾ ����
        //���� �ڽ����� �ִ� �÷��̾�� ��� �ٲ��� ��.
        CharacterModel myPlayer = transform.GetComponentInChildren<CharacterModel>();

        Quaternion newRotation = Quaternion.Euler(0, trCenterEye.rotation.eulerAngles.y, 0);
        myPlayer.transform.rotation = newRotation;

        //�������߰��κ�(��ġ�߰�) -> ��ȭ �����Ҷ���!
        // ī�޶�� �� �� �տ� �ְ� �ʹ�. ������ ī�޶��� �̵��� ���� �̵��ϰ� �ʹ�.
        // �÷��̾�� ī�޶��� x z���� �������� ī�޶�� trEye�� y���� ���󰣴�.
        if (isRecMove)
        {
            //������ �� ��ġ�� �޶����� ������ -> �ڲ� ������ ���� ���� �߻�
            //���� �� ��ġ�� trCenterEye�� ��ġ�� ���� ��ŭ�� �̵���Ű�� �� ��ġ���� �������� ������?
            Vector3 newPosition = new Vector3(trCenterEye.transform.position.x, myPlayer.transform.position.y, trCenterEye.transform.position.z);
            myPlayer.transform.position = newPosition;
        }

        //�������߰��κ�(�ȸ���)
        lt = transform.GetComponentInChildren<LHandTarget>();
        rt = transform.GetComponentInChildren<RHandTarget>();

        //player�� �������� ������ �� �ְ�
        lt.isTargeting = true;
        rt.isTargeting = true;
    }

    CharacterModel cm;
    public void CharChange(GameObject target)
    {

        targetPlayer = target.transform;
        //rigBuilder �� ��Ȱ��ȭ -> �Ȳ��� ��.
        //rigBuilder.enabled = false;

        //player �������� ������ �� ����
        lt.isTargeting = false;
        rt.isTargeting = false;

        //rigBuilder �� �̿��ؼ� �θ�κ��� ������
        rigBuilder.transform.SetParent(null);

        //targetPlayer = target.transform;
        //���� ��ġ�� targetPlayer �� ��ġ�� ����
        transform.position = targetPlayer.position;
        //���� ������ targetPlayer �� ������ ����
        transform.rotation = targetPlayer.rotation;
        //targetPlayer ���� CharacterModel �� ��������.
        cm = targetPlayer.GetComponent<CharacterModel>();

        UISetting();

        //trEye �� ������ ������Ʈ�� trEye �� ����
        trEye = cm.trEye;

        //targetPlayer ���� RigBuilder �� ��������. (���� ������ �޾ƶ�)
        RigBuilder rb = targetPlayer.GetComponent<RigBuilder>();

        //������ ������Ʈ �� Ȱ��ȭ
        rb.enabled = true;

        //targetPlayer �� �θ� ���� ����
        targetPlayer.SetParent(transform);
        //targetPlayer �� rigBuilder �� transform �� ����.
        targetPlayer = rigBuilder.transform;
        //rigBuilder �� ���� ���������� �޾Ƴ��� rigBuilder�� ����
        rigBuilder = rb;
    }

    public void UISetting()
    {
        //UI
        if (cm.CompareTag("Player"))
        {
            UI.Player_State = UI.PlayerState.Rec;

            print("��ȭ�÷��̾�");
            //��ȭ ������ƮȰ��ȭ
            uiSetting[0].SetActive(true);
            uiSetting[1].SetActive(false);
            uiSetting[2].SetActive(false);
        }
        else if (cm.CompareTag("MainPlayer"))
        {
            //UI.Player_State = UI.PlayerState.Normal;
            print("�����÷��̾�");
            //������ UI ���� UI �Ѵ� Ȱ��ȭ
            uiSetting[0].SetActive(false);
            uiSetting[1].SetActive(true);
            uiSetting[2].SetActive(true);
        }
    }

    //�÷��̾� ���� (����)
    public void PlayerChange()
    {
        player_index = (player_index + 1) % playerList.Count;

        if (playerList != null)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                //���� ����
                if (i == player_index)
                {
                    targetPlayer = playerList[i].transform;
                }

                //���� �ʴ�
                else
                {

                }
            }
        }
    }
}
