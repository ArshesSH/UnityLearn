using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    public float MinAngleY = -40.0f;
    public float MaxAngleY = 60.0f;
    public float MouseAccelerationTime = 0.5f;
    public float MouseSpeed = 1.5f;
    public bool InvertX;
    public bool InvertY;

    float xAxis;
    float yAxis;
    float horizon;
    float vertical;
    float curVerticalAngle;

    void Start()
    {
    }

    void Update()
    {
        MouseInput();
        RotateHorizon();
    }

    void MouseInput()
    {
        xAxis = (InvertX ? -Input.GetAxisRaw( "Mouse X" ) : Input.GetAxisRaw( "Mouse X" )) * MouseSpeed;
        yAxis = (InvertY ? -Input.GetAxisRaw( "Mouse Y" ) : Input.GetAxisRaw( "Mouse Y" )) * MouseSpeed;
        horizon = Mathf.SmoothStep( horizon, xAxis, MouseAccelerationTime );
        vertical = Mathf.SmoothStep( vertical, yAxis, MouseAccelerationTime );
        //horizon = Input.GetAxis( "Mouse X" );
        //vertical = Input.GetAxis( "Mouse Y" );
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



    private void OnGUI()
    {
        //GUI.Box( new Rect( 0, 0, 200, 30 ), temp.ToString() );
    }
}
