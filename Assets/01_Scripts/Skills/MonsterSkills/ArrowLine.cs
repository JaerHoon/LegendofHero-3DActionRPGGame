using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLine : MonoBehaviour
{
    TrailRenderer tr;
    public Vector3 endPos;

    private void Awake()
    {
        tr = GetComponent<TrailRenderer>();
        tr.startColor = new Color(1, 0, 0, 0.7f);
        tr.endColor = new Color(1, 0, 0, 0.7f);
        Destroy(this.gameObject, 0.3f);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * 3.5f);
    }


}
