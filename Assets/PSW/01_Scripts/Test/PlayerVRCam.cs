using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayerVRCam : MonoBehaviour
{
    // 플레이어 1,2 설정
    public Transform tr1;
    public Transform tr2;

    // 활성화 할거인지 아닌지에 대한 설정
    bool isPlayerActive = true;
    void Start()
    {
        // tr을 부모로 설정
        transform.SetParent(tr1); // false를 전달하여 부모의 로컬 변환을 무시합니다.

        // 부모 오브젝트의 트랜스폼 컴포넌트 가져오기
        tr2 = transform.parent;

        // 부모와 자식의 x 좌표를 동일하게 설정
        Vector3 childPosition = transform.localPosition;
        childPosition.x = tr2.localPosition.x;
        transform.localPosition = childPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // G키를 누르면 플레이어 교환
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isPlayerActive)
            {
                SetActivePlayer(tr1.transform);
            }
            else
            {
                SetActivePlayer(tr2.transform);
            }

            // 플레이어 교환 후 상태 변경
            isPlayerActive = !isPlayerActive;
        }
    }

    // 주어진 플레이어를 활성화하고 카메라를 해당 위치로 이동
    private void SetActivePlayer(Transform playerTransform)
    {
        transform.SetParent(playerTransform);
        Transform playerParent = transform.parent;
        Vector3 childPosition = transform.localPosition;
        childPosition.x = playerParent.localPosition.x;
        transform.localPosition = childPosition;
    }
}
