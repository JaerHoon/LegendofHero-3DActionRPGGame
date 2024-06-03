using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemUI : MonoBehaviour
{

    public Image hpBar;
    Monster monster;
    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Monster>();
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
