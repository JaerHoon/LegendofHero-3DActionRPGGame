using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttack : MonsterAttack
{

    GameObject rockPrefab;
    GameObject projectilePrefab;

    GameObject parentDropRock;
    GameObject parentProjectile;


    ObjectFactory rockFactory;
    ObjectFactory projectileFactory;

    List<GameObject> ObjectStore = new List<GameObject>();

    public float maxXrange;
    public float maxZrange;
    int rockNum;

    // Start is called before the first frame update
    void Start()
    {
        parentDropRock = new GameObject("DropRocks");
        parentProjectile = new GameObject("Projectiles");

        rockPrefab = Resources.Load("Prefabs/DropRock") as GameObject;
        projectilePrefab = Resources.Load("Prefabs/projectile") as GameObject;

        rockFactory = new ObjectFactory(rockPrefab, 30);
        projectileFactory = new ObjectFactory(projectilePrefab, 30);

        maxXrange = 20;
        maxZrange = 20;
        rockNum = 8;
        Init();
    }

    public override void Attack1()//serch and attack
    {
        base.Attack1();
        monster.anim.OnAtk1Anim();
    }

    public override void Attack2()//projectile
    {
        base.Attack2();
        monster.anim.OnAtk2Anim();
        for (int i = 0; i < 20; i++)
        {
            GameObject projectile =  projectileFactory.GetObject(parentProjectile);
            ObjectStore.Add(projectile);
            projectile.transform.position = gameObject.transform.position;
            projectile.transform.rotation = Quaternion.Euler(0, 18 * i, 0);
            projectile.GetComponent<Projectile>().Init();
            StartCoroutine(DestroyProjectile(projectile));
        }
    }

    IEnumerator DestroyProjectile(GameObject projectile)
    {
        yield return new WaitForSeconds(7.0f);
        OnProjectileRestore(projectile);
    }

    public override void Attack3()//rock
    {
        base.Attack3();
        monster.anim.OnAtk3Anim();
        for (int i = 0; i < rockNum; i++)
        {
            SpawnRock(0, 1, 0, 1);
            SpawnRock(1, 0, 0, 1);
            SpawnRock(0, 1, 1, 0);
            SpawnRock(1, 0, 1, 0);
        }
        
    }

    void SpawnRock(int _x, int x, int _y, int y)
    {
        float randX = Random.Range(-maxXrange * _x, maxXrange * x);
        float randZ = Random.Range(-maxZrange * _y, maxZrange * y);
        float delayTime = Random.Range(0, 1.5f);
        StartCoroutine(DelaySpawnRock(delayTime, randX, randZ));
       
    }
    IEnumerator DelaySpawnRock(float delayTime, float randx, float randz)
    {
        yield return new WaitForSeconds(delayTime);
        GameObject dropRock = rockFactory.GetObject(parentDropRock);
        dropRock.transform.position = gameObject.transform.position + new Vector3(randx, dropRock.transform.position.y, randz);
        StartCoroutine(DestroyDropRock(dropRock));
    }

    IEnumerator DestroyDropRock(GameObject rock)
    {
        yield return new WaitForSeconds(3.65f);
        rockFactory.ObjectRestore(rock);
    }

    public void OnProjectileRestore(GameObject usedProjectile)
    {
        int objectIndex = ObjectStore.IndexOf(usedProjectile);
        ObjectStore.RemoveAt(objectIndex);
        projectileFactory.ObjectRestore(usedProjectile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
