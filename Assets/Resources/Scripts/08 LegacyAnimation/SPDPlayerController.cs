using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPDPlayerController : SPDCharacterController
{

    [Header( "Camera Settings" )]
    [SerializeField]
    protected GameObject cameraTarget;
    [SerializeField]
    protected GameObject camObjectTPV;
    [SerializeField]
    protected GameObject camObjectFPV;
    private Camera camTPV;
    private Camera camFPV;
    [SerializeField]
    private float defaultTPVAngle = 20.0f;
    [SerializeField]
    private float defaultFPVAngle = 0.0f;
    [SerializeField]
    protected float camZoomFactor = 20.0f;
    [SerializeField]
    protected float camMaxAngle = 60.0f;
    [SerializeField]
    protected float camMinAngle = -60.0f;
    private float curCamTPVAngleX;
    private float curCamFPVAngleX;

    void Start()
    {
        InitSettings();
        camTPV = camObjectTPV.GetComponent<Camera>();
        camFPV = camObjectFPV.GetComponent<Camera>();
        curCamTPVAngleX = camTPV.transform.eulerAngles.x;
    }

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.F) )
        {
            SwitchingCamera();
        }

        if ( camObjectTPV.activeInHierarchy )
        {
            ZoomCamera();
        }
        RotateCamera();
        SetDirection();
        SetState();
        PlayAnimations();
    }
    private void SwitchingCamera()
    {
        if( camObjectTPV.activeInHierarchy )
        {
            curCamFPVAngleX = defaultFPVAngle;
            camObjectTPV.SetActive( false );
            camObjectFPV.SetActive( true );
        }
        else if ( camObjectFPV.activeInHierarchy )
        {
            curCamTPVAngleX = defaultTPVAngle;
            camObjectFPV.SetActive( false );
            camObjectTPV.SetActive( true );
        }
    }
    private void RotateCamera()
    {
        if( camObjectTPV.activeInHierarchy )
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
        else if( camObjectFPV.activeInHierarchy)
        {
            camObjectFPV.transform.RotateAround( cameraTarget.transform.position, Vector3.up, Input.GetAxisRaw( "Mouse X" ) );

            float verticalInput = -Input.GetAxisRaw( "Mouse Y" );
            curCamFPVAngleX += verticalInput;
            if ( curCamFPVAngleX <= camMaxAngle && curCamFPVAngleX >= camMinAngle )
            {
                camObjectFPV.transform.RotateAround( cameraTarget.transform.position, camFPV.transform.right, verticalInput );
            }
            else
            {
                curCamFPVAngleX -= verticalInput;
            }
        }

    }

    private void ZoomCamera()
    {
        camTPV.fieldOfView -= (camZoomFactor * Input.GetAxis( "Mouse ScrollWheel" ));
        // °¢Á¦ÇÑ
        if ( camTPV.fieldOfView < 20 )
        {
            camTPV.fieldOfView = 20;
        }
        if ( camTPV.fieldOfView > 80 )
        {
            camTPV.fieldOfView = 80;
        }
    }
    protected override void SetState()
    {
        base.SetState();
        if ( Input.GetButtonDown( "Fire1" ) )
        {
            transform.LookAt( transform.position + Vector3.ProjectOnPlane(camObjectTPV.transform.forward, Vector3.up ) );
            state = State.Attack;
        }
        if ( state != State.Attack )
        {
            if ( Input.GetKey( KeyCode.LeftAlt ) )
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
            RotateCharacter();
        }
    }
    protected override void SetDirection()
    {
        float horizon = Input.GetAxis( "Horizontal" );
        float vertical = Input.GetAxis( "Vertical" );

        Vector3 rightVec;
        Vector3 fowardVec;

        if(camObjectFPV.activeInHierarchy)
        {
            rightVec = camObjectFPV.transform.right;
            fowardVec = camObjectFPV.transform.forward;

            direction = (horizon * rightVec + vertical * Vector3.ProjectOnPlane(fowardVec, Vector3.up)).normalized;
        }
        else if(camObjectTPV.activeInHierarchy)
        {
            rightVec = camObjectTPV.transform.right;
            fowardVec = camObjectTPV.transform.forward;

            direction = (horizon * rightVec + vertical * Vector3.ProjectOnPlane(fowardVec, Vector3.up)).normalized;
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(30.0f, 400.0f, 150.0f, 30.0f), "State: " + state.ToString());
    }
}
