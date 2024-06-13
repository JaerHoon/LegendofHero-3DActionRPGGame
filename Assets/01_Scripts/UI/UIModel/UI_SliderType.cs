using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SliderType : UI
{
    int UICount;
    
    private void Start()
    {
        Init();
        UIUpdate();
    }
    public override void UIUpdate()
    {
        UICount++;
       
        value = GetValue(SetectedValue,1);
        value2 = GetValue(SetectedValue1,2);

      
        slider.maxValue = value != null ? (float)value : 0;
        slider.value = value2 != null ? (float)value2 : 0;

        print("UICount:" + UICount + "/" + "CurHP :" + slider.value + "/" + slider.maxValue);
    }
}
