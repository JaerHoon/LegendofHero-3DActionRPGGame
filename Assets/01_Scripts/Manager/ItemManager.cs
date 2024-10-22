using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    //[HideInInspector]
    public float itemToSkillGCD;//아이템으로 적용된 글로벌 쿨다운
    //[HideInInspector]
    public float itemToAllSkillDamage;
    //[HideInInspector]
    public float itemToAttackDamageRate;
    //[HideInInspector]
    public float itemToSpeed;
    //[HideInInspector]
    public float itemToAddCritDamage;
    //[HideInInspector]
    public float itemToNonHitTime;

    Inventory inventory;

    public List<BaseItem> items = new List<BaseItem>();

    public Material[] item_Materials;
    public Sprite[] item_Sprite;


    public Dictionary<int, int> itemDic = new Dictionary<int, int>();
    public List<Relic> RelicItems = new List<Relic>();
    public List<Skill_Item> SkillItems = new List<Skill_Item>();

    public Transform closestMonster;
    public List<GameObject> allClosestMonster = new List<GameObject>();
    public bool[] isMonsterExist;
    [HideInInspector]
    public bool[] isReadyItemSkill;

    GameObject Player;

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
        item_Sprite = Resources.LoadAll<Sprite>("Material/items/Sprite");
        inventory = FindFirstObjectByType<Inventory>();
        Player = null;
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
            items[i].itemIcon = item_Sprite[i];
            items[i].itemMaterial = item_Materials[i];
            items[i].value = 500;
        }

        for (int i = 0; i < 10; i++)
        {
            itemDic[i] = 0;
        }
        itemToSkillGCD = 0;
        itemToAllSkillDamage = 0;
        itemToAttackDamageRate = 1;
        itemToSpeed = 1;
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

    void CheckPlayer()
    {
        if (GameObject.FindWithTag("Player") != null)
            Player = GameObject.FindWithTag("Player");
        else
            print("플레이어가 존재하지 않습니다.");
    }

    // Start is called before the first frame update
    void CreateItem()
    {
        Relic relic = new Relic(0, "비둘기의 활", null,null, "3초마다 타겟을 향해 투사체를 발사하여 70 데미지를 준다.",0, 70f, 0, 3f);
        items.Add(relic); RelicItems.Add(relic);
        relic = new Relic(1, "저주의 발톱", null, null, "가까운 범위에 적에게 저주를 부여한다.( 저주 : 5)", 0, 0, 5f, 0);
        items.Add(relic); RelicItems.Add(relic);
        relic = new Relic(2, "화강암 대검", null, null, "9초마다 자기 주위를 원형으로 크게 베어 300 데미지를 준다.", 0, 300f, 7f, 9f);
        items.Add(relic); RelicItems.Add(relic);
        relic = new Relic(3, "왕비의 왕관", null, null, "크리티컬이 50%가 아닌 150%의 추가 피해를 주게 된다,", 0, 0, 0, 0);
        items.Add(relic); RelicItems.Add(relic);
        relic = new Relic(4, "담쟁이덩쿨 스태프", null, null, "7초마다 타겟을 향해 투사체를 발사하여 50의 데미지를 주고 5초의 독을 부여한다. (독 : 30(5))", 0, 50f, 0, 7);
        items.Add(relic); RelicItems.Add(relic);

        Skill_Item skill_Item1 = new Skill_Item(5, "뱀송곳니 단검", null, null, "스킬 적중 시 5초의 독을 부여한다. (독 : 30(5))", 0, 0, 0, 0, 0, 0);
        items.Add(skill_Item1); SkillItems.Add(skill_Item1);
        skill_Item1 = new Skill_Item(6, "독수리 부적", null, null, "모든 스킬의 데미지가 20 증가한다.", 0, 20, 0, 0, 0, 0);
        items.Add(skill_Item1); SkillItems.Add(skill_Item1);
        skill_Item1 = new Skill_Item(7, "강철 방패", null, null, "무적 상태의 지속 시간이 1초 길어진다.", 0, 0, 1, 0, 0, 0);
        items.Add(skill_Item1); SkillItems.Add(skill_Item1);
        skill_Item1 = new Skill_Item(8, "작은 날개", null, null, "이동속도가 크게 증가한다.", 0, 0, 0, 1.5f, 0, 0);
        items.Add(skill_Item1); SkillItems.Add(skill_Item1);
        skill_Item1 = new Skill_Item(9, "거대한 곤봉", null, null, "평타의 데미지가 70% 증가하지만 이동속도가 조금 감소한다.", 0, 0, 0, 0.85f, 1.7f, 0.0f);
        items.Add(skill_Item1); SkillItems.Add(skill_Item1);

        
    }

    public void OnGetItem(int ItemNum)//아이템을 먹을 때 호출
    {
        inventory.AddItem(items[ItemNum]);
        AddDicItem();
        
    }

    void AddDicItem()//아이템 딕셔너리에 넣기
    {
        Init();//먹을 때마다 아이템 증가효과를 리셋하고 다시 적용
        for (int i = 0; i < inventory.invenItems.Count; i++)//인벤토리에 있는 아이템 만큼 돌기
        {
            for (int j = 0; j < 10; j++)//아이템 종류 만큼 돌기
            {
                if (inventory.invenItems[i].itemID == j) { itemDic[j] = 1; }
            }
        }

    }


    public void UseItemActiveItem()//공격형 아이템 사용 시작 함수
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

    void UseItem0()//3초마다 타겟을 향해 투사체를 발사하여 70 데미지를 준다.
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
            if (CD <= 0)
            {
                isReadyItemSkill[0] = true;
            }
            else
                isReadyItemSkill[0] = false;
            if (isMonsterExist[0] && isReadyItemSkill[0])
            {
                CheckPlayer();
                GameObject item_Arrow = PoolFactroy.instance.GetPool(11) as GameObject;

                item_Arrow.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);

                item_Arrow.transform.rotation = Quaternion.LookRotation(closestMonster.position - item_Arrow.transform.position);

                CD = RelicItems[0].cd;
            }
        }
    }

    void UseItem2()//16초마다 자기 주위를 원형으로 크게 베어 700 데미지를 준다.
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
                CheckPlayer();
                GameObject circleSlash = PoolFactroy.instance.GetPool(13) as GameObject;

                circleSlash.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);

                CD = RelicItems[2].cd;
            }
           
        }
    }

    void UseItem4()//10초마다 타겟을 향해 투사체를 발사하여 50의 데미지를 주고 5초의 독을 부여한다. (독 : 30(5))
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
                CheckPlayer();
                GameObject poisonPJ = PoolFactroy.instance.GetPool(12) as GameObject;

                poisonPJ.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);

                poisonPJ.transform.rotation = Quaternion.LookRotation(closestMonster.position - poisonPJ.transform.position);
                CD = RelicItems[4].cd;
            }
        }
    
    }
    Coroutine curseCorout;

    public void PassiveItem1() 
    {
        if (curseCorout != null)
            StopCoroutine(curseCorout);
        curseCorout = StartCoroutine("CurseMonster");

        //StartCoroutine("CurseMonster");
        CheckPlayer();
        print("저주의 발톱 활성화 : 주변 적 5초간 저주"); 
    }

    IEnumerator CurseMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            
            for (int i = 0; i < allClosestMonster.Count; i++)
            {
                allClosestMonster[i].GetComponent<MonsterBuff>().OnCurse(5, 0, 0);
            }
        }
    }
    public void PassiveItem3() { print("왕비의 왕관 활성화 : 크리뎀지 250%"); itemToAddCritDamage = 1.0f; }
    public void PassiveItem5() { print("뱀송곳니 단검 활성화 : 스킬적중 시 독 부여"); }
    public void PassiveItem6() { print($"독수리 부적 활성화 : 모든 스킬 위력 {SkillItems[1].power} 증가"); itemToAllSkillDamage = SkillItems[1].power; }
    public void PassiveItem7() { print($"강철 방배 활성화 : 무적 시간 {SkillItems[2].nonHitTime}초 증가"); itemToNonHitTime = SkillItems[2].nonHitTime; }
    public void PassiveItem8()
    {
        print("작은 날개 활성화 : 이동속도 증가"); itemToSpeed = SkillItems[3].speedRate;
        if (itemDic[8] == 1 && itemDic[9] == 1) itemToSpeed = 1.25f;
    }
    public void PassiveItem9()
    {
        print($"평타의 데미지가 {(SkillItems[4].damageRate - 1) * 100}% 증가하지만 이동속도가 조금 감소");
        itemToAttackDamageRate = SkillItems[4].damageRate;
        itemToSpeed = SkillItems[4].speedRate;
        if (itemDic[8] == 1 && itemDic[9] == 1) itemToSpeed = 1.25f;
    }

    public void SetClosestMonster(Transform tr)
    {
        closestMonster = tr;
    }

    public void SetAllClosestMonster(List<GameObject> monster)
    {
        allClosestMonster.Clear();
        allClosestMonster = monster;
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
