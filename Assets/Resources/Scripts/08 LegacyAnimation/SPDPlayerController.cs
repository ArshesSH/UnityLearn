using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPDPlayerController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walk,
        Run,
        Sprint,
        Attack,
        Die
    }

    [Header( "Weapon Settings" )]
    [SerializeField]
    protected GameObject weaponObject;
    protected Collider weaponCollider;

    [Header("Character Settings")]
    [SerializeField]
    protected GameObject cameraObject;
    [SerializeField]
    protected int maxHP = 3;
    protected int curHP;
    [SerializeField]
    protected float walkSpeed = 4.0f;
    [SerializeField]
    protected float runSpeed = 8.0f;
    [SerializeField]
    protected float sprintSpeed = 12.0f;
    [SerializeField]
    protected float rotateSpeed = 360.0f;
    [SerializeField]
    protected float attackDelay = 1.0f;
    protected float attackHitDelay = 0.3f;

    [Header( "Camera Settings" )]
    [SerializeField]
    protected GameObject cameraTargetObject;
    [SerializeField]
    protected float camZoomFactor = 20.0f;
    [SerializeField]
    protected float camMaxAngle = 60.0f;
    [SerializeField]
    protected float camMinAngle = -60.0f;
    private float curCamAngleX;


    private Camera cam;
    protected CharacterController controller;
    protected Animation spartaAnim;
    protected State state;
    protected Vector3 direction;


    protected float moveSpeed;

    void Start()
    {
        cam = cameraObject.GetComponent<Camera>();
        controller = GetComponentInParent<CharacterController>();
        spartaAnim = GetComponent<Animation>();
        weaponCollider = weaponObject.GetComponent<BoxCollider>();
        weaponCollider.enabled = false;

        curCamAngleX = cam.transform.eulerAngles.x;
    }

    void Update()
    {
        PlayerInput();
        PlayAnimations();
    }

    private void PlayerInput()
    {
        ZoomCamera();
        RotateCamera();
        SetDirection();

        if ( Input.GetButtonDown("Fire1") )
        {
            transform.LookAt( transform.position + Vector3.ProjectOnPlane( cameraObject.transform.forward, Vector3.up ) );
            state = State.Attack;
        }
        if (state != State.Attack)
        {
            RotateCharacter();
        }

    }

    private void RotateCamera()
    {
        cam.transform.RotateAround( cameraTargetObject.transform.position, Vector3.up, Input.GetAxisRaw( "Mouse X" ) );

        float verticalInput = -Input.GetAxisRaw( "Mouse Y" );
        curCamAngleX += verticalInput;
        if ( curCamAngleX <= camMaxAngle && curCamAngleX >= camMinAngle )
        {
            cam.transform.RotateAround( cameraTargetObject.transform.position, cam.transform.right, verticalInput );
        }
        else
        {
            curCamAngleX -= verticalInput;
        }
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
        float horizon = Input.GetAxis( "Horizontal" );
        float vertical = Input.GetAxis( "Vertical" );

        direction = (horizon * cameraObject.transform.right + vertical * Vector3.ProjectOnPlane( cameraObject.transform.forward, Vector3.up )).normalized;
    }

    protected void RotateCharacter()
    {

        if ( direction.sqrMagnitude > 0.01f )
        {
            Vector3 forward = Vector3.Slerp( transform.forward, direction,
                    rotateSpeed * Time.deltaTime / Vector3.Angle( transform.forward, direction ) );
            transform.LookAt( transform.position + forward );


            if ( state != State.Attack && Input.GetKey( KeyCode.LeftAlt ) )
            {
                state = State.Walk;
            }
            else if ( Input.GetKey( KeyCode.LeftShift ) )
            {
                state = State.Sprint;
            }
            else
            {
                state = State.Run;
            }
            MoveCharacter();
        }
        else
        {
            state = State.Idle;
        }
    }

    protected void MoveCharacter()
    {
        switch ( state )
        {
            case State.Walk:
            {
                moveSpeed = walkSpeed;
            }
            break;
            case State.Run:
            {
                moveSpeed = runSpeed;
            }
            break;
            case State.Sprint:
            {
                moveSpeed = sprintSpeed;
            }
            break;
        }
        controller.Move( moveSpeed * Time.deltaTime * direction );
    }


    protected void PlayAnimations()
    {
        switch (state)
        {
            case State.Idle:
            {
                PlayIdle();
            }
            break;
            case State.Walk:
            {
                PlayWalk();
            }
            break;
            case State.Run:
            {
                PlayRun();
            }
            break;
            case State.Sprint:
            {
                PlaySprint();
            }
            break;
            case State.Attack:
            {
                StartCoroutine( "PlayAttack" );
            }
            break;

        }
    }

    protected void PlayIdle()
    {
        spartaAnim.wrapMode = WrapMode.Loop;
        spartaAnim.CrossFade( "idle", 0.3f );
    }
    protected void PlayWalk()
    {
        spartaAnim.wrapMode = WrapMode.Loop;
        spartaAnim.CrossFade( "walk", 0.3f );
    }
    protected void PlayRun()
    {
        spartaAnim.wrapMode = WrapMode.Loop;
        spartaAnim.CrossFade( "run", 0.3f );
    }
    protected void PlaySprint()
    {
        spartaAnim.wrapMode = WrapMode.Loop;
        spartaAnim.CrossFade( "charge", 0.3f );
    }

    IEnumerator PlayAttack()
    {
        if ( !spartaAnim.IsPlaying( "attack" ) )
        {
            spartaAnim.wrapMode = WrapMode.Once;
            spartaAnim.CrossFade( "attack", 0.3f );

            float delayTime = spartaAnim.GetClip( "attack" ).length - 0.3f; // playtime - crossfadetime

            yield return new WaitForSeconds( attackHitDelay );
            weaponCollider.enabled = true;
            yield return new WaitForSeconds( attackDelay - attackHitDelay );
            weaponCollider.enabled = false;
            state = State.Idle;
            spartaAnim.wrapMode = WrapMode.Loop;
            spartaAnim.CrossFade( "idle", 0.3f );
        }
    }


    private void OnGUI()
    {
        GUI.Box(new Rect(30.0f, 30.0f, 150.0f, 30.0f), "State: " + state.ToString());
    }
}
