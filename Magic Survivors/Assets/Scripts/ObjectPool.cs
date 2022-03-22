using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private GameObject poolingObjectPrefab;

    Queue<Enemy> poolingObjectQueue= new Queue<Enemy>();

    void Awake() 
    {
        Instance = this;
        Initialize(20);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount;i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    //로딩때 미리 큐에 오브젝트를 만들어서 보관
    private Enemy CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Enemy>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    //보관했던 오브젝트를 큐에서 빼서 활성화시킴
    public static Enemy GetObject()
    {
        if(Instance.poolingObjectQueue.Count >0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(Instance.transform);
            return newObj;
        }
    }

    //다 쓴 오브젝트를 큐로 돌려줌
    public static void ReturnObj(Enemy obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }

}
