using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SliderType : UI
{
    private void Start()
    {
        Init();
        UIUpdate();
    }
    public override void UIUpdate()
    {
        value = GetValue(SetectedValue);
        value2 = GetValue(SetectedValue1);
       
        slider.maxValue = value != null ? (float)value : 0;
        slider.value = value2 != null ? (float)value2 : 0;
    }
}
