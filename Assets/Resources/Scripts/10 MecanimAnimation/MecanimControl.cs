using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanimControl : MonoBehaviour
{
    public float runSpeed = 6.0f;
    public float dashSpeed = 10.0f;
    public float rotationSpeed = 360.0f;
    [SerializeField]
    protected GameObject PlayerObj;

    [Header( "Camera Settings" )]
    [SerializeField]
    protected GameObject cameraTarget;
    [SerializeField]
    protected GameObject camObjectTPV;
    private Camera camTPV;
    [SerializeField]
    private float defaultTPVAngle = 20.0f;
    [SerializeField]
    protected float camZoomFactor = 20.0f;
    [SerializeField]
    protected float camMaxAngle = 60.0f;
    [SerializeField]
    protected float camMinAngle = -60.0f;
    private float curCamTPVAngleX;
    public bool isControl = true;



    float curMoveSpeed = 6.0f;
    float timer = 0.0f;
    public float DashTimeMax = 1.0f;

    CharacterController pcController;
    Vector3 direction;
    Animator animator;

    bool isStartSlide = false;

    void Start()
    {
        pcController = GetComponentInParent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        camTPV = camObjectTPV.GetComponent<Camera>();
    }

    void Update()
    {
        animator.SetFloat( "Speed", pcController.velocity.magnitude );

        ZoomCamera();
        RotateCamera();
        SetDirection();

        Control();

        if(isStartSlide)
        {
            timer += Time.deltaTime;
            if(timer <= DashTimeMax)
            {
                pcController.Move( PlayerObj.transform.forward * dashSpeed * Time.deltaTime );
            }
            else
            {
                timer = 0.0f;
                isStartSlide = false;
            }
        }

    }

    void Control()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger( "Punch" );
        }

        if ( Input.GetButtonDown( "Fire3" ) )
        {
            animator.SetTrigger( "Slide" );
            isStartSlide = true;
        }
        if (direction.sqrMagnitude > 0.01f && !isStartSlide)
        {
            Vector3 forward = Vector3.Slerp( PlayerObj.transform.forward, direction,
                rotationSpeed * Time.deltaTime / Vector3.Angle( PlayerObj.transform.forward, direction ) );
            PlayerObj.transform.LookAt( PlayerObj.transform.position + forward );
        }
        else
        {

        }

        if( !(isStartSlide))
        {
            pcController.Move( direction * runSpeed * Time.deltaTime );
        }


    }

    private void RotateCamera()
    {
        if(camTPV != null)
        {
        if ( camObjectTPV.activeInHierarchy )
        {
            camObjectTPV.transform.RotateAround( cameraTarget.transform.position, Vector3.up, Input.GetAxisRaw( "Mouse X" ) );

            float verticalInput = -Input.GetAxisRaw( "Mouse Y" );
            curCamTPVAngleX += verticalInput;
            if ( curCamTPVAngleX <= camMaxAngle && curCamTPVAngleX >= camMinAngle )
            {
                camObjectTPV.transform.RotateAround( cameraTarget.transform.position, camTPV.transform.right, verticalInput );
            }
            else
            {
                curCamTPVAngleX -= verticalInput;
            }
        }
        }
    }


    private void ZoomCamera()
    {
        if(camTPV != null)
        {
            camTPV.fieldOfView -= (camZoomFactor * Input.GetAxis( "Mouse ScrollWheel" ));
            if ( camTPV.fieldOfView < 20 )
            {
                camTPV.fieldOfView = 20;
            }
            if ( camTPV.fieldOfView > 80 )
            {
                camTPV.fieldOfView = 80;
            }
        }
    }

    protected void SetDirection()
    {
        float horizon = Input.GetAxis( "Horizontal" );
        float vertical = Input.GetAxis( "Vertical" );

        Vector3 rightVec;
        Vector3 fowardVec;

        if ( camObjectTPV.activeInHierarchy )
        {
            rightVec = camObjectTPV.transform.right;
            fowardVec = camObjectTPV.transform.forward;

            direction = (horizon * rightVec + vertical * Vector3.ProjectOnPlane( fowardVec, Vector3.up )).normalized;
        }
    }


}
