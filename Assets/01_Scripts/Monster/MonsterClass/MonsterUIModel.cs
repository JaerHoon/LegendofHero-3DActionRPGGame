using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MonsterUIModel : UIModel
{
    Monster monster;
    [HideInInspector]
    public float maxhp;
    [HideInInspector]
    public float curhp;
    [SerializeField]
    Slider HPBar;
    public virtual void Init()
    {
        monster = GetComponent<Monster>();
       
        UIupdate();

    }

    public virtual void UIupdate()
    {
        
        maxhp = monster.MaxHP;
        curhp = monster.CurrenHP;
        HPBar.maxValue = maxhp;
        HPBar.value = curhp;
       
    }


}
