using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;

public class PoolFactroy : MonoBehaviour
{
    public static PoolFactroy instance; 

    public List<PoolObject> poolPrefabs = new List<PoolObject>();
  
    public List<ObjectPool<GameObject>> pooledObjects = new List<ObjectPool<GameObject>>();


    private void Awake()
    {
        instance = this;

        Init();
    }

    private void Init()
    {
        for(int i=0; i< poolPrefabs.Count; i++)
        {
            int index = i;

            GameObject gameObject = new GameObject(poolPrefabs[index].poolName);
            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(poolPrefabs[index].prefabs),
            actionOnGet: obj => { obj.SetActive(true); obj.transform.SetParent(gameObject.transform); },
            actionOnRelease: obj => { obj.SetActive(false); },
            actionOnDestroy: obj => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: poolPrefabs[index].defaultCapacity,
            maxSize: poolPrefabs[index].maxSize);

           
            pooledObjects.Add(pool);
        }

      
        
    }

    public GameObject GetPool(int index)
    {
        GameObject pool = pooledObjects[index].Get();
        print(pool.name);
        return pool;
    }

    public void OutPool(GameObject obj, int index)
    {
        pooledObjects[index].Release(obj);
    }
}

[Serializable]
public class PoolObject 
{
    [Header("�ε����ѹ�")]
    public int Index;
    [Header("Ǯ�̸�")]
    public string poolName;
    [Header("������")]
    public GameObject prefabs;
    [Header("�ʱ� ���� ����")]
    public int defaultCapacity;
    [Header("�ִ� ���� ����")]
    public int maxSize;

}
