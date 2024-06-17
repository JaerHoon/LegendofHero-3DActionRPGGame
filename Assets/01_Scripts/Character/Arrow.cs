using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    float arrowSpeed;

    Archer archerController;
    public TrailRenderer trail;
    void Start()
    {
        
        archerController = GameObject.FindWithTag("Player").GetComponent<Archer>();
        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(missArrowDestroy());
    }

    IEnumerator missArrowDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        PoolFactroy.instance.OutPool(this.gameObject, Consts.Arrow);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Monster")
        {
            other.GetComponent<MonsterDamage>().OnDamage(archerController.playerSkillsSlot[0].damage * ItemManager.instance.itemToAttackDamageRate);
            PoolFactroy.instance.OutPool(this.gameObject, Consts.Arrow);
        }
        else if(other.gameObject.tag == "Dummy")
        {
            other.GetComponent<Dummy>().OnHit(archerController.playerSkillsSlot[0].damage * ItemManager.instance.itemToAttackDamageRate);
            PoolFactroy.instance.OutPool(this.gameObject, Consts.Arrow);
        }
        
        
    }

    private void OnDisable()
    {
        trail.Clear();
        trail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
    }
}
