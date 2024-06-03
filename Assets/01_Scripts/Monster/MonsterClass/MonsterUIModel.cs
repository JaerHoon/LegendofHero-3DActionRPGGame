using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MonsterUIModel : UIModel
{
    Monster monster;
    [HideInInspector]
    public float maxhp;
    [HideInInspector]
    public float curhp;
   
    public virtual void Init()
    {
        monster = GetComponent<Monster>();
       
        UIupdate();

    }

    public virtual void UIupdate()
    {
        
        maxhp = monster.MaxHP;
        curhp = monster.CurrenHP;
       
        ChangeUI();
    }


}
