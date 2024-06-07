using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ButtonType : UI
{
    private void Start()
    {
        Init();
        button.onClick.AddListener(OnClick);
    }


    void OnClick()
    {
        ButtonPressed?.Invoke();
    }
}
