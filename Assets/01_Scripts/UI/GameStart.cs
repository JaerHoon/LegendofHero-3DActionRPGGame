using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameStart : MonoBehaviour
{
    TextMeshProUGUI text;
    float speed = 2.0f;
    bool isBlink = true;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(blinkText());
    }

    IEnumerator blinkText()
    {
        float alpha = 1.0f; // 기준 알파값
        float minAlpha = 0f; // 최소 알파값
        float maxAlpha = 1.0f; // 최대 알파값

        while(isBlink==true)
        {
            // text의 알파값을 조절하여 점멸하는 모습을 구현하는 코드
            while (alpha > minAlpha)
            {
                //기준값이 최소값보다 큰 경우인 동안에 알파값이 speed 값만큼 줄어든다.
                alpha -= speed * Time.deltaTime;
                SetTextAlpha(alpha); // 변경되는 알파값을 넣어준다.
                yield return null;
            }
            
            while (alpha < maxAlpha)
            {
                //반대로 기준값이 최대값보다 작은 경우인 동안에 알파값이 speed 값만큼 늘어난다.
                alpha += speed * Time.deltaTime;
                SetTextAlpha(alpha); // 변경되는 알파값을 넣어준다.
                yield return null;
            }
        }
    }

    void SetTextAlpha(float alpha)
    {
        Color color = text.color; // text의 현재 색상
        color.a = alpha; // 현재 색상의 알파값을 alpha값으로 변경
        text.color = color; // 변경된 색상을 다시 text에 적용
    }

    public void OnCharacterChoice()
    {
        SceneManager.LoadScene("CharacterChoice");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
