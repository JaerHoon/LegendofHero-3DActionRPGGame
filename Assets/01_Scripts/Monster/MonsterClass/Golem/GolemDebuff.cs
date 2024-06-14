using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDebuff : MonsterDebuff
{
    InGameCanvasController inGameCanvas;
    // Start is called before the first frame update
    void Start()
    {
        inGameCanvas = FindFirstObjectByType<InGameCanvasController>();
        curseImage = inGameCanvas.golemCurse;
        posionImage = inGameCanvas.golemPoison;
        freezeImage = inGameCanvas.golemFreeze;
        Init();
    }

    // Update is called once per frame
    void Update()
    {

       
    }
}
