using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ImageType : UI
{
    private void Start()
    {
        Init();
        UIUpdate();
    }

    public override void UIUpdate()
    {
        value = GetValue(SetectedValue);

        image.sprite = (Sprite)value;
    }
}
