using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCanvasController : UIController
{
    PlayerInfo playerInfo;
    InGameSkillSlot skillSlot;
    InteractionPanel interaction;
    MarketSkillInfo marketSkillInfo;
    InGameSkillInfo skillInfo;

    GameObject[] Panels = new GameObject[7];

    Vector2 skillinfoPos1 = new Vector2(-395, 0);
    Vector2 skillinfoPos2 = new Vector2(210,0);
    Vector2 skillinfoPos3 = new Vector2(390, 0);

    private void Start()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            Panels[i] = transform.GetChild(i).gameObject;
        }

        skillInfo = viewModels[4] as InGameSkillInfo;
        skillSlot = viewModels[1] as InGameSkillSlot;
        interaction = viewModels[2] as InteractionPanel;
        marketSkillInfo = viewModels[3] as MarketSkillInfo;
        playerInfo = viewModels[0] as PlayerInfo;

        Panels[2].SetActive(false);
        Panels[3].SetActive(false);
        Panels[4].SetActive(false);
    }

    public void OnInteraction(string text)
    {
        interaction.Setting(text);
        Panels[3].SetActive(true);
    }

    public void OffInteraction()
    {
        Panels[3].SetActive(false);
    }
       

    public void OnPlayerInfo()
    {
        
    }

    public void OnskillInfo(SkillInfo skill, int posnum)
    {
        if(posnum == 1)
        {
            Panels[4].GetComponent<RectTransform>().anchoredPosition = skillinfoPos1;
        }
        else
        {
            Panels[4].GetComponent<RectTransform>().anchoredPosition = skillinfoPos2;
        }
        skillInfo.Setting(skill);
        
        Panels[4].SetActive(true);
    }

    public void OnskillInfo(BaseItem item)
    {
        skillInfo.Setting(item);
        Panels[4].GetComponent<RectTransform>().anchoredPosition = skillinfoPos3;
        Panels[4].SetActive(true);
    }

    public void OffSkillinfo()
    {
        Panels[4].SetActive(false);
    }

    public void OnMarketSkillInfo(SkillInfo skill)
    {
        marketSkillInfo.Setting(skill);
        Panels[3].SetActive(true);
    }

    public void OnMarketSkillInfo(BaseItem item)
    {
        marketSkillInfo.Setting(item);
        Panels[3].SetActive(true);
    }

    public void OffMarketSKillinfo()
    {
        Panels[3].SetActive(false);
    }

    public void StartSkillCoolTime(int Slotnum)
    {
        skillSlot.startCooltime(Slotnum);
    }
}
