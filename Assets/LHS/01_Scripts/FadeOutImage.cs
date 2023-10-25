using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutImage : MonoBehaviour
{
    public Image image; // ���̵� �ƿ��� �̹���
    public float fadeDuration = 2.0f; // ���̵� �ƿ� ���� �ð� (��)

    private void Start()
    {
       
    }

    public void FadeOutStart()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // �ʱ� ���� ��
        float startAlpha = image.color.a;

        // Ÿ�̸�
        float timer = 0f;

        while (timer < fadeDuration)
        {
            // �ð� ����� ���� ���� ���� �����մϴ�.
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, timer / fadeDuration);

            // �̹����� ���� ���� �����մϴ�.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            yield return null;
        }

        // ���̵� �ƿ� �Ϸ� �� �̹����� ��Ȱ��ȭ�մϴ�.
        //image.gameObject.SetActive(false);

        Destroy(image.gameObject);
    }
}