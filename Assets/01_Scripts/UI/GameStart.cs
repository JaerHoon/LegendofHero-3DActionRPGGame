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
        float alpha = 1.0f;
        float minAlpha = 0f;
        float maxAlpha = 1.0f;

        while(isBlink==true)
        {
            while (alpha > minAlpha)
            {
                alpha -= speed * Time.deltaTime;
                SetTextAlpha(alpha);
                yield return null;
            }
            
            while (alpha < maxAlpha)
            {
                alpha += speed * Time.deltaTime;
                SetTextAlpha(alpha);
                yield return null;
            }
        }
    }

    void SetTextAlpha(float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
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
