using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameItemRelic : UIModel
{
    InGameCanvasController inGameCanvasController;
    Inventory inventory;

    public List<BaseItem> gainedItems = new List<BaseItem>();

    public List<GameObject> itemSlots = new List<GameObject>(new GameObject[10]);

    private void Start()
    {
        inGameCanvasController = GetComponent<InGameCanvasController>();
        inventory = GameObject.FindAnyObjectByType<Inventory>();
        inventory.itemSetting += Setting;
        Setting();
    }

    public void Setting()
    {
        gainedItems = inventory.invenItems;

        for(int i=0; i < itemSlots.Count; i++)
        {
            itemSlots[i].SetActive(false);
        }

        if(gainedItems.Count <= itemSlots.Count)
        {
            for (int i = 0; i < gainedItems.Count; i++)
            {
                itemSlots[i].SetActive(true);
            }
        }
        else
        {
            Debug.Log("아이템 개수가 슬롯개수보다 큽니다");
        }

        ChangeUI();
       
    }

    public void OnCursor(int Slotnum)
    {
        inGameCanvasController.OnskillInfo(gainedItems[Slotnum],3);
    }

    public void OffCusor()
    {
        inGameCanvasController.OffSkillinfo();
    }
}
