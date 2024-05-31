using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModel : MonoBehaviour
{

    public UIController.UpDateUI upDateUI;

    public virtual void ChangeUI()
    {
        upDateUI?.Invoke();
    }

}
