using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

namespace KSH_Lib
{
    public class TPV_CameraController_v2 : BaseCameraController
    {
        /*--- Inspector ---*/
        [Header ( "Camera Speed Setting" )]
        [SerializeField]
        protected float zoomSpeed = 0.001f;
        [SerializeField]
        protected float zoomAccel = 0.15f;
        
        [Header ("Limit Setting")]
        [SerializeField]
        protected float minZoomLength = 2.0f;
        [SerializeField]
        protected float maxZoomLength = 5.0f;

        [Header("Invert Setting")]
        [SerializeField]
        bool isInvertZoom = false;


        /*--- Private Field ---*/
        Cinemachine3rdPersonFollow cm3rdPersonFollow;
        protected float zoomValRaw;
        protected float zoomVal;


        /*--- Monobehaviour Callbacks ---*/
        protected override void Start()
        {
            base.Start();
            cm3rdPersonFollow = virtualCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            if ( cm3rdPersonFollow == null )
            {
                Debug.LogError( "BaseCameraController: Can not find Cinemachine3rdPersonFollow" );
            }
        }
        protected override void LateUpdate()
        {
            if(!canUpdate)
            {
                return;
            }

            base.LateUpdate();
            ZoomCamera();
        }


        /*--- Protected Methods ---*/
        protected override void GetDataFromInputManager()
        {
            base.GetDataFromInputManager();

            zoomValRaw = zoomSpeed * BasePlayerInputManager.Instance.GetCameraZoom();
            if ( isInvertZoom )
            {
                zoomValRaw = -zoomValRaw;
            }
        }
        protected override void SmoothInputData()
        {
            base.SmoothInputData();
            zoomVal = Mathf.SmoothStep( zoomVal, zoomValRaw, zoomAccel );
        }
        protected virtual void ZoomCamera()
        {
            cm3rdPersonFollow.CameraDistance += zoomVal;
            if ( cm3rdPersonFollow.CameraDistance <= minZoomLength )
            {
                cm3rdPersonFollow.CameraDistance = minZoomLength;
            }
            else if ( cm3rdPersonFollow.CameraDistance >= maxZoomLength )
            {
                cm3rdPersonFollow.CameraDistance = maxZoomLength;
            }
        }

        /*--- Private Methods ---*/

    }
}