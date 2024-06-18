using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasGroup; // 패널의 CanvasGroup 컴포넌트
    public float fadeDuration = 1.0f; // 페이드 인/아웃 시간
    public float displayDuration = 2.0f; // 패널이 완전히 보이는 시간

  

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

        // 패널이 완전히 보이는 상태에서 일정 시간 대기
        yield return new WaitForSeconds(displayDuration);

        // 페이드 아웃 시작
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

        // 패널을 비활성화
        gameObject.SetActive(false);
    }
}
