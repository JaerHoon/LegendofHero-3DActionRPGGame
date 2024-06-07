using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotButtonType : UI
{
    private void Start()
    {
        Init();
        button.onClick.AddListener(Onclick);
    }

    void Onclick()
    {
        SlotButtonPressed?.Invoke(slotNumber);
    }
}
