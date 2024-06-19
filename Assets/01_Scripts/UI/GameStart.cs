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
        float alpha = 1.0f; // ���� ���İ�
        float minAlpha = 0f; // �ּ� ���İ�
        float maxAlpha = 1.0f; // �ִ� ���İ�

        while(isBlink==true)
        {
            // text�� ���İ��� �����Ͽ� �����ϴ� ����� �����ϴ� �ڵ�
            while (alpha > minAlpha)
            {
                //���ذ��� �ּҰ����� ū ����� ���ȿ� ���İ��� speed ����ŭ �پ���.
                alpha -= speed * Time.deltaTime;
                SetTextAlpha(alpha); // ����Ǵ� ���İ��� �־��ش�.
                yield return null;
            }
            
            while (alpha < maxAlpha)
            {
                //�ݴ�� ���ذ��� �ִ밪���� ���� ����� ���ȿ� ���İ��� speed ����ŭ �þ��.
                alpha += speed * Time.deltaTime;
                SetTextAlpha(alpha); // ����Ǵ� ���İ��� �־��ش�.
                yield return null;
            }
        }
    }

    void SetTextAlpha(float alpha)
    {
        Color color = text.color; // text�� ���� ����
        color.a = alpha; // ���� ������ ���İ��� alpha������ ����
        text.color = color; // ����� ������ �ٽ� text�� ����
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
