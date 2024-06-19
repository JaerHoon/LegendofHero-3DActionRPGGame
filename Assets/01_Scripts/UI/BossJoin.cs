using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossJoin : MonoBehaviour
{
    [SerializeField]
    Image redPanel;
    [SerializeField]
    GameObject bossObj;

    float speed = 2.5f;
    void Start()
    {
        
        
    }

    private void OnEnable()
    {
        redPanel.enabled = false;
        bossObj.SetActive(false);
        BossRoomEnter();
    }

    public void BossRoomEnter()
    {
        redPanel.enabled = true;
        StartCoroutine(blinkRedPanel());
        StartCoroutine(blinkBossObj());
    }

    IEnumerator blinkRedPanel()
    {
        float alpha = 1.0f;
        float minAlpha = 0f;
        float maxAlpha = 1.0f;
        float blinkTime = 0;

        while (blinkTime <3)
        {
            blinkTime++;
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
        redPanel.enabled = false;
        yield break;
    }

    IEnumerator blinkBossObj()
    {
        float blinkTime = 0;
        

        while(blinkTime < 3)
        {
            bossObj.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            bossObj.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            blinkTime++;

        }
        offImage();
    }

    void SetTextAlpha(float alpha)
    {
        Color color = redPanel.color;
        color.a = alpha;
        redPanel.color = color;
    }

    void offImage()
    {
        this.gameObject.SetActive(false);
    }
}
