using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDebuff : MonsterDebuff
{
    InGameCanvasController inGameCanvas;
    private void Awake()
    {
        inGameCanvas = FindFirstObjectByType<InGameCanvasController>();
        curseImage = inGameCanvas.golemCurse;
        posionImage = inGameCanvas.golemPoison;
        freezeImage = inGameCanvas.golemFreeze;
        Init();
    }

}
