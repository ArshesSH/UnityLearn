using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotateByInput();
    }

    void RotateByInput()
    {
        if ( Input.GetKey( KeyCode.A ) )
        {
            float angularVel = speed * Time.deltaTime;
            transform.Rotate( transform.up * -angularVel );
        }
        if ( Input.GetKey( KeyCode.D ) )
        {
            float angularVel = speed * Time.deltaTime;
            transform.Rotate( transform.up * angularVel );
        }
    }
}
