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
        Sprite sp = (Sprite)value;
        print(sp.name);
        image.sprite = sp;
    }


}
