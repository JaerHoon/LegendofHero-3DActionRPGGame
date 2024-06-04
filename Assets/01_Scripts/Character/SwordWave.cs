using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWave : MonoBehaviour
{
    [SerializeField]
    float skillPower;
    
    public ParticleSystem Ex;
    void Start()
    {
        StartCoroutine(DestroyWave());
 
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
            Debug.Log("검기 충돌!!");
            if(PlayerAttack.instance.isButtonPressed3)
            {
                exPos(transform.position);
            }
            
        }

    }

    void exPos(Vector3 pos)
    {
        Destroy(this.gameObject);
        Ex.transform.position = pos;
        Ex.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * skillPower * Time.deltaTime);
        
    }
}
