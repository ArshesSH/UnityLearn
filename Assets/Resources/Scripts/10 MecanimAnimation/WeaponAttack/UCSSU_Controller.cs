using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCSSU_Controller : MonoBehaviour
{
    public enum UC_State
    {
        Wait,

    }

    [Header("Object Setting")]
    [SerializeField]
    GameObject playerModel;
    [SerializeField]
    GameObject camObj;

    [Header( "Status Setting" )]
    [SerializeField]
    float rotateSpeed = 600.0f;
    [SerializeField]
    float runSpeed = 6.0f;

    CharacterController controller;
    Camera cam;
    Vector3 direction;

    float horizontal;
    float vertical;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = camObj.GetComponent<Camera>();
    }

    void Update()
    {
        PlayerInput();
        SetDirection();
        RotateToDirection();

        if(direction.sqrMagnitude > 0.01f)
        {
            MoveCharacter();
        }
    }

    void SetDirection()
    {
        Vector3 rightVec = cam.transform.right;
        Vector3 fowardVec = cam.transform.forward;
        direction = (horizontal * rightVec +  vertical * Vector3.ProjectOnPlane( fowardVec, Vector3.up )).normalized;
    }

    void RotateToDirection()
    {
        Vector3 forward = Vector3.Slerp( playerModel.transform.forward, direction,
                rotateSpeed * Time.deltaTime / Vector3.Angle( playerModel.transform.forward, direction ) );
        transform.LookAt( playerModel.transform.position + forward );
    }

    void MoveCharacter()
    {
        controller.Move( playerModel.transform.forward * runSpeed * Time.deltaTime );
    }

    void PlayerInput()
    {
        horizontal = Input.GetAxis( "Horizontal" );
        vertical = Input.GetAxis( "Vertical" );
    }

    private void OnGUI()
    {
    }

}


