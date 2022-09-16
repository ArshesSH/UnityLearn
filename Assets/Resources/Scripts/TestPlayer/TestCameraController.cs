using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraController : MonoBehaviour
{
    #region Public Fields

    [Header( "Object Setting" )]
    [SerializeField]
    GameObject camTarget;

    [Header( "Speed Setting" )]
    [SerializeField]
    float mouseSpeed = 1.5f;
    [SerializeField]
    float mouseAccelerationTime = 0.15f;
    [SerializeField]
    float zoomSpeed = 20.0f;
    [SerializeField]
    float zoomAccelerationTime = 0.15f;

    [Header( "Limit Setting" )]
    [SerializeField]
    float minAngleX = 180;
    [SerializeField]
    float maxAngleX = 340;
    [SerializeField]
    float minAngleY = -40.0f;
    [SerializeField]
    float maxAngleY = 60.0f;

    [Header( "Invert Setting" )]
    [SerializeField]
    bool InvertX;
    [SerializeField]
    bool InvertY;
    [SerializeField]
    bool InvertZoom;

    #endregion


    #region Private Fields
    float xAxis;
    float yAxis;
    float vertical;
    float horizontal;
    float wheelValRaw;
    float wheelVal;
    Vector3 angle;
    #endregion


    #region MonoBehaviour Callbacks
    private void Start()
    {
        angle = camTarget.transform.localEulerAngles;
    }
    private void Update()
    {
        MouseInput();
        RotateCamera();
    }
    #endregion


    #region Public Methods
    #endregion


    #region Private Methods
    void MouseInput()
    {
        xAxis = (InvertX ? -Input.GetAxis( "Mouse X" ) : Input.GetAxis( "Mouse X" )) * mouseSpeed;
        yAxis = (InvertY ? -Input.GetAxis( "Mouse Y" ) : Input.GetAxis( "Mouse Y" )) * mouseSpeed;
        wheelValRaw = (InvertZoom ? -Input.GetAxis( "Mouse ScrollWheel" ) : Input.GetAxis( "Mouse ScrollWheel" )) * zoomSpeed;

        horizontal = Mathf.SmoothStep( horizontal, xAxis, mouseAccelerationTime );
        vertical = Mathf.SmoothStep( vertical, yAxis, mouseAccelerationTime );
        wheelVal = Mathf.SmoothStep( wheelVal, wheelValRaw, zoomAccelerationTime );
    }
    void RotateCamera()
    {
        camTarget.transform.rotation = Quaternion.Lerp( camTarget.transform.rotation,
            camTarget.transform.rotation * Quaternion.AngleAxis( horizontal * mouseSpeed, Vector3.up ), mouseAccelerationTime );
        //curVerticalAngle += vertical;
        //if ( curVerticalAngle <= MaxAngleY && curVerticalAngle >= MinAngleY )
        //{
        //    transform.RotateAround( Target.transform.position, transform.right, vertical );
        //}
        //else
        //{
        //    curVerticalAngle -= vertical;
        //}
    }
    #endregion
}
