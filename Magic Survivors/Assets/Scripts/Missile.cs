using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    private Rigidbody2D rigid;


    [SerializeField]  private Vector2 defaultDir;
    private float boostTime;
    [SerializeField] private float velocity;


    [SerializeField]
    float zAngle;

    private void Start()
    {
        transform.Rotate(GetRandomRot());
        rigid = GetComponent<Rigidbody2D>();
        boostTime = 1.0f;
        velocity = 10f;

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

    IEnumerator Boost()
    {
        rigid.velocity = defaultDir * velocity;
        while (boostTime>=0)
        {
            rigid.velocity = rigid.velocity - defaultDir * velocity * 0.1f;
            boostTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        
        
        //transform.Translate(Vector3.right*Time.deltaTime);

        
    }

    Vector3 GetRandomRot()
    {
        float _spawnAngle = Random.Range(0, 360);
        Vector3 _randomRot = new Vector3(0, 0, _spawnAngle);

        return _randomRot;
    }
}
