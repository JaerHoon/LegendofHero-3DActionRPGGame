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
        slider.maxValue = (float)value;
        slider.value = (float)value2;
    }
    public override void UIUpdate()
    {
        slider.maxValue = (float)value;
        slider.value = (float)value2;
    }
}
