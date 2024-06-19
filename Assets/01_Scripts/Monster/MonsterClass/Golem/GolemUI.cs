using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemUI : UIModel
{

    public Image hpBar;
    Monster monster;

    InGameCanvasController inGameCanvas;
    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Monster>();
        inGameCanvas = FindFirstObjectByType<InGameCanvasController>();
        hpBar = inGameCanvas.golemHPBar;
        UI_Update();
    }

    public void UI_Update()
    {
        
        hpBar.fillAmount = monster.CurrenHP / monster.MaxHP;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
