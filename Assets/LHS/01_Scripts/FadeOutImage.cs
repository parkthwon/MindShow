using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutImage : MonoBehaviour
{
    public Image image; // 페이드 아웃할 이미지
    public float fadeDuration = 2.0f; // 페이드 아웃 지속 시간 (초)

    private void Start()
    {
       
    }

    public void FadeOutStart()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // 초기 알파 값
        float startAlpha = image.color.a;

        // 타이머
        float timer = 0f;

        while (timer < fadeDuration)
        {
            // 시간 경과에 따른 알파 값을 조절합니다.
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, timer / fadeDuration);

            // 이미지의 알파 값을 설정합니다.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            yield return null;
        }

        // 페이드 아웃 완료 후 이미지를 비활성화합니다.
        //image.gameObject.SetActive(false);

        Destroy(image.gameObject);
    }
}