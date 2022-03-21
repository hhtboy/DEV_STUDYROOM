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

    private float bulletDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(AutoAttack());
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, bulletDir));
        SetBulletDir();

        if (curtime<=0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                bulletDir = new Vector3(inputX,inputY,0);
                Instantiate(bullet,pos.position,transform.rotation);
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;
    }



    IEnumerator AutoAttack()
    {
        while(true)
        {
            Instantiate(bullet,pos.position,transform.rotation);
            bullet.transform.Rotate(bulletDir);
            yield return new WaitForSeconds(1f);

        }
        if((inputX!=0)||(inputY!=0))
        {
            bulletDir = Mathf.Atan2(inputY, inputX) * Mathf.Rad2Deg;
        }

        Debug.Log(bulletDir);
    }
}
