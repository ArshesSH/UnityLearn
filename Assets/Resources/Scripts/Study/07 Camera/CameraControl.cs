using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float zoomAmount = 20.0f;

    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();
        ZoomCamera();
    }

    private void LateUpdate()
    {
    }

    void MoveCamera()
    {
        // Left Click
        if(Input.GetMouseButton(0))
        {
            transform.Translate( Input.GetAxisRaw( "Mouse X" ) / 10.0f, Input.GetAxisRaw( "Mouse Y" ) / 10.0f, 0.0f);
        }
    }

    void RotateCamera()
    {
        if(Input.GetMouseButton(1))
        {
            transform.Rotate(-Input.GetAxis( "Mouse Y" ) /10.0f, -Input.GetAxis( "Mouse X" ) / 10.0f, 0.0f);
        }
    }

    void ZoomCamera()
    {
        mainCamera.fieldOfView -= (zoomAmount * Input.GetAxis( "Mouse ScrollWheel" ));
        // °¢Á¦ÇÑ
        if ( mainCamera.fieldOfView < 10 )
        {
            mainCamera.fieldOfView = 10;
        }
        if ( mainCamera.fieldOfView > 150 )
        {
            mainCamera.fieldOfView = 150;
        }
    }
}
