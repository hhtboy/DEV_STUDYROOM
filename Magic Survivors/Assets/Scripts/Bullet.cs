using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        Invoke("DestroyBullet", 2);
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(Vector2.down * bulletSpeed * Time.deltaTime);
        
        

    }

    
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
