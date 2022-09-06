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

    [Header( "Status Setting" )]
    [SerializeField]
    float rotateSpeed = 600.0f;
    [SerializeField]
    float runSpeed = 6.0f;

    CharacterController controller;
    Cinemachine.CinemachineFreeLook cmFreeLook;
    Vector3 direction;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        SetDirection();
        RotateToDirection();
        MoveCharacter();

    }

    void SetDirection()
    {
    }

    void RotateToDirection()
    {
        Vector3 forward = Vector3.Slerp( transform.forward, direction,
                rotateSpeed * Time.deltaTime / Vector3.Angle( transform.forward, direction ) );
        transform.LookAt( transform.position + forward );
    }

    void MoveCharacter()
    {
        controller.Move( direction * runSpeed * Time.deltaTime );
    }

    private void OnGUI()
    {
    }

}


