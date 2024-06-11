using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
  
    public SkillChoiceController.StageType stageType;
    [SerializeField]
    GameObject skill_NormalATK;
    SkillChoiceController normalController;
    [SerializeField]
    GameObject skill_SkillATK;
    SkillChoiceController skillController;
    [SerializeField]
    GameObject itemRelicATK;
    SkillChoiceController itemRelicController;

    private void OnEnable()
    {
        skill_NormalATK.SetActive(false);
        skill_SkillATK.SetActive(false);
        itemRelicATK.SetActive(false);
    }

    private void Start()
    {
        normalController = skill_NormalATK.GetComponent<SkillChoiceController>();
        skillController = skill_SkillATK.GetComponent<SkillChoiceController>();
        itemRelicController = itemRelicATK.GetComponent<SkillChoiceController>();
        stageType = SkillChoiceController.StageType.start;
        Setting();
    }

    public void Setting()
    {
        if(stageType == SkillChoiceController.StageType.stage || stageType == SkillChoiceController.StageType.start)
        {
            StartStageSetting();
        }
        else
        {
            MarketSetting();
        }
    }

    void StartStageSetting()
    {
        skill_NormalATK.SetActive(true);
        normalController.stageType = stageType;
        normalController.Setting();
    }

    void MarketSetting()
    {
        itemRelicATK.SetActive(true);
        itemRelicController.stageType = stageType;
        itemRelicController.Setting();
    }
}
