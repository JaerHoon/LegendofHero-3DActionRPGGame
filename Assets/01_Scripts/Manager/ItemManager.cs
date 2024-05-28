using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public static ItemManager instance;
    public Inventory inventory;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory();
        Skill_Item skill_Item1 = new Skill_Item("��۰��� �ܰ�", null, "��ų ���� �� 5���� ���� �ο��Ѵ�. (�� : 30(5))", 0, 0, 0, 0, 0);
        inventory.AddItem(skill_Item1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
