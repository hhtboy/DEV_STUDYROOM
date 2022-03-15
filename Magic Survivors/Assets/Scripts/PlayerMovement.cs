using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    
    private SpriteRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        

        if(inputX<0)
        {
            //myRenderer.flipX = true;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(inputX>0)
        {
            //myRenderer.flipX = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        inputX = Mathf.Abs(inputX);
        Vector2 velocity = new Vector2(inputX,inputY);
        velocity = velocity.normalized;

        transform.Translate(velocity*moveSpeed*Time.deltaTime);
    }
}
