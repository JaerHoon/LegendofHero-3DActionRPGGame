using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
  
    public SkillChoiceController.StageType stageType;
    [SerializeField]
    GameObject ChoicePlatforms;
   
    GameObject skill_NormalATK;
    SkillChoiceController normalController;
    
    GameObject skill_SkillATK;
    SkillChoiceController skillController;
    
    GameObject itemRelicATK;
    SkillChoiceController itemRelicController;

    private void Awake()
    {
        skill_NormalATK = ChoicePlatforms.transform.GetChild(0).gameObject;
        skill_SkillATK = ChoicePlatforms.transform.GetChild(1).gameObject;
        itemRelicATK = ChoicePlatforms.transform.GetChild(2).gameObject;
        normalController = skill_NormalATK.GetComponent<SkillChoiceController>();
        skillController = skill_SkillATK.GetComponent<SkillChoiceController>();
        itemRelicController = itemRelicATK.GetComponent<SkillChoiceController>();
    }

    private void OnEnable()
    {
        skill_NormalATK.SetActive(false);
        skill_SkillATK.SetActive(false);
        itemRelicATK.SetActive(false);
    }

    private void Start()
    {
        stageType = SkillChoiceController.StageType.start;
        Setting();
    }

    public void Setting()
    {
        if(stageType == SkillChoiceController.StageType.start)
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
      
    }

    void MarketSetting()
    {
        itemRelicATK.SetActive(true);
        itemRelicController.stageType = stageType;
        itemRelicController.Setting();
    }
}
