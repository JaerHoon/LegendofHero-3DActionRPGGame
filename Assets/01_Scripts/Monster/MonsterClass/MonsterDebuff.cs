using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterDebuff : MonoBehaviour
{
    protected Monster monster;

    [SerializeField]
    protected GameObject curseImage;
    [SerializeField]
    protected GameObject posionImage;
    [SerializeField]
    protected GameObject freezeImage;

    Image curseCD;
    Image poisonCD;
    Image freezeCD;

    float curseDuration = 0;
    float poisonDuration = 0;
    float freezeDuration = 0;

    enum CURSESTATE { None, Curse }
    enum POISONSTATE { None, Poison }
    enum FREEZESTATE { None, Freeze }

    protected List<object> enumList = new List<object>
    {
        CURSESTATE.None,
        POISONSTATE.None,
        FREEZESTATE.None
    };

    protected virtual void Init()
    {
        monster = GetComponent<Monster>();

        curseCD = curseImage.GetComponentsInChildren<Image>()[1];
        poisonCD = posionImage.GetComponentsInChildren<Image>()[1];
        freezeCD = freezeImage.GetComponentsInChildren<Image>()[1];

        curseImage.SetActive(false);
        posionImage.SetActive(false);
        freezeImage.SetActive(false);
    }

    public virtual void OnCurseState(float duration)
    {
        curseDuration = duration;
        enumList[0] = CURSESTATE.Curse;
        curseImage.SetActive(true);
        curseCD.fillAmount = 0;
    }

    protected virtual void OffCurseState()
    {
        enumList[0] = CURSESTATE.None;
        curseImage.SetActive(false);
    }

    public virtual void OnPoisonState(float duration)
    {
        poisonDuration = duration;
        enumList[1] = POISONSTATE.Poison;
        posionImage.SetActive(true);
        poisonCD.fillAmount = 0;
    }

    protected virtual void OffPoisonState()
    {
        enumList[1] = POISONSTATE.None;
        posionImage.SetActive(false);
    }

    public virtual void OnFreezeState(float duration)
    {
        freezeDuration = duration;
        enumList[2] = FREEZESTATE.Freeze;
        freezeImage.SetActive(true);
        freezeCD.fillAmount = 0;
    }

    protected virtual void OffFreezeState()
    {
        enumList[2] = FREEZESTATE.None;
        freezeImage.SetActive(false);
    }

    protected virtual void UpdateDeBuff()
    {
        if((CURSESTATE)enumList[0] == CURSESTATE.Curse)
        {
            curseCD.fillAmount += (1f/curseDuration) * Time.deltaTime;
            if (curseCD.fillAmount >= 1) OffCurseState();
            
        }
        if ((POISONSTATE)enumList[1] == POISONSTATE.Poison)
        {
            poisonCD.fillAmount += (1f / poisonDuration) * Time.deltaTime;
            if (poisonCD.fillAmount >= 1) OffPoisonState();
        }
        if ((FREEZESTATE)enumList[2] == FREEZESTATE.Freeze)
        {
            freezeCD.fillAmount += (1f / freezeDuration) * Time.deltaTime;
            if (freezeCD.fillAmount >= 1) OffFreezeState();
        }
    }
}
