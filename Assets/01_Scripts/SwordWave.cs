using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWave : MonoBehaviour
{
    [SerializeField]
    float skillPower;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Monster")
        {
            Debug.Log("검기 충돌!!");
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * skillPower * Time.deltaTime);
    }
}
