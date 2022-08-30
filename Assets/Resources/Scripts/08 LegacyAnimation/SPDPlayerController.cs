using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPDPlayerController : MonoBehaviour
{
    public enum State
    {
        Idle,
        IdleBattle,
        Attack,
        Walk,
        Run,
        Die
    }

    [Header( "Weapon Settings" )]
    [SerializeField]
    protected GameObject WeaponCollider;


    [Header("Character Settings")]
    [SerializeField]
    protected GameObject cameraObject;
    [SerializeField]
    protected int maxHP = 3;
    protected int curHP;

    [SerializeField]
    protected float moveSpeed = 6.0f;
    [SerializeField]
    protected float rotateSpeed = 360.0f;
    [SerializeField]
    protected float camZoomFactor = 20.0f;


    private Camera cam;
    protected CharacterController controller;
    protected State state;
    protected Vector3 direction;

    void Start()
    {
        cam = cameraObject.GetComponent<Camera>();
        controller = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        PlayerInput(); 
    }

    private void PlayerInput()
    {
        ZoomCamera();
        RotateCamera();
        SetDirection();
        RotateCharacter();
    }


    private void RotateCamera()
    {
        cam.transform.RotateAround( transform.position, Vector3.up, Input.GetAxisRaw( "Mouse X" ) );
    }

    private void ZoomCamera()
    {
        cam.fieldOfView -= (camZoomFactor * Input.GetAxis( "Mouse ScrollWheel" ));
        // °¢Á¦ÇÑ
        if ( cam.fieldOfView < 20 )
        {
            cam.fieldOfView = 20;
        }
        if ( cam.fieldOfView > 80 )
        {
            cam.fieldOfView = 80;
        }
    }
    virtual protected void SetDirection()
    {
        // direction = new Vector3( -Input.GetAxis( "Horizontal" ), 0.0f, Input.GetAxis( "Vertical" ) );
        float horizon = Input.GetAxis( "Horizontal" );
        float vertical = Input.GetAxis( "Vertical" );

        direction = horizon * cam.transform.right + vertical * new Vector3(cam.transform.forward.x , 0.0f, cam.transform.forward.z);
    }

    protected void RotateCharacter()
    {
        if ( direction.sqrMagnitude > 0.01f )
        {
            Vector3 forward = Vector3.Slerp( transform.forward, direction,
                    rotateSpeed * Time.deltaTime / Vector3.Angle( transform.forward, direction ) );
            transform.LookAt( transform.forward + forward );
        }
        controller.Move( direction * moveSpeed * Time.deltaTime );
    }


    private void OnGUI()
    {
        //GUI.Box(new Rect(0.0f, 30.0f, 300.0f, 30.0f), "Thrust: " + thrust);
    }
}
