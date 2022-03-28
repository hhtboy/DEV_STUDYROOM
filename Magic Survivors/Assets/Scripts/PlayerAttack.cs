using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerMovement
{
    public GameObject bullet;
    public GameObject enemy;
    public Transform pos;
    public float cooltime;
    public float curtime;
    public GameObject player;

    private float bulletDir;
    private float shortTime;
    private Vector3 spawnVector;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        shortTime = 0f;
        spawnVector = new Vector3(0, 0, 0);
        StartCoroutine(AutoAttack());
        StartCoroutine(EnemySpawn());
        StartCoroutine(Missile());
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        
        
        if((inputX!=0)||(inputY!=0))
        {
            bulletDir = Mathf.Atan2(inputY, inputX) * Mathf.Rad2Deg;
            
        }
        
        
        
        
         
    }



    IEnumerator AutoAttack()
    {
        while(true)
        {
            //총알에 랜덤 각도를 더함(-10도에서 10도 사이)
            int _randVal = Random.Range(-10, 10);
            bulletDir += _randVal;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, bulletDir));
            Instantiate(bullet,pos.position,transform.rotation);
            bulletDir -= _randVal;
            yield return new WaitForSeconds(1f);

        }
    }

    IEnumerator EnemySpawn()
    {
        while(true)
        {
            if(shortTime < 0.4f)
            {
                shortTime += 0.1f;
            }
            var Enemy = ObjectPool.GetObject();
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Missile()
    {
        while(true)
        {
            var missile = MisslePool.GetObject();

            yield return new WaitForSeconds(0.05f);
        }
    }

}
