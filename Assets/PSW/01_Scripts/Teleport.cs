using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform hand;
    public LineRenderer lr;
    public Transform marker;
    public float kAdjust = 1;
    public float maxLineDistance = 3f;

    private bool isPlacingPlayer = false;
    private GameObject spawnObject;
    private Vector3 placementPosition;
    bool isClickPending = false; // Ŭ�� ��� ���¸� ����
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        marker.localScale = Vector3.one * kAdjust;
    }

    void Update()
    {

        Ray ray = new Ray(hand.position, hand.forward);
        //lr.SetPosition(0, ray.origin);
        lr.SetPosition(0, hand.position);
        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(ray, out hitInfo, maxLineDistance);

        if (isHit)
        {
            lr.SetPosition(1, hitInfo.point);
            marker.position = hitInfo.point;
            marker.up = hitInfo.normal;
            marker.localScale = Vector3.one * kAdjust * hitInfo.distance;
        }

        else
        {
            lr.enabled = false;
            lr.SetPosition(1, ray.origin + ray.direction * maxLineDistance);
            marker.position = ray.origin + ray.direction * maxLineDistance;
            marker.up = -ray.direction;
            marker.localScale = Vector3.one * kAdjust * maxLineDistance;

        }
    }

    public void OnClickSelect(string name)
    {

        if (!isPlacingPlayer)
        {
            lr.enabled = true;
            isPlacingPlayer = true;
            placementPosition = hand.position + hand.forward * maxLineDistance;

            /*//�������� ��
            if (isHit && hitInfo.collider.CompareTag("Ground"))
            {
                GameObject tmp = Resources.Load(name) as GameObject;
                Vector3 spawnPosition = hitInfo.point;


                // �÷��̾��� ��ġ�� ���� ǥ������ ����
                spawnPosition.y = GetGroundHeight(spawnPosition);
                // spawnPosition.y = 0f; // player�� ���� ������ ������ �������� y ���� 0���� ����
                GameObject obj = Instantiate(tmp, spawnPosition, Quaternion.identity);

                spawnObject = obj;

                isClickPending = true; // ù ��° Ŭ�� �̺�Ʈ�� �߻��� �� �� ��° Ŭ�� ��� ���·� ����
                print("�����������");
            }*/
        }

        else
        {
            placementPosition = hand.position + hand.forward * maxLineDistance;

            if (spawnObject != null)
            {
                // �÷��̾��� ��ġ�� ���� ǥ������ ����
                placementPosition.y = GetGroundHeight(placementPosition);
                spawnObject.transform.position = placementPosition;
            }

            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.Touch))
            {
                //�ƿ� �÷��̾ ������
                PlacePlayer();
            }
        }
    }


    public void PlayerDelete(string name)
    {

        // ����ġ���� ���� �չ������� Ray�� �����
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, hand.position);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);

            // �ε��� ���� �ִٸ�
            // ���� ���콺 ���ʹ�ư�� ������ ��
            // �س�ȭ���ᰡ ���� �̰� �����ؾ��� - UI Ŭ�� �ȵǰ� �ؾ� ��. 
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.Touch))/*Input.GetMouseButtonDown(0)*/
            {
                //���� �������� Enemy���
                if (hitInfo.collider.CompareTag("Player"))
                {
                    Debug.Log(hitInfo.collider.name);

                    // �����Ϸ��� �÷��̾� ������Ʈ ã��
                    GameObject playerObject = hitInfo.collider.gameObject;

                    // �÷��̾� ������Ʈ ����
                    Destroy(playerObject);
                }
            }
        }
    }

    private void PlacePlayer()
    {
        if (spawnObject != null)
        {
            isClickPending = false; // �� ��° Ŭ�� �̺�Ʈ�� ó����
        }
        lr.enabled = false;
        isPlacingPlayer = false;
    }

    // Ư�� ��ġ���� ���� ���̸� �˻��ϴ� ����
    private float GetGroundHeight(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * 100f, Vector3.down);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            return hitInfo.point.y;
        }

        return 0f; // ���� ã�� ���� ��� �⺻������ 0�� ��ȯ
    }

    // 1. position �Ű������� ���޵� ��ġ �ֺ����� �Ʒ� �������� RayCast�� �߻��Ѵ�.
    // 2. Ray�� 'position'���� �����Ͽ� 'Vector3.up'�� ����Ͽ� �������� 100 ������ ������ �Ʒ������� Ray�� �߻��Ѵ�.
    // -> �̰��� Ray�� �ش� ��ġ���� 100 ���� �ø��� ������ �Ѵ�.

    // 3.'Physics.Raycast' �Լ��� ����Ͽ� Ray �� �浹�ϴ� ��ü�� �˻��Ѵ�.
    // -> 'out hitInfo'�� ����Ͽ� �浹 ������ �����Ѵ�.

    // 4. Ray�� � ��ü�� �浹�� ���, 'hitInfo.point.y' �� ����Ͽ� �浹 ������ y ��ǥ�� �����´�. �̰��� �ٷ� ���� ǥ�� ����
    // 5. ���� ã�� ���, ���� ǥ���� ���̸� ��ȯ�Ѵ�.

    // 6. Ray �� � 
}