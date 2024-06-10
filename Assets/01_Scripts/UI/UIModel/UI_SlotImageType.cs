using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotImageType : UI
{
 
    void Start()
    {
        Init();
    }

    public override void UIUpdate()
    {
        value = GetSlotValue(1);
        image.sprite = (Sprite)value;
    }


}
