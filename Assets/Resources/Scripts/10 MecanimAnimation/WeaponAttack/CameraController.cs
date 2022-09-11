using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum Mode
    {
        TPV,
        FPV
    }
    [Header( "Mode Setting" )]
    public Mode mode = Mode.TPV;
    public bool canZoom = true;

    [Header( "Object Setting" )]
    public GameObject Target;
    public GameObject AimTargetTransform;

    [Header( "Speed Setting" )]
    public float MouseSpeed = 1.5f;
    public float MouseAccelerationTime = 0.15f;
    public float ZoomSpeed = 20.0f;
    public float ZoomAccelerationTime = 0.15f;

    [Header( "Limit Setting" )]
    public float MinAngleY = -40.0f;
    public float MaxAngleY = 60.0f;

    [Header( "Invert Setting" )]
    public bool InvertX;
    public bool InvertY;
    public bool InvertZoom;


    float xAxis;
    float yAxis;
    float wheelValRaw;
    float wheelVal;
    float horizon;
    float vertical;
    float curVerticalAngle;
    bool isAiming = false;

    Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        MouseInput();
        RotateHorizon();
        
        if(canZoom)
        {
            ZoomCamera();
        }
    }

    void MouseInput()
    {
        xAxis = (InvertX ? -Input.GetAxisRaw( "Mouse X" ) : Input.GetAxisRaw( "Mouse X" )) * MouseSpeed;
        yAxis = (InvertY ? -Input.GetAxisRaw( "Mouse Y" ) : Input.GetAxisRaw( "Mouse Y" )) * MouseSpeed;
        wheelValRaw = (InvertZoom ? -Input.GetAxis( "Mouse ScrollWheel" ) : Input.GetAxis( "Mouse ScrollWheel" )) * ZoomSpeed;

        horizon = Mathf.SmoothStep( horizon, xAxis, MouseAccelerationTime );
        vertical = Mathf.SmoothStep( vertical, yAxis, MouseAccelerationTime );
        wheelVal = Mathf.SmoothStep( wheelVal, wheelValRaw, ZoomAccelerationTime );
    }

    void RotateHorizon()
    {
        transform.RotateAround( Target.transform.position, Vector3.up, horizon );

        curVerticalAngle += vertical;
        if ( curVerticalAngle <= MaxAngleY && curVerticalAngle >= MinAngleY )
        {
            transform.RotateAround( Target.transform.position, transform.right, vertical );
        }
        else
        {
            curVerticalAngle -= vertical;
        }
    }

    void ZoomCamera()
    {
        cam.fieldOfView -= wheelVal;
        if ( cam.fieldOfView < 20 )
        {
            cam.fieldOfView = 20;
        }
        if ( cam.fieldOfView > 80 )
        {
            cam.fieldOfView = 80;
        }
    }



    private void OnGUI()
    {
        //GUI.Box( new Rect( 0, 0, 200, 30 ), temp.ToString() );
    }
}
