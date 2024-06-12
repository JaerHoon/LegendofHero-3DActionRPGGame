using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [HideInInspector]
    public SkillChoiceController skillChoiceController;

    [HideInInspector]
    public int SkillNum;

    [HideInInspector]
    public  SkillInfo skill;

    protected BoxCollider coll;
    [SerializeField]
    protected MeshRenderer Icon_mesh;

   
    public GameObject Skill_obj;

    public SkillChoiceController.StageType stageType;


    protected InGameCanvasController InGameCanvas;


    public virtual void Init()
    {
        InGameCanvas = GameObject.FindAnyObjectByType<InGameCanvasController>();
        coll = GetComponent<BoxCollider>();
        Icon_mesh = FindInChildren(this.transform, "Icon")?.GetComponent<MeshRenderer>();
        if (Icon_mesh == null) print("Icon이라는 오브젝트가 없습니다");
    }

    private void OnEnable()
    {
        Skill_obj.SetActive(true);
    }

    Transform FindInChildren(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform result = FindInChildren(child, name);
            if (result != null)
                return result;
        }
        return null;
    }

    public virtual void Setting(CharacterManager.ChoicedCharacter choicedCharacter, int skill_Id)
    {
        if((int)choicedCharacter == 0)
        {
            skill = SkillManager.instance.warriorSkills[skill_Id];
            Icon_mesh.material = skill.Material;
            
        }
        else
        {
            skill = SkillManager.instance.archerSkills[skill_Id];
            Icon_mesh.material =skill.Material;
        }
    }


    public virtual void GetSkill()
    {
        coll.enabled = false;
        skillChoiceController.GetSkill(skill, this);
        Skill_obj.SetActive(false);

    }

    public virtual void Restore()
    {
        coll.enabled = true;
        Skill_obj.SetActive(true);
    }

   protected virtual void OnSkillInfo()
    {
        if (stageType == SkillChoiceController.StageType.stage || stageType == SkillChoiceController.StageType.start)
        {
            InGameCanvas.OnskillInfo(skill,2);
        }
        else
        {
            InGameCanvas.OnMarketSkillInfo(skill);
        }

    }

    protected virtual void OffSkillInfo()
    {
        if (stageType == SkillChoiceController.StageType.stage || stageType == SkillChoiceController.StageType.start)
        {
            InGameCanvas.OffSkillinfo();
        }
        else
        {
            InGameCanvas.OffMarketSKillinfo();
        }
    }
}
