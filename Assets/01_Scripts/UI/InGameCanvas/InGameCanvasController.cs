using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvasController : UIController
{
    PlayerInfo playerInfo;
    InGameSkillSlot skillSlot;
    InteractionPanel interaction;
    MarketSkillInfo marketSkillInfo;
    InGameSkillInfo skillInfo;
    Dialog dialog;

    List<GameObject> Panels = new List<GameObject>();

    Vector2 skillinfoPos1 = new Vector2(250, 0);
    Vector2 skillinfoPos2 = new Vector2(350,0);
    Vector2 skillinfoPos3 = new Vector2(390, 0);

    public Image golemHPBar;
    public GameObject golemCurse;
    public GameObject golemPoison;
    public GameObject golemFreeze;

    CameraController cameraController;

    bool IsDialogging;
  


    private void Start()
    {
        cameraController = FindFirstObjectByType<CameraController>();

        for(int i=0; i < transform.childCount; i++)
        {
            GameObject panel = transform.GetChild(i).gameObject;
            Panels.Add(panel);
        }

        skillInfo = viewModels[4] as InGameSkillInfo;
        skillSlot = viewModels[1] as InGameSkillSlot;
        interaction = viewModels[2] as InteractionPanel;
        marketSkillInfo = viewModels[3] as MarketSkillInfo;
        playerInfo = viewModels[0] as PlayerInfo;
        dialog = viewModels[6] as Dialog;

        Panels[2].SetActive(false);
        Panels[3].SetActive(false);
        Panels[4].SetActive(false);
        Panels[6].SetActive(false);
        Panels[8].SetActive(false);
        Panels[9].SetActive(false);

    }

    public void OnInteraction(string text)
    {
        interaction.Setting(text);
        Panels[2].SetActive(true);
    }

    public void OffInteraction()
    {
        Panels[2].SetActive(false);
    }
       

    public void OnPlayerInfo()
    {
        
    }

    public void OnskillInfo(SkillInfo skill, int posnum)
    {
        if(posnum == 1)
        {
            Panels[4].GetComponent<RectTransform>().anchoredPosition = skillinfoPos2;
        }
        else
        {
            Panels[4].GetComponent<RectTransform>().anchoredPosition = skillinfoPos1;
        }
        skillInfo.Setting(skill);
        
        Panels[4].SetActive(true);
    }

    public void OnskillInfo(BaseItem item, int posnum)
    {
        if(posnum == 1)
        {
           
            Panels[4].GetComponent<RectTransform>().anchoredPosition = skillinfoPos1;
           
        }
        else
        {
            Panels[4].GetComponent<RectTransform>().anchoredPosition = skillinfoPos3;
        }
        skillInfo.Setting(item);
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
        print("start");
        skillSlot.startCooltime(Slotnum);
    }
    public void OnFadeIn_Out()
    {
        Panels[6].SetActive(true);
    }

    public void OnGolemHP()
    {
        Panels[8].SetActive(true);
    }

    public void OnDialog(string[] text, Transform npc)
    {
        if(IsDialogging == false)
        {
            IsDialogging = true;
            cameraController.OnNPCdialog(npc);
            Panels[9].SetActive(true);
            dialog.OnDialog(text);
        }
        
    }

    public void OffDialog()
    {
        IsDialogging = false;
        cameraController.Offdialog();
        Panels[9].SetActive(false);
    }
}
