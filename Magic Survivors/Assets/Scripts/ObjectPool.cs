using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private Vector3 spawnVector;
    public GameObject player;

    [SerializeField] private GameObject poolingObjectPrefab;

    Queue<Enemy> poolingObjectQueue= new Queue<Enemy>();

    void Awake() 
    {
        Instance = this;
        Initialize(100);
    }
    void Start() 
    {
        spawnVector = new Vector3(0, 0, 0);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount ; i++)
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
            obj.transform.position = Instance.GetRandomPos();
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(Instance.transform);
            newObj.transform.position = Instance.GetRandomPos();
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

    Vector3 GetRandomPos()
    {
        Vector3 _playerPos = player.transform.position;
        float _spawnAngle = Random.Range(0, 360);
        spawnVector.x = Mathf.Cos(_spawnAngle);
        spawnVector.y = Mathf.Sin(_spawnAngle);
        spawnVector = spawnVector * 15 + _playerPos;

        return spawnVector;

    }

}
