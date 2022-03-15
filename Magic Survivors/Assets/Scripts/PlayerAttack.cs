using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;
    public float cooltime;
    public float curtime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(curtime<=0)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Instantiate(bullet,pos.position,transform.rotation);
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;
    }
}
