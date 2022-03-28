using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public GameObject player;

    private Rigidbody2D rigid;


    [SerializeField]  private Vector2 defaultDir;
   
    [SerializeField] private float velocity;
    [SerializeField] Transform target;
    [SerializeField] LayerMask layer;
    private float curSpeed;
    private float maxSpeed;

    [SerializeField]
    float zAngle;

    private void Start()
    {
        transform.Rotate(GetRandomRot());
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        velocity = 5f;
        curSpeed = 0f;
        maxSpeed = 10f;

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
        
        if(target== null)
        {
            DestroyMissile();
        }
        else if (!target.gameObject.activeSelf)
        {
            //DestroyMissile();
            SearchEnemy();
        }
        else if ((target != null) || target.gameObject.activeSelf)
        {
            if (curSpeed < maxSpeed)
            {
                curSpeed += maxSpeed * Time.deltaTime;
            }

            transform.position += transform.right * curSpeed * Time.deltaTime;

            Vector3 direction = (target.position - transform.position).normalized;
            transform.right = Vector3.Lerp(transform.right, direction, 0.25f);
        }
        else if(target == null)
        {
            SearchEnemy();
        }
        

    }


    public void DestroyMissile()
    {
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

    

    IEnumerator Boost()
    {
        SearchEnemy();
        rigid.velocity = defaultDir * velocity;
        for (int i=0;i<5;i++)
        {
            rigid.velocity = rigid.velocity - defaultDir * velocity * 0.2f;
            yield return new WaitForSeconds(0.1f);
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
            MisslePool.ReturnObj(this);
        }
    }

}
