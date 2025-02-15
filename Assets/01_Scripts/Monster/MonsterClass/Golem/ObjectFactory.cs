using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory
{
    List<GameObject> pool = new List<GameObject>();//몬스터 풀 리스트 저장
    GameObject _gameObjectPrefab;//몬스터 프리팹
    int defaultgameObjectNumber;//처음 pool에 만들 몬스터 수

    //프리팹 정보 강제 주입
    public ObjectFactory(GameObject gameObjectPrefab, int defaultgameObjectNumber = 100)
    {
        _gameObjectPrefab = gameObjectPrefab;//외부에서 받은 프리팹 정보 
        this.defaultgameObjectNumber = defaultgameObjectNumber;//외부에서 받은 몬스터 수 설정
        Debug.Assert(this._gameObjectPrefab != null, "몬스터 팩토리에 몬스터 프리팹 없음");
    }


    //오브젝트 생성
    void CreatePool(GameObject parentObject)
    {
        for (int i = 0; i < defaultgameObjectNumber; i++)
        {//비활성으로 생성할 몬스터 생성
            GameObject obj = GameObject.Instantiate(_gameObjectPrefab) as GameObject;
            obj.transform.SetParent(parentObject.transform, true);
            obj.gameObject.SetActive(false);//생성 후 바로 비활성화
            pool.Add(obj);//pool 리스트에 저장
        }
    }

    //몬스터 불러오기
    public GameObject GetObject(GameObject parentObject)
    {
        if (pool.Count == 0) CreatePool(parentObject);//pool에 남아있는 몬스터가 없다면 몬스터 생성
        int lastIndex = pool.Count - 1;// pool리스트의 마지막 인덱스
        GameObject obj = pool[lastIndex];//pool리스트 마지막 몬스터를 obj에 담음
        pool.RemoveAt(lastIndex);//비활성 상태의 리스트 마지막 몬스터를 리스트에서 제거(사용 했으면 리스트에서 제거하기 위해)
        obj.gameObject.SetActive(true);//사용을 위해 몬스터 활성화
        return obj;//몬스터 



    }

    //반납함수 재활용하기 위해
    public void ObjectRestore(GameObject obj)
    {
        Debug.Assert(obj != null, "아무것도 없는 오브젝트는 반환되어야 합니다");
        obj.gameObject.SetActive(false);//상용된 몬스터 비활성화
        pool.Add(obj);//재활용하기 위해 다시 pool에 추가
    }
}
