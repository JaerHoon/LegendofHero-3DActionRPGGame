using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUIController : UIController
{
   protected virtual void Init()
    {
        UIModel uIModel = GetComponent<MonsterMageUIModel>();
        viewModels.Add(uIModel);
    }
}
