using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    float arrowSpeed;
    void Start()
    {
        
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Monster")
        {
            Debug.Log("화살 충돌!!");
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
    }
}
