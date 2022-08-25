using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    private Vector3 rotateRefAxis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveByInput();
    }

    void MoveByInput()
    {
        if( Input.GetKey(KeyCode.W) )
        {
            float vel = speed * Time.deltaTime;
            transform.position += transform.forward * vel;
        }
        if ( Input.GetKey( KeyCode.S ) )
        {
            float vel = speed * Time.deltaTime;
            transform.position -= transform.forward * vel;
        }
        
    }
}
