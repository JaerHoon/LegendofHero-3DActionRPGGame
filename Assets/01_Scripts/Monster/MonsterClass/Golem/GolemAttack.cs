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
    public int rockNum;

    

    BoxCollider handBoxColl;

    // Start is called before the first frame update
    void Start()
    {
        parentDropRock = new GameObject("DropRocks");
        parentProjectile = new GameObject("Projectiles");

        rockPrefab = Resources.Load("Prefabs/DropRock") as GameObject;
        projectilePrefab = Resources.Load("Prefabs/projectile") as GameObject;

        rockFactory = new ObjectFactory(rockPrefab, 30);
        projectileFactory = new ObjectFactory(projectilePrefab, 30);

        handBoxColl = GetComponentsInChildren<BoxCollider>()[1];
        handBoxColl.enabled = false;

        

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
        int skillNum = 6;
        int rand = Random.Range(0, skillNum);
        switch (rand)
        {
            case 0://나선 정방향
                SpawnProjectile(0, 20, 18, 0);
                break;
            case 1://나선 역방향
                SpawnProjectile(1, 20, 18, 18);
                break;
            case 2://나선 혼합
                SpawnProjectile(0, 10, 36, 0);
                SpawnProjectile(1, 10, 36, 18);
                break;
            case 3://정면 두번
                SpawnProjectile(2, 20, 18, 0);
                StartCoroutine(DelaySpawnPJ(2, 20, 18, 9,0.8f));
                break;
            case 4:
                for (int i = 0; i < 8; i++)
                    SpawnProjectile(3, 8, 2, i*45.0f+ rand*10f);
                break;
            case 5:
                StartCoroutine(DelaySpawnPJ2(2, 1, 0, rand*50f ,0.04f,90));
                break;
            default:
                break;
        }
        
    }

    IEnumerator DelaySpawnPJ(int skN, int pN, float ang, float aAng, float time)
    {
        yield return new WaitForSeconds(time);
        SpawnProjectile(skN, pN, ang, aAng);
    }
    IEnumerator DelaySpawnPJ2(int skN, int pN, float ang, float aAng, float time,int RePlayNum)
    {
        for (int i = 0; i < RePlayNum; i++)
        {
            yield return new WaitForSeconds(time);
            SpawnProjectile(skN, pN, ang, i*8f);
        }
        
    }

    void SpawnProjectile(int skillType, int projectileNum, float angle, float addAngle)
    {
       
        for (int i = 0; i < projectileNum; i++)
        {
            //GameObject projectile = projectileFactory.GetObject(parentProjectile);
            GameObject projectile = PoolFactroy.instance.GetPool(14);
            //ObjectStore.Add(projectile);
            projectile.transform.position = gameObject.transform.position;
            projectile.transform.rotation = Quaternion.Euler(0, (angle * i) + addAngle, 0);
            projectile.GetComponent<Projectile>().Init();
            projectile.GetComponent<Projectile>().Settype(skillType);
            //StartCoroutine(DestroyProjectile(projectile));
        }
    }

    //IEnumerator DestroyProjectile(GameObject projectile)
    //{
    //    yield return new WaitForSeconds(12.0f);
    //    if(projectile != null)
    //        OnProjectileRestore(projectile);
    //}

    public override void Attack3()//rock
    {
        base.Attack3();
        monster.anim.OnAtk3Anim();

        Invoke("DelayAttack3", 0.4f);
        
    }
    void DelayAttack3()
    {
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
        //GameObject dropRock = rockFactory.GetObject(parentDropRock);
        GameObject dropRock = PoolFactroy.instance.GetPool(15);
        dropRock.transform.position = new Vector3(randx, dropRock.transform.position.y, randz);
        StartCoroutine(SetCollider(dropRock));
        //StartCoroutine(DestroyDropRock(dropRock));
    }
    IEnumerator SetCollider(GameObject dr)
    {
        dr.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(3.1f);
        dr.GetComponent<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        dr.GetComponent<SphereCollider>().enabled = false;
    }

    //IEnumerator DestroyDropRock(GameObject rock)
    //{
    //    yield return new WaitForSeconds(3.45f);
    //    rockFactory.ObjectRestore(rock);
    //}

    public void OnProjectileRestore(GameObject usedProjectile)
    {
        int objectIndex = ObjectStore.IndexOf(usedProjectile);
        ObjectStore.RemoveAt(objectIndex);
        projectileFactory.ObjectRestore(usedProjectile);
    }


    public void StartHandAttack()
    {
        handBoxColl.enabled = true;
    }

    public void EndHandAttack()
    {
        handBoxColl.enabled = false;
    }

    
}
