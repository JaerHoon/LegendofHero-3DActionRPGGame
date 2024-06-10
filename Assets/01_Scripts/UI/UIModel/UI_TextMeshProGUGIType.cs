using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TextMeshProGUGIType : UI
{
   
    void Start()
    {
        Init();
    }

    public override void UIUpdate()
    {
        value = GetValue(SetectedValue,1);

        textMesh.text = value?.ToString() ?? null;
    }
}
