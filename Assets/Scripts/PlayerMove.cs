using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //этот код для тестов

    public float speed = 300f;
    public Vector2 direction;
    public Rigidbody2D theRB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            direction = new Vector2(0, 1);
        else if (Input.GetKey(KeyCode.S))
            direction = new Vector2(0, -1);
        else if (Input.GetKey(KeyCode.A))
            direction = new Vector2(-1, 0);
        else if (Input.GetKey(KeyCode.D))
            direction = new Vector2(1, 0);
        else
            direction = new Vector2(0, 0);
        theRB.velocity = speed * direction * Time.deltaTime;
    }
}
