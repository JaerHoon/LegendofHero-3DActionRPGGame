using System.Collections;
using UnityEngine;

public class CircleSlashTrigger : MonoBehaviour
{
    public static CircleSlashTrigger instance;
    public bool isItemAttack = false;

    [SerializeField]
    ParticleSystem slash;
    [SerializeField]
    Transform slashTr;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            other.GetComponent<MonsterDamage>().OnDamage(50);
        }
        else if (other.gameObject.tag == "Dummy")
        {
            other.GetComponent<Dummy>().OnHit(50);
        }
    }

    IEnumerator circleAttack()
    {
        yield return new WaitForSeconds(2.0f);
        slash.Play();
        slash.transform.position = slashTr.position;

        OnColliders();
        StartCoroutine(circleAttack());
    }

    void OnColliders()
    {
        gameObject.GetComponent<SphereCollider>().enabled = true;

    }

    public void OnItemAttack()
    {
        isItemAttack = !isItemAttack;

        StartCoroutine(circleAttack());

    }

    // Update is called once per frame
    void Update()
    {

    }
}
