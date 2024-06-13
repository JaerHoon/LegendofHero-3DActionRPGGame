using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(missRockDestroy());
    }

    IEnumerator missRockDestroy()
    {
        yield return new WaitForSeconds(3.45f);
        PoolFactroy.instance.OutPool(this.gameObject, 15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterDamage>().OnDamage(5);
        }
    }
}
