using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    float speedMove = 10.0f;
    float speedAngular = 100.0f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float rotate = Input.GetAxis( "Horizontal" );
        float move = Input.GetAxis( "Vertical" );

        rotate = rotate * speedAngular * Time.deltaTime;
        move = move * speedMove * Time.deltaTime;

        transform.Rotate( Vector3.up * rotate );
        transform.Translate( Vector3.forward * move );
    }

    private void OnCollisionEnter( Collision collision )
    {
        GameObject hitObject = collision.gameObject;
        print( "충돌 시작 : " + hitObject.name );
    }
    private void OnCollisionStay( Collision collision )
    {
        GameObject hitObject = collision.gameObject;
        print( "충돌 중 : " + hitObject.name );
    }
    private void OnCollisionExit( Collision collision )
    {
        GameObject hitObject = collision.gameObject;
        print( "충돌 종료 : " + hitObject.name );
    }

    private void OnTriggerEnter( Collider other )
    {
        GameObject hitObject = other.gameObject;
        print( "트리거 시작 : " + hitObject.name );
    }
    private void OnTriggerStay( Collider other )
    {
        GameObject hitObject = other.gameObject;
        print( "트리거 중 : " + hitObject.name );
    }
    private void OnTriggerExit( Collider other )
    {
        GameObject hitObject = other.gameObject;
        print( "트리거 종료 : " + hitObject.name );
    }


}

