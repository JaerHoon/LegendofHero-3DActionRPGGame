using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    TextMeshPro textcomponent;

    [SerializeField]
    float speed = 2f;
    [SerializeField]
    float Maxtime=0.5f;
    Transform cam;

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    WaitForSeconds waitForSeconds = new WaitForSeconds(0.3f);
    
    Vector3 initialScale = new Vector3(1,1,1);

    public float Damage=0;

    private void Awake()
    {
        textcomponent = GetComponent<TextMeshPro>();
        initialScale = new Vector3(0.4f, 0.4f, 0.4f);
        cam = GameObject.FindGameObjectWithTag("FallowCam").transform;
    }
  

    private void OnEnable()
    {
        gameObject.transform.localScale = initialScale;
        StartCoroutine(texting());
    }
    public void OnTexting(float amount)
    {
        textcomponent.text = amount.ToString();
    }

    IEnumerator texting()
    {
        float time = 0;
        

        Vector3 finalScale = new Vector3(1.5f, 1.5f, 1.5f);
        while (time < Maxtime)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / Maxtime);
            Vector3 interpolatedScale = Vector3.Lerp(initialScale, finalScale, t);

            // 오브젝트에 스케일 적용
            transform.localScale = interpolatedScale;
            gameObject.transform.Translate(Vector3.up * time*speed);
            yield return waitForFixedUpdate;
        }

        yield return waitForSeconds;
        PoolFactroy.instance.OutPool(this.gameObject, Consts.DamageText);
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward,
                         cam.rotation * Vector3.up);
    }
}
