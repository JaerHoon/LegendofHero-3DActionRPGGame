using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasGroup; // �г��� CanvasGroup ������Ʈ
    public float fadeDuration = 1.0f; // ���̵� ��/�ƿ� �ð�
    public float displayDuration = 2.0f; // �г��� ������ ���̴� �ð�

  

    public void starting()
    {
        canvasGroup.alpha = 1f;
        StartCoroutine(FadeOut());
    }

    public void Stage()
    {
        StartCoroutine(FadeIn());
    }

   

    private IEnumerator FadeIn()
    {
        float startAlpha = 0f;
        float endAlpha = 1f;
        float elapsedTime = 0f;

        canvasGroup.alpha = startAlpha;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        // �г��� ������ ���̴� ���¿��� ���� �ð� ���
        yield return new WaitForSeconds(displayDuration);

        // ���̵� �ƿ� ����
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float startAlpha = 1f;
        float endAlpha = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        // �г��� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
