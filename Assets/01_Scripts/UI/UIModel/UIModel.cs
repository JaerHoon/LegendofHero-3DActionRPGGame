
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIModel : MonoBehaviour
{

    public UIController.UpDateUI upDateUI;
    public UIController.SlotCoolTime SlotCoolTimeStart;
    public UIController.OneCoolTime OneCoolTimeStart;
    public UIController.SlotCoolTime SlotCountCoolTime;
    public UIController.SlotCountCoolTime StopCoolTime;

    public virtual void ChangeUI()
    {
        upDateUI?.Invoke();
      
    }

}
