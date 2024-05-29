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
        Skill_Item skill_Item1 = new Skill_Item(0,"뱀송곳니 단검", null, "스킬 적중 시 5초의 독을 부여한다. (독 : 30(5))", 0, 0, 0, 0, 0);
        items.Add(skill_Item1);
        skill_Item1 = new Skill_Item(1,"독수리 부적", null, "모든 스킬의 데미지가 20 증가한다.", 20, 0, 0, 0, 0);
        items.Add(skill_Item1);
        skill_Item1 = new Skill_Item(2, "강철 방패", null, "무적 상태의 지속 시간이 1초 길어진다.", 0, 1, 0, 0, 0);
        items.Add(skill_Item1);
        skill_Item1 = new Skill_Item(3, "작은 날개", null, "이동속도가 크게 증가한다.", 0, 0, 1.5f, 0, 0);
        items.Add(skill_Item1);
        skill_Item1 = new Skill_Item(4, "거대한 곤봉", null, "평타의 데미지가 70% 증가하지만 GCD가 0.6초 길어지고 이동속도가 조금 감소한다.", 0, 0, 0.85f, 1.7f, 0.6f);
        items.Add(skill_Item1);

        Relic relic = new Relic(5, "비둘기의 활", null, "3초마다 타겟을 향해 투사체를 발사하여 70 데미지를 준다.", 70f, 0, 3f);
        items.Add(relic);
        relic = new Relic(6, "저주의 발톱", null, "가까운 범위에 적에게 저주를 부여한다.( 저주 : 5)", 0, 5f, 0);
        items.Add(relic);
        relic = new Relic(7, "화강암 대검", null, "16초마다 자기 주위를 원형으로 크게 베어 700 데미지를 준다.", 700f, 7f, 16f);
        items.Add(relic);
        relic = new Relic(8, "왕비의 왕관", null, "크리티컬이 50%가 아닌 150%의 추가 피해를 주게 된다,", 0, 0, 0);
        items.Add(relic);
        relic = new Relic(9, "담쟁이덩쿨 스태프", null, "10초마다 타겟을 향해 투사체를 발사하여 50의 데미지를 주고 5초의 독을 부여한다. (독 : 30(5))", 50f, 0, 10);
        items.Add(relic);

    }

    void OnGetItem(int ItemNum)//아이템을 먹을 때 호출
    {
        inventory.AddItem(items[ItemNum]);
        AddDicItem();
    }

    void AddDicItem()//아이템 딕셔너리에 넣기
    {
        init();//먹을 때마다 아이템 증가효과를 리셋하고 다시 적용
        for (int i = 0; i < inventory.invenItems.Count; i++)//인벤토리에 있는 아이템 만큼 돌기
        {
            for (int j = 0; j < 10; j++)//아이템 종류 만큼 돌기
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
