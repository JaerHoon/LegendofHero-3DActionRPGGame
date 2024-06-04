using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLine : MonoBehaviour
{
    public TrailRenderer tr;
    public Vector3 Dir;
    public float Speed;

    private void Awake()
    {
        tr = GetComponent<TrailRenderer>();
        tr.startColor = new Color(1, 0, 0, 0.7f);
        tr.endColor = new Color(1, 0, 0, 0.7f);
        
    }

    private void OnEnable()
    {
      
        StartCoroutine(OffObject());
    }
    private void FixedUpdate()
    {
        transform.Translate(Dir * Speed * Time.fixedDeltaTime);
    }

    IEnumerator OffObject()
    {
        yield return new WaitForSeconds(1f);
        PoolFactroy.instance.OutPool(this.gameObject, Consts.ArrowLine);
    }
    private void OnDisable()
    {
        tr.Clear();
        tr.enabled = false;
        StopAllCoroutines();
    }


}
