using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSH_Lib
{
	public class TPV_RestrictCamController : TPV_CameraController_v2
	{
        /*--- Public Fields ---*/


        /*--- Protected Fields ---*/


        /*--- Private Fields ---*/


        /*--- MonoBehaviour Callbacks ---*/


        /*--- Public Methods ---*/

        /*--- Protected Methods ---*/
        protected override void RotateCamera()
        {
            if ( BasePlayerInputManager.Instance.CanRotateCamera() )
            {
                base.RotateCamera();
            }
        }
        /*--- Private Methods ---*/
    }
}