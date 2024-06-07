using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPanel : UIModel
{
    public string noticeText;

    public void Setting(string text)
    {
        noticeText = text;
        ChangeUI();
    }
}
