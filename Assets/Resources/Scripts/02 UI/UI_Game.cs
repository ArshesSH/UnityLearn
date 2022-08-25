using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class UI_Game : MonoBehaviour
{
    private void OnGUI()
    {
        if ( GUI.Button( new Rect( 400.0f, 250.0f, 150.0f, 30.0f ), "Check Game Scene" ) )
        {
            SceneManager.LoadScene( "99 End" );
        }
    }
}
