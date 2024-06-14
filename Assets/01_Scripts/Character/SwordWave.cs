using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWave : MonoBehaviour
{
    [SerializeField]
    float skillPower;
    
    public GameObject Ex;
    BoxCollider box;
    GameObject wave;
    Warrior controller;
    Inventory inventroy;
    int monsterHit = 2;
    private void Awake()
    {
        controller = GameObject.FindWithTag("Player").GetComponent<Warrior>();
       
        box = GetComponent<BoxCollider>();
        wave = transform.GetChild(0).gameObject;
        if(transform.childCount== 2)
        {
            Ex = transform.GetChild(1).gameObject;
        }
        
    }
    void Start()
    {
        StartCoroutine(DestroyWave());
        
    }

    private void OnEnable()
    {
        wave.SetActive(true);
        box.enabled = true;
        if(Ex != null) Ex.SetActive(false);
    }

    IEnumerator DestroyWave()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Monster")
        {
            other.GetComponent<MonsterDamage>().OnDamage(controller.playerSkillsSlot[1].damage);
            inventroy = GameObject.FindFirstObjectByType<Inventory>();
            
            foreach (var item in inventroy.invenItems)
            {
                if(item.itemID==5)
                {
                    other.GetComponent<MonsterBuff>().OnPoison(5.0f, 1.0f, 5.0f);
                }
            }

            if(SkillManager.instance.gainedSkill_Warrior[1].id==7)
            {
                exPos(transform.position);
            }

            /*if (PlayerAttack.instance.isSkillSetting3)
            {
                exPos(transform.position);
            }*/

            monsterHit--;
            if(monsterHit <= 0)
            {
                Destroy(this.gameObject);
            }


        }
        else if (other.gameObject.tag == "Dummy")
        {
            other.GetComponent<Dummy>().OnHit(controller.playerSkillsSlot[1].damage);
        }

        /*if(other.gameObject.tag=="Monster" && PlayerAttack.instance.isButtonPressed3)
        {
            exPos(transform.position);
        }*/

    }

    void exPos(Vector3 pos)
    {
        Ex.SetActive(true);
        box.enabled = false;
        wave.SetActive(false);
        Destroy(this.gameObject,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * skillPower * Time.deltaTime);
        
    }
}
