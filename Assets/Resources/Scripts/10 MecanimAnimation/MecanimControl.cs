using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanimControl : MonoBehaviour
{
    public float runSpeed = 6.0f;
    public float rotationSpeed = 360.0f;

    CharacterController pcController;
    Vector3 direction;

    void Start()
    {
        pcController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Control();
    }

    void Control()
    {
        direction = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );

        if(direction.sqrMagnitude > 0.01f)
        {
            Vector3 forward = Vector3.Slerp( transform.forward, direction,
                rotationSpeed * Time.deltaTime / Vector3.Angle( transform.forward, direction ) );
            transform.LookAt( transform.position + forward );
        }
        else
        {

        }

        pcController.Move( direction * runSpeed * Time.deltaTime );

    }
}
