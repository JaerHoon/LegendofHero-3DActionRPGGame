using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotGameObject : UI
{
    private void Start()
    {
        Init();
        viewController.SlotGameObjectSet(uITypeNumber, SetectedValue, SetectedValue1, this.gameObject, slotNumber);
    }
}
