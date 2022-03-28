using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisslePool : MonoBehaviour
{
    public static MisslePool Instance;
    private Vector3 spawnVector;

    public GameObject player;

    [SerializeField] private GameObject poolingMisslePrefab;

    Queue<Missile> poolingMissleQueue = new Queue<Missile>();

    private void Awake()
    {
        Instance = this;
        Initialize(2);
    }

    private void Start()
    {
        spawnVector = new Vector3(0, 0, 0);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingMissleQueue.Enqueue(CreateNewObject());
        }
    }

    private Missile CreateNewObject()
    {
        var newObj = Instantiate(poolingMisslePrefab).GetComponent<Missile>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Missile GetObject()
    {
        if (Instance.poolingMissleQueue.Count > 0)
        {
            var obj = Instance.poolingMissleQueue.Dequeue();
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

    public static void ReturnObj(Missile obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingMissleQueue.Enqueue(obj);
    }

    Vector3 GetRandomPos()
    {
        Vector3 _playerPos = player.transform.position;

        return _playerPos;


    }
}
