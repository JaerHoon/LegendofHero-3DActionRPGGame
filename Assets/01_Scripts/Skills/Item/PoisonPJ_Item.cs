using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPJ_Item : MonoBehaviour
{
    [SerializeField]
    float pJSpeed;
    void Start()
    {
        StartCoroutine(missPoisonDestroy());
    }

    IEnumerator missPoisonDestroy()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            other.GetComponent<MonsterDamage>().OnDamage(ItemManager.instance.RelicItems[4].power);
            other.GetComponent<MonsterBuff>().OnPoison(5,1,30);
            Destroy(this.gameObject);
        }

    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * pJSpeed * Time.deltaTime);
    }
}
