using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterArrow : MonoBehaviour
{
    [SerializeField]
    float arrowSpeed;
    [SerializeField]
    float duration=2;
    WaitForSeconds waitForSeconds;
    [HideInInspector]
    public float damage;
    public TrailRenderer tr;

    private void Awake()
    {
        waitForSeconds = new WaitForSeconds(duration);
        tr = GetComponentInChildren<TrailRenderer>();
    }

  
    private void OnEnable()
    {
        StartCoroutine(OffObject());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<CharacterDamage>().OnDamage(damage);
            PoolFactroy.instance.OutPool(this.gameObject, Consts.MonsterArrow);
            StopAllCoroutines();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
    }

    IEnumerator OffObject()
    {
        yield return waitForSeconds;
        if(this.gameObject.activeSelf == true)
        {
            PoolFactroy.instance.OutPool(this.gameObject, Consts.MonsterArrow);
        }
        
    }

    private void OnDisable()
    {
        tr.Clear();
   
        tr.enabled = false;
        StopAllCoroutines();
    }
}
