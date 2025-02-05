using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    private GameObject player;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction = direction.normalized;
        transform.Translate(direction * moveSpeed *Time.deltaTime);
    }

    public void DestroyEnemy()
    {
        ObjectPool.ReturnObj(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Bullet")||(other.tag =="Missile")||other.tag=="Player")
        {
            DestroyEnemy();
        }
    }
}
