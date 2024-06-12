using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    [HideInInspector]
    public float itemToSkillGCD;//���������� ����� �۷ι� ��ٿ�
    [HideInInspector]
    public float itemToAllSkillDamage;
    [HideInInspector]
    public float itemToAttackDamageRate;
    [HideInInspector]
    public float itemToSpeed;
    [HideInInspector]
    public float itemToAddCritDamage;
    [HideInInspector]
    public float itemToNonHitTime;

    Inventory inventory;

    public List<BaseItem> items = new List<BaseItem>();

    public Material[] item_Materials;


    public Dictionary<int, int> itemDic = new Dictionary<int, int>();
    public List<Relic> RelicItems = new List<Relic>();
    public List<Skill_Item> SkillItems = new List<Skill_Item>();

    public Transform closestMonster;
    public bool[] isMonsterExist;
    [HideInInspector]
    public bool[] isReadyItemSkill;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        CreateItem();
    }

    private void Start()
    {
        item_Materials = Resources.LoadAll<Material>("Material/items");
        inventory = gameObject.GetComponent<Inventory>();
        for (int i = 0; i < 10; i++)
        {
            itemDic.Add(i, 0);
        }
        Init();
    }

    void Init()
    {
        for (int i = 0; i < 10; i++)
        {
            itemDic[i] = 0;
        }
        itemToSkillGCD = 0;
        itemToAllSkillDamage = 0;
        itemToAttackDamageRate = 1;
        itemToSpeed = 0;
        itemToAddCritDamage = 0;
        itemToNonHitTime = 0;

        closestMonster = null;
        isMonsterExist = new bool[3];
        isReadyItemSkill = new bool[3];
        for (int i = 0; i < 3; i++)
        {
            isMonsterExist[i] = false;
            isReadyItemSkill[i] = false;
        }
    }

    // Start is called before the first frame update
    void CreateItem()
    {
        Relic relic = new Relic(0, "��ѱ��� Ȱ", null, "3�ʸ��� Ÿ���� ���� ����ü�� �߻��Ͽ� 70 �������� �ش�.", 70f, 0, 3f);
        items.Add(relic); RelicItems.Add(relic);
        relic = new Relic(1, "������ ����", null, "����� ������ ������ ���ָ� �ο��Ѵ�.( ���� : 5)", 0, 5f, 0);
        items.Add(relic); RelicItems.Add(relic);
        relic = new Relic(2, "ȭ���� ���", null, "16�ʸ��� �ڱ� ������ �������� ũ�� ���� 700 �������� �ش�.", 700f, 7f, 16f);
        items.Add(relic); RelicItems.Add(relic);
        relic = new Relic(3, "�պ��� �հ�", null, "ũ��Ƽ���� 50%�� �ƴ� 150%�� �߰� ���ظ� �ְ� �ȴ�,", 0, 0, 0);
        items.Add(relic); RelicItems.Add(relic);
        relic = new Relic(4, "�����̵��� ������", null, "10�ʸ��� Ÿ���� ���� ����ü�� �߻��Ͽ� 50�� �������� �ְ� 5���� ���� �ο��Ѵ�. (�� : 30(5))", 50f, 0, 10);
        items.Add(relic); RelicItems.Add(relic);

        Skill_Item skill_Item1 = new Skill_Item(5, "��۰��� �ܰ�", null, "��ų ���� �� 5���� ���� �ο��Ѵ�. (�� : 30(5))", 0, 0, 0, 0, 0);
        items.Add(skill_Item1); SkillItems.Add(skill_Item1);
        skill_Item1 = new Skill_Item(6, "������ ����", null, "��� ��ų�� �������� 20 �����Ѵ�.", 20, 0, 0, 0, 0);
        items.Add(skill_Item1); SkillItems.Add(skill_Item1);
        skill_Item1 = new Skill_Item(7, "��ö ����", null, "���� ������ ���� �ð��� 1�� �������.", 0, 1, 0, 0, 0);
        items.Add(skill_Item1); SkillItems.Add(skill_Item1);
        skill_Item1 = new Skill_Item(8, "���� ����", null, "�̵��ӵ��� ũ�� �����Ѵ�.", 0, 0, 1.5f, 0, 0);
        items.Add(skill_Item1); SkillItems.Add(skill_Item1);
        skill_Item1 = new Skill_Item(9, "�Ŵ��� ���", null, "��Ÿ�� �������� 70% ���������� GCD�� 0.6�� ������� �̵��ӵ��� ���� �����Ѵ�.", 0, 0, 0.85f, 1.7f, 0.6f);
        items.Add(skill_Item1); SkillItems.Add(skill_Item1);


    }

    public void OnGetItem(int ItemNum)//�������� ���� �� ȣ��
    {
        inventory.AddItem(items[ItemNum]);
        AddDicItem();
    }

    void AddDicItem()//������ ��ųʸ��� �ֱ�
    {
        Init();//���� ������ ������ ����ȿ���� �����ϰ� �ٽ� ����
        for (int i = 0; i < inventory.invenItems.Count; i++)//�κ��丮�� �ִ� ������ ��ŭ ����
        {
            for (int j = 0; j < 10; j++)//������ ���� ��ŭ ����
            {
                if (inventory.invenItems[i].itemID == j) { itemDic[j] = 1; }
            }
        }

    }


    public void UseItemActiveItem()//������ ������ ��� ���� �Լ�
    {
        if (itemDic[0] == 1) UseItem0();
        if (itemDic[2] == 1) UseItem2();
        if (itemDic[4] == 1) UseItem4();
    }
    public void StopActiveItem()
    {
        StopAllCoroutines();
    }

    public void UsePassiveSkillItem()//
    {
        if (itemDic[1] == 1) PassiveItem1();
        if (itemDic[3] == 1) PassiveItem3();
        if (itemDic[5] == 1) PassiveItem5();
        if (itemDic[6] == 1) PassiveItem6();
        if (itemDic[7] == 1) PassiveItem7();
        if (itemDic[8] == 1) PassiveItem8();
        if (itemDic[9] == 1) PassiveItem9();

    }

    void UseItem0()//3�ʸ��� Ÿ���� ���� ����ü�� �߻��Ͽ� 70 �������� �ش�.
    {
        StartCoroutine(LunchItem0Projectile());
    }

    IEnumerator LunchItem0Projectile()
    {
        float CD = RelicItems[0].cd;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            CD -= 0.1f;
            if(CD <= 0)
            {
                isReadyItemSkill[0] = true;
            }
            else
                isReadyItemSkill[0] = false;
            if (isMonsterExist[0] && isReadyItemSkill[0])
            {
                print($"�ٵѱ��� Ȱ �߻�!, ������ : {RelicItems[0].power}, {RelicItems[0].cd}�ʿ� �ѹ� �߻�");

                CD = RelicItems[0].cd;
            }
        }
    }

    void UseItem2()//16�ʸ��� �ڱ� ������ �������� ũ�� ���� 700 �������� �ش�.
    {
        StartCoroutine(LunchItem2Slash());
    }

    IEnumerator LunchItem2Slash()
    {
        float CD = RelicItems[2].cd;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            CD -= 0.1f;
            if (CD <= 0)
            {
                isReadyItemSkill[2] = true;
            }
            else
                isReadyItemSkill[2] = false;

            if (isMonsterExist[2] && isReadyItemSkill[2])
            {
                print($"ȭ���� ��� ������!, ������ : {RelicItems[2].power}, {RelicItems[2].cd}�ʿ� �ѹ� �ֵθ���");
                CD = RelicItems[2].cd;
            }
           
        }
    }

    void UseItem4()//10�ʸ��� Ÿ���� ���� ����ü�� �߻��Ͽ� 50�� �������� �ְ� 5���� ���� �ο��Ѵ�. (�� : 30(5))
    {
        StartCoroutine(LunchItem4Projectile());
    }

    IEnumerator LunchItem4Projectile()
    {
     
        float CD = RelicItems[4].cd;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            CD -= 0.1f;
            if (CD <= 0)
            {
                isReadyItemSkill[1] = true;
            }
            else
                isReadyItemSkill[1] = false;
        
            if (isMonsterExist[1] && isReadyItemSkill[1])
            {
                print($"�����̵��� ������ �߻�!, ������ : {RelicItems[4].power}, {RelicItems[4].cd}�ʿ� �ѹ� �߻�");
                CD = RelicItems[4].cd;
            }
        }
    
    }


    public void PassiveItem1() { print("������ ���� Ȱ��ȭ : �ֺ� �� 5�ʰ� ����"); }
    public void PassiveItem3() { print("�պ��� �հ� Ȱ��ȭ : ũ������ 250%"); itemToAddCritDamage = 1.0f; }
    public void PassiveItem5() { print("��۰��� �ܰ� Ȱ��ȭ : ��ų���� �� �� �ο�"); }
    public void PassiveItem6() { print($"������ ���� Ȱ��ȭ : ��� ��ų ���� {SkillItems[1].power} ����"); itemToAllSkillDamage = SkillItems[1].power; }
    public void PassiveItem7() { print($"��ö ��� Ȱ��ȭ : ���� �ð� {SkillItems[2].nonHitTime}�� ����"); itemToNonHitTime = SkillItems[2].nonHitTime; }
    public void PassiveItem8()
    {
        print("���� ���� Ȱ��ȭ : �̵��ӵ� ����"); itemToSpeed = SkillItems[3].speedRate;
        if (itemDic[8] == 1 && itemDic[9] == 1) itemToSpeed = 1.25f;
    }
    public void PassiveItem9()
    {
        print($"��Ÿ�� �������� {(SkillItems[4].damageRate - 1) * 100}% ���������� GCD�� {SkillItems[4].gcd}�� ������� �̵��ӵ��� ���� ����");
        itemToAttackDamageRate = SkillItems[4].damageRate;
        itemToSkillGCD = SkillItems[4].gcd;
        itemToSpeed = SkillItems[4].speedRate;
        if (itemDic[8] == 1 && itemDic[9] == 1) itemToSpeed = 1.25f;
    }

    public void SetClosestMonster(Transform tr)
    {
        closestMonster = tr;
    }

    public void SetIsMonsterExist(bool TF,int typeNum)
    {
        isMonsterExist[typeNum] = TF;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
