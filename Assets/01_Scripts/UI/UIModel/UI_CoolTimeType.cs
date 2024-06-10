using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CoolTimeType : UI
{
    private void Start()
    {
        Init();
    }

    public override void UIUpdate()
    {
        value = GetValue(SetectedValue,1);
        OnCoolTime();
    }

    void OnCoolTime()
    {
        StopAllCoroutines();
        StartCoroutine(CoolTiem());
    }

    IEnumerator CoolTiem()
    {
        image.fillAmount = 0;

        float time = 0;
        float cooltime = (float)value;

        while (time < cooltime)
        {
            image.fillAmount += (1 / cooltime * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
