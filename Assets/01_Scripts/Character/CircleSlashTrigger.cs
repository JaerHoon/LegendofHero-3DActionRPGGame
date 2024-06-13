using System.Collections;
using UnityEngine;

public class CircleSlashTrigger : MonoBehaviour
{

    [SerializeField]
    ParticleSystem slash;
    SphereCollider sphereCollider;

    private void OnEnable()
    {
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        slash.Play();
        StartCoroutine(SlashDestroy());
    }

    IEnumerator SlashDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        sphereCollider.enabled = true;
        yield return new WaitForSeconds(0.3f);
        sphereCollider.enabled = false;
        PoolFactroy.instance.OutPool(this.gameObject, 13);
    }

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            other.GetComponent<MonsterDamage>().OnDamage(ItemManager.instance.RelicItems[2].power);
        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}
