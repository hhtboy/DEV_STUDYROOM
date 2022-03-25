using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    private Rigidbody2D rigid;
    private Vector2 defaultDir;

    private void Start()
    {
        transform.Rotate(GetRandomRot());
        rigid = GetComponent<Rigidbody2D>();
        defaultDir = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z), Mathf.Sin(transform.rotation.eulerAngles.z));

        Boost();
        
    }

    // Start is called before the first frame update
    public void DestroyEnemy()
    {
        MisslePool.ReturnObj(this);
    }

    void Boost()
    {
        rigid.AddForce(defaultDir, ForceMode2D.Impulse);
    }

    Vector3 GetRandomRot()
    {
        float _spawnAngle = Random.Range(0, 360);
        Vector3 _randomRot = new Vector3(0, 0, _spawnAngle);

        return _randomRot;
    }
}
