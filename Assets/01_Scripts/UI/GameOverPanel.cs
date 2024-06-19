using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    Image image;
    StageManager stageManager;

    private void Awake()
    {
        image = GetComponent<Image>();
        stageManager = FindFirstObjectByType<StageManager>();
    }

    private void OnEnable()
    {
        SetImageAlpha(0);
        panelOnOff();
    }


    void panelOnOff()
    {
        StartCoroutine(onoffPanel());
    }

    IEnumerator onoffPanel()
    {
        float dur = 1.0f;
        float time = 0f;
        float target = 250.0f / 255.0f;

        while (time < dur)
        {
            time += Time.deltaTime;
            float Alpha = Mathf.Clamp01(time / dur);
            SetImageAlpha(Alpha);
            yield return null;


        }
        SetImageAlpha(target);
        yield break;

    }

    void SetImageAlpha(float alpha)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

    void Restart()
    {
        stageManager.PlayerDie();
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Restart();
        }
    }
}
