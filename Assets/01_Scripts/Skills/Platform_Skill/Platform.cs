using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    protected SkillData skillData;
    protected SphereCollider coll;
    [SerializeField]
    protected MeshRenderer Icon_mesh;
   
    private void Start()
    {
        coll = GetComponent<SphereCollider>();
        //Icon_mesh.material = 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("¡¯¿‘");
        }
    }
}
