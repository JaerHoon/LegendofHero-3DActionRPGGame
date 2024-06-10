using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotSliderType : UI
{
    private void Start()
    {
        Init();
        UIUpdate();

    }

    public override void UIUpdate()
    {
        value = GetValue(SetectedValue,1);
        value2 = GetValue(SetectedValue1,2);

        slider.maxValue = value != null ? (float)value : 0;
        slider.value = value2 != null ? (float)value2 : 0;
    }
}
