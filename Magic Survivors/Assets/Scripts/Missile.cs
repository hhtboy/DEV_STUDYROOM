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
    private bool isSearching;
    private bool isEnemyNear;

    [SerializeField]
    float zAngle;

    private void Start()
    {
        transform.Rotate(GetRandomRot());
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        velocity = 10f;
        layer = 0;

        zAngle = transform.rotation.eulerAngles.z;

        if(zAngle>180)
        {
            zAngle -= 360;
        }

        defaultDir = new Vector2(Mathf.Cos(zAngle*Mathf.Deg2Rad), Mathf.Sin(zAngle * Mathf.Deg2Rad));
        StartCoroutine(Boost());
        

    }


    // Start is called before the first frame update
    public void DestroyEnemy()
    {
        MisslePool.ReturnObj(this);
    }

    void SearchEnemy()
    {
        if(!isSearching)
        {
            Collider2D[] enemyArr = Physics2D.OverlapCircleAll(player.transform.position, 100f, layer);
            if (enemyArr.Length > 0)
            {
                isEnemyNear = true;
                target = enemyArr[Random.Range(0, enemyArr.Length)].transform;
                Debug.Log("ÃßÀû");
            }
            else
            {
                isEnemyNear = false;
            }
        }
            
    }

    IEnumerator Boost()
    {
        rigid.velocity = defaultDir * velocity;
        for (int i=0;i<5;i++)
        {
            rigid.velocity = rigid.velocity - defaultDir * velocity * 0.2f;
            SearchEnemy();
            if(isEnemyNear)
            {
                rigid.velocity = target.position - transform.position;
            }
            yield return new WaitForSeconds(0.1f);
        }

        
        //rigid.velocity = player.transform.position - transform.position;
        //transform.Translate(Vector3.right*Time.deltaTime);

        
    }

    Vector3 GetRandomRot()
    {
        float _spawnAngle = Random.Range(0, 360);
        Vector3 _randomRot = new Vector3(0, 0, _spawnAngle);

        return _randomRot;
    }

}
