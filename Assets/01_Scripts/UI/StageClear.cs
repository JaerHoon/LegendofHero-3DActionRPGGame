using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class StageClear : MonoBehaviour
{
    [SerializeField]
    GameObject effect;

    TextMeshProUGUI text;
    Image backImage;
    float speed = 1.0f;
    void Start()
    {
        effect.SetActive(false);
        text = GetComponentInChildren<TextMeshProUGUI>();
        backImage = GetComponentInChildren<Image>();
        StartCoroutine(blinkText());
        
    }

    
   

    IEnumerator blinkText()
    {
        yield return new WaitForSeconds(2.0f);
        effect.SetActive(true);

        /*float alpha = 0.0f;
        float maxAlpha = 1.0f;

        while (alpha < maxAlpha)
        {
            alpha += speed * Time.deltaTime;
            SetTextAlpha(alpha);
            yield return null;
        }
        SetTextAlpha(maxAlpha);*/

    }

    /*void SetTextAlpha(float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;

        if (backImage != null)
        {
            color = backImage.color;
            color.a = alpha;
            backImage.color = color;
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
