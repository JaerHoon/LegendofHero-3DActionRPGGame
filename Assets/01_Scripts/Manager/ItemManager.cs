using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public float itemToSkillGCD;
    public float itemToAllSkillDamage;
    public float itemToAttackDamageRate;
    public float itemToSpeed;
    public float itemToCrit;
    public float itemToNonHitTime;

    public Dictionary<int, int> itemDic = new Dictionary<int, int>();

    Inventory inventory;

    public static ItemManager instance;
    public List<BaseItem> items = new List<BaseItem>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        SetItem();
    }

    private void Start()
    {
        inventory = gameObject.GetComponent<Inventory>();

        init();
    }

    void init()
    {
        for (int i = 0; i < 10; i++)
        {
            itemDic.Add(i, 0);
        }
        itemToSkillGCD = 0;
        itemToAllSkillDamage = 0;
        itemToAttackDamageRate = 0;
        itemToSpeed = 0;
        itemToCrit = 0;
        itemToNonHitTime = 0;
    }

    // Start is called before the first frame update
    void SetItem()
    {
        Skill_Item skill_Item1 = new Skill_Item(0,"��۰��� �ܰ�", null, "��ų ���� �� 5���� ���� �ο��Ѵ�. (�� : 30(5))", 0, 0, 0, 0, 0);
        items.Add(skill_Item1);
        skill_Item1 = new Skill_Item(1,"������ ����", null, "��� ��ų�� �������� 20 �����Ѵ�.", 20, 0, 0, 0, 0);
        items.Add(skill_Item1);
        skill_Item1 = new Skill_Item(2, "��ö ����", null, "���� ������ ���� �ð��� 1�� �������.", 0, 1, 0, 0, 0);
        items.Add(skill_Item1);
        skill_Item1 = new Skill_Item(3, "���� ����", null, "�̵��ӵ��� ũ�� �����Ѵ�.", 0, 0, 1.5f, 0, 0);
        items.Add(skill_Item1);
        skill_Item1 = new Skill_Item(4, "�Ŵ��� ���", null, "��Ÿ�� �������� 70% ���������� GCD�� 0.6�� ������� �̵��ӵ��� ���� �����Ѵ�.", 0, 0, 0.85f, 1.7f, 0.6f);
        items.Add(skill_Item1);

        Relic relic = new Relic(5, "��ѱ��� Ȱ", null, "3�ʸ��� Ÿ���� ���� ����ü�� �߻��Ͽ� 70 �������� �ش�.", 70f, 0, 3f);
        items.Add(relic);
        relic = new Relic(6, "������ ����", null, "����� ������ ������ ���ָ� �ο��Ѵ�.( ���� : 5)", 0, 5f, 0);
        items.Add(relic);
        relic = new Relic(7, "ȭ���� ���", null, "16�ʸ��� �ڱ� ������ �������� ũ�� ���� 700 �������� �ش�.", 700f, 7f, 16f);
        items.Add(relic);
        relic = new Relic(8, "�պ��� �հ�", null, "ũ��Ƽ���� 50%�� �ƴ� 150%�� �߰� ���ظ� �ְ� �ȴ�,", 0, 0, 0);
        items.Add(relic);
        relic = new Relic(9, "�����̵��� ������", null, "10�ʸ��� Ÿ���� ���� ����ü�� �߻��Ͽ� 50�� �������� �ְ� 5���� ���� �ο��Ѵ�. (�� : 30(5))", 50f, 0, 10);
        items.Add(relic);

    }

    void OnGetItem(int ItemNum)//�������� ���� �� ȣ��
    {
        inventory.AddItem(items[ItemNum]);
        AddDicItem();
    }

    void AddDicItem()//������ ��ųʸ��� �ֱ�
    {
        init();//���� ������ ������ ����ȿ���� �����ϰ� �ٽ� ����
        for (int i = 0; i < inventory.invenItems.Count; i++)//�κ��丮�� �ִ� ������ ��ŭ ����
        {
            for (int j = 0; j < 10; j++)//������ ���� ��ŭ ����
            {
                if (inventory.invenItems[i].itemID == j) { itemDic[j] = 1; }
            }
        }
       
    }
    

    void UseItemAtiveItem()
    {
        if (itemDic[0] == 1) UseItem0();
        if (itemDic[1] == 1) UseItem1();
        if (itemDic[2] == 1) UseItem2();
        if (itemDic[3] == 1) UseItem4();
    }

    void UseItem0() { }
    void UseItem1() { }
    void UseItem2() { }
    void UseItem4() { }

    void ActiveItem3() { }
    void ActiveItem5() { }
    void ActiveItem6() { }
    void ActiveItem7() { }
    void ActiveItem8() { }
    void ActiveItem9() { }

    // Update is called once per frame
    void Update()
    {
        
    }
}
