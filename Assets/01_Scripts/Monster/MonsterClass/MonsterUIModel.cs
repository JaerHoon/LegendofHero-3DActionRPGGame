using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MonsterUIModel : UIModel
{
    Monster monster;
    public float maxhp;
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
