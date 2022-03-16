using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerMovement
{
    public GameObject bullet;
    public Transform pos;
    public float cooltime;
    public float curtime;
    public GameObject player;

    private Vector3 shootDir;

    private Vector3 bulletDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        
        shootDir = player.GetComponent<PlayerMovement>().velocity;
        
        if(curtime<=0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SetBulletDir();
                Instantiate(bullet,pos.position,transform.rotation);
                bullet.transform.Rotate(bulletDir);
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;
    }

    void SetBulletDir()
    {

        bulletDir = new Vector3(inputX,inputY,0);
                Debug.Log(bulletDir);
    }
}
