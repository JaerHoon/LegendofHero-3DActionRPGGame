using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Item : MonoBehaviour
{
    [SerializeField]
    float arrowSpeed;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        StartCoroutine(missArrowDestroy());
    }

    IEnumerator missArrowDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        PoolFactroy.instance.OutPool(this.gameObject, 11);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            //other.GetComponent<MonsterDamage>().OnDamage(ItemManager.instance.RelicItems[0].power);
            PoolFactroy.instance.OutPool(this.gameObject, 11);
        }

    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
    }
}
