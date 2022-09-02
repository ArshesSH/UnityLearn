using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Rigidbody2D ) )]
public class Player2D : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float maxSpeed = 500.0f;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move2D();
    }

    void Move2D()
    {
        float x = Input.GetAxis( "Horizontal" );
        float y = Input.GetAxis( "Vertical" );

        Vector3 pos = rb2D.transform.position;
        pos = new Vector3( pos.x + (x * maxSpeed * Time.deltaTime), pos.y + (y * maxSpeed * Time.deltaTime), pos.z );

        rb2D.MovePosition( pos );
    }
}
