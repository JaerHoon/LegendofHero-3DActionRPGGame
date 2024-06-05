using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory
{
    List<GameObject> pool = new List<GameObject>();//���� Ǯ ����Ʈ ����
    GameObject _gameObjectPrefab;//���� ������
    int defaultgameObjectNumber;//ó�� pool�� ���� ���� ��

    //������ ���� ���� ����
    public ObjectFactory(GameObject gameObjectPrefab, int defaultgameObjectNumber = 100)
    {
        _gameObjectPrefab = gameObjectPrefab;//�ܺο��� ���� ������ ���� 
        this.defaultgameObjectNumber = defaultgameObjectNumber;//�ܺο��� ���� ���� �� ����
        Debug.Assert(this._gameObjectPrefab != null, "���� ���丮�� ���� ������ ����");
    }


    //������Ʈ ����
    void CreatePool(GameObject parentObject)
    {
        for (int i = 0; i < defaultgameObjectNumber; i++)
        {//��Ȱ������ ������ ���� ����
            GameObject obj = GameObject.Instantiate(_gameObjectPrefab) as GameObject;
            obj.transform.SetParent(parentObject.transform, true);
            obj.gameObject.SetActive(false);//���� �� �ٷ� ��Ȱ��ȭ
            pool.Add(obj);//pool ����Ʈ�� ����
        }
    }

    //���� �ҷ�����
    public GameObject GetObject(GameObject parentObject)
    {
        if (pool.Count == 0) CreatePool(parentObject);//pool�� �����ִ� ���Ͱ� ���ٸ� ���� ����
        int lastIndex = pool.Count - 1;// pool����Ʈ�� ������ �ε���
        GameObject obj = pool[lastIndex];//pool����Ʈ ������ ���͸� obj�� ����
        pool.RemoveAt(lastIndex);//��Ȱ�� ������ ����Ʈ ������ ���͸� ����Ʈ���� ����(��� ������ ����Ʈ���� �����ϱ� ����)
        obj.gameObject.SetActive(true);//����� ���� ���� Ȱ��ȭ
        return obj;//���� 



    }

    //�ݳ��Լ� ��Ȱ���ϱ� ����
    public void ObjectRestore(GameObject obj)
    {
        Debug.Assert(obj != null, "�ƹ��͵� ���� ������Ʈ�� ��ȯ�Ǿ�� �մϴ�");
        obj.gameObject.SetActive(false);//���� ���� ��Ȱ��ȭ
        pool.Add(obj);//��Ȱ���ϱ� ���� �ٽ� pool�� �߰�
    }
}
