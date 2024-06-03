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

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
    
    Vector3 initialScale = new Vector3(1,1,1);

    public float Damage;

    private void Awake()
    {
        textcomponent = GetComponent<TextMeshPro>();
        initialScale = new Vector3(0.4f, 0.4f, 0.4f);
    }
  

    private void OnEnable()
    {
        gameObject.transform.localScale = initialScale;
        OnTexting();
        StartCoroutine(texting());
    }
    public void OnTexting()
    {
        textcomponent.text = Damage.ToString();
    }

    IEnumerator texting()
    {
        float time = 0;
        

        Vector3 finalScale = new Vector3(1, 1, 1);
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
        gameObject.SetActive(false);
    }
}
