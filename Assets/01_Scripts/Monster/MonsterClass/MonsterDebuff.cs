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

    protected Coroutine[] debuffCorouts;

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

        debuffCorouts = new Coroutine[3];
        debuffCorouts[0] = StartCoroutine(OnCurseCorout());
        debuffCorouts[1] = StartCoroutine(OnPoisonCorout());
        debuffCorouts[2] = StartCoroutine(OnFreezeCorout());
        StopAllCoroutines();

        curseImage.SetActive(false);
        posionImage.SetActive(false);
        freezeImage.SetActive(false);
    }

    public virtual void OnCurseState(float duration)
    {
        StopDebuffCoroutine(0);
        curseDuration = duration;
        enumList[0] = CURSESTATE.Curse;
        curseImage.SetActive(true);
        curseCD.fillAmount = 0;
        //RestartCoroutine(0);

        debuffCorouts[0] = StartCoroutine(OnCurseCorout());
    }

    IEnumerator OnCurseCorout()
    {
        while ((CURSESTATE)enumList[0] == CURSESTATE.Curse)
        {
            yield return new WaitForFixedUpdate();
            curseCD.fillAmount += (1f / curseDuration) * Time.deltaTime;
            if (curseCD.fillAmount >= 1) OffCurseState();
        }
    }

    protected virtual void OffCurseState()
    {
        enumList[0] = CURSESTATE.None;
        curseImage.SetActive(false);
    }

    public virtual void OnPoisonState(float duration)
    {
        StopDebuffCoroutine(1);
        poisonDuration = duration;
        enumList[1] = POISONSTATE.Poison;
        posionImage.SetActive(true);
        poisonCD.fillAmount = 0;
        //RestartCoroutine(1);

        debuffCorouts[1] = StartCoroutine(OnPoisonCorout());
    }


    IEnumerator OnPoisonCorout()
    {
        while ((POISONSTATE)enumList[1] == POISONSTATE.Poison)
        {
            yield return new WaitForFixedUpdate();
            poisonCD.fillAmount += (1f / poisonDuration) * Time.deltaTime;
            if (poisonCD.fillAmount >= 1) OffPoisonState();
        }
    }

    protected virtual void OffPoisonState()
    {
        enumList[1] = POISONSTATE.None;
        posionImage.SetActive(false);
    }

    public virtual void OnFreezeState(float duration)
    {
        StopDebuffCoroutine(2);
        freezeDuration = duration;
        enumList[2] = FREEZESTATE.Freeze;
        freezeImage.SetActive(true);
        freezeCD.fillAmount = 0;
        //RestartCoroutine(2);

        debuffCorouts[2] = StartCoroutine(OnFreezeCorout());
    }

    IEnumerator OnFreezeCorout()
    {
        while ((FREEZESTATE)enumList[2] == FREEZESTATE.Freeze)
        {
            yield return new WaitForFixedUpdate();
            freezeCD.fillAmount += (1f / freezeDuration) * Time.deltaTime;
            if (freezeCD.fillAmount >= 1) OffFreezeState();
        }
    }

    protected virtual void OffFreezeState()
    {
        enumList[2] = FREEZESTATE.None;
        freezeImage.SetActive(false);
    }

    public void RestartCoroutine(int index)
    {
        if (index >= 0 && index < debuffCorouts.Length)
        {
            if (debuffCorouts[index] == null)
            {
                switch (index)
                {
                    case 0:
                        StopDebuffCoroutine(0);
                        debuffCorouts[0] = StartCoroutine(OnCurseCorout());
                        break;
                    case 1:
                        StopDebuffCoroutine(1);
                        debuffCorouts[1] = StartCoroutine(OnPoisonCorout());
                        break;
                    case 2:
                        StopDebuffCoroutine(2);
                        debuffCorouts[2] = StartCoroutine(OnFreezeCorout());
                        break;
                }
            }
        }
    }

    public void StopDebuffCoroutine(int index)
    {
        if (debuffCorouts[index] != null)
        {
            StopCoroutine(debuffCorouts[index]);
        }
    }

    
}
