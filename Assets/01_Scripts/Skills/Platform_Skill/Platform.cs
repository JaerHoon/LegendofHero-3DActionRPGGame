using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [HideInInspector]
    public Character player;
   
    [HideInInspector]
    public SkillChoiceController skillChoiceController;

    [HideInInspector]
    public int SkillNum;

    [HideInInspector]
    public  SkillInfo skill;

    [HideInInspector]
    public BaseItem item;

    protected BoxCollider coll;
    [SerializeField]
    protected MeshRenderer Icon_mesh;
    public GameObject Skill_obj;
    public SkillChoiceController.StageType stageType;
    protected InGameCanvasController InGameCanvas;


    public virtual void Init()
    {
        player = FindFirstObjectByType<Character>();
        InGameCanvas = GameObject.FindAnyObjectByType<InGameCanvasController>();
        coll = GetComponent<BoxCollider>();
        Icon_mesh = FindInChildren(this.transform, "Icon")?.GetComponent<MeshRenderer>();
        if (Icon_mesh == null) print("Icon이라는 오브젝트가 없습니다");
    }

    private void OnEnable()
    {
        Skill_obj.SetActive(true);
        if (coll != null &&  coll.enabled == false) coll.enabled = true;
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

    public virtual void Setting(BaseItem item)
    {
        this.item = item;
        Icon_mesh.material = item.itemMaterial;
    }


    public virtual void GetSkill()
    {
        skillChoiceController.GetSkill(skill);
        Skill_obj.SetActive(false);

    }

    public virtual void GetItem()
    {
        coll.enabled = false;
        skillChoiceController.GetItemRelic(item);
        Skill_obj.SetActive(false);
    }
     
   protected virtual void OnSkillInfo()
    {
        if (skillChoiceController.skillType == SkillChoiceController.SkillType.ItemRelic)
        {
           
            if (stageType == SkillChoiceController.StageType.stage)
            {
                InGameCanvas.OnskillInfo(item, 1);
                    
            }
            else
            {
                
                InGameCanvas.OnMarketSkillInfo(item);
            }
        }
        else
        {
            if (stageType == SkillChoiceController.StageType.stage)
            {
                InGameCanvas.OnskillInfo(skill, 2);
            }
            else
            {
                InGameCanvas.OnMarketSkillInfo(skill);
            }
        }
     

    }

    protected virtual void OffSkillInfo()
    {
        if (stageType == SkillChoiceController.StageType.stage)
        {
            InGameCanvas.OffSkillinfo();
        }
        else
        {
            InGameCanvas.OffMarketSKillinfo();
        }
    }
}
