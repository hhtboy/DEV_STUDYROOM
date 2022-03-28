using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public GameObject player;

    public bool isBoostOn;

    private Rigidbody2D rigid;


    [SerializeField]  private Vector2 defaultDir;
   
    [SerializeField] private float velocity;
    [SerializeField] Transform target;
    [SerializeField] LayerMask layer;
    private float curSpeed;
    private float maxSpeed;
    private bool isStraight;
    
    private float destroyTime;

    [SerializeField]
    float zAngle;

    private void Start()
    {
        transform.Rotate(GetRandomRot());
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        velocity = 7f;
        curSpeed = 0f;
        maxSpeed = 7f;
        isStraight = false;
        isBoostOn = false;
        destroyTime = 0f;

        zAngle = transform.rotation.eulerAngles.z;

        if(zAngle>180)
        {
            zAngle -= 360;
        }

        defaultDir = new Vector2(Mathf.Cos(zAngle*Mathf.Deg2Rad), Mathf.Sin(zAngle * Mathf.Deg2Rad));
        StartCoroutine(Boost());
        

    }

    private void Update()
    {
        if (!isStraight)
        {
            if (target == null) //타겟이 없으면 생성 직후 파괴
            {
                DestroyMissile();
                SearchEnemy();
            }
            else if (target.gameObject.activeSelf) //활성화 상태일 때 (큐 밖에 있을 때)
            {
                if(isBoostOn)
                {
                    StartCoroutine(Boost());
                }
                if (curSpeed < maxSpeed)
                {
                    curSpeed += maxSpeed * Time.deltaTime;
                }

                transform.position += transform.right * curSpeed * Time.deltaTime;

                Vector3 direction = (target.position - transform.position).normalized;
                transform.right = Vector3.Lerp(transform.right, direction, 0.25f);
            }
            else if (!target.gameObject.activeSelf)
            {
                isStraight = true;
            }
        }
        else //타겟이 큐 안으로 들어가서 비활성화 되면 공격을 쭉 진행하고 3초 뒤 ReturnObj
        {
            Debug.Log("straight");
            Straight();
        }
        

    }


    public void DestroyMissile()
    {
        isBoostOn = true;
        MisslePool.ReturnObj(this);
    }

    void SearchEnemy()
    {
        Collider2D[] enemyArr = Physics2D.OverlapCircleAll(transform.position, 8f, layer);
        if (enemyArr.Length > 0)
        {
            target = enemyArr[Random.Range(0, enemyArr.Length)].transform;
        }
    }

    void Straight()
    {
        if(destroyTime >= 15.0f)
        {
            isStraight = false;
            destroyTime = 0f;
            target = null;
            MisslePool.ReturnObj(this);
        }
        else
        {
            destroyTime += 0.02f;
            transform.position += transform.right * curSpeed * Time.deltaTime;
        }
        
    }

    

    public IEnumerator Boost()
    {
        Debug.Log("부스터 사용 가능");
        isBoostOn = false;
        SearchEnemy();
        rigid.velocity = defaultDir * velocity;
        for (int i=0;i<5;i++)
        {
            rigid.velocity = rigid.velocity - defaultDir * velocity * 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
    }

    Vector3 GetRandomRot()
    {
        float _spawnAngle = Random.Range(0, 360);
        Vector3 _randomRot = new Vector3(0, 0, _spawnAngle);

        return _randomRot;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.name);
        if (collision.transform.CompareTag("Enemy"))
        {
            target = null;
            MisslePool.ReturnObj(this);
        }
    }

}
