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
    bool isClickPending = false; // 클릭 대기 상태를 추적
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

            /*//가져오는 애
            if (isHit && hitInfo.collider.CompareTag("Ground"))
            {
                GameObject tmp = Resources.Load(name) as GameObject;
                Vector3 spawnPosition = hitInfo.point;


                // 플레이어의 위치를 땅의 표면으로 조정
                spawnPosition.y = GetGroundHeight(spawnPosition);
                // spawnPosition.y = 0f; // player가 땅에 박히는 현상이 생김으로 y 값을 0으로 고정
                GameObject obj = Instantiate(tmp, spawnPosition, Quaternion.identity);

                spawnObject = obj;

                isClickPending = true; // 첫 번째 클릭 이벤트가 발생한 후 두 번째 클릭 대기 상태로 변경
                print("내려놓고싶음");
            }*/
        }

        else
        {
            placementPosition = hand.position + hand.forward * maxLineDistance;

            if (spawnObject != null)
            {
                // 플레이어의 위치를 땅의 표면으로 조정
                placementPosition.y = GetGroundHeight(placementPosition);
                spawnObject.transform.position = placementPosition;
            }

            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.Touch))
            {
                //아에 플레이어가 놓아짐
                PlacePlayer();
            }
        }
    }


    public void PlayerDelete(string name)
    {

        // 손위치에서 손의 앞방향으로 Ray를 만들고
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, hand.position);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);

            // 부딪힌 곳이 있다면
            // 만약 마우스 왼쪽버튼을 눌렀을 때
            // ※녹화종료가 눌림 이거 수정해야함 - UI 클릭 안되게 해야 함. 
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.Touch))/*Input.GetMouseButtonDown(0)*/
            {
                //만약 닿은곳이 Enemy라면
                if (hitInfo.collider.CompareTag("Player"))
                {
                    Debug.Log(hitInfo.collider.name);

                    // 삭제하려는 플레이어 오브젝트 찾기
                    GameObject playerObject = hitInfo.collider.gameObject;

                    // 플레이어 오브젝트 삭제
                    Destroy(playerObject);
                }
            }
        }
    }

    private void PlacePlayer()
    {
        if (spawnObject != null)
        {
            isClickPending = false; // 두 번째 클릭 이벤트가 처리됨
        }
        lr.enabled = false;
        isPlacingPlayer = false;
    }

    // 특정 위치에서 땅의 높이를 검색하는 역할
    private float GetGroundHeight(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * 100f, Vector3.down);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            return hitInfo.point.y;
        }

        return 0f; // 땅을 찾지 못한 경우 기본값으로 0을 반환
    }

    // 1. position 매개변수로 전달된 위치 주변에서 아래 방향으로 RayCast를 발사한다.
    // 2. Ray는 'position'에서 시작하여 'Vector3.up'을 사용하여 위쪽으로 100 떨어진 곳에서 아래쪽으로 Ray를 발사한다.
    // -> 이것은 Ray를 해당 위치에서 100 위로 올리는 역할을 한다.

    // 3.'Physics.Raycast' 함수를 사용하여 Ray 와 충돌하는 객체를 검색한다.
    // -> 'out hitInfo'를 사용하여 충돌 정보를 저장한다.

    // 4. Ray가 어떤 물체와 충돌할 경우, 'hitInfo.point.y' 를 사용하여 충돌 지점의 y 좌표를 가져온다. 이것이 바로 땅의 표면 높이
    // 5. 땅을 찾은 경우, 땅의 표면의 높이를 반환한다.

    // 6. Ray 가 어떤 
}