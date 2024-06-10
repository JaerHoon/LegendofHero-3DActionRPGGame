using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotTextMeshProGUGItype : UI
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void UIUpdate()
    {
        value = GetSlotValue(1);
        textMesh.text = value.ToString();
    }
}
