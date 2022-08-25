using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_End : MonoBehaviour
{
    private void OnGUI()
    {
        if ( GUI.Button( new Rect( 400.0f, 250.0f, 150.0f, 30.0f ), "Game End" ) )
        {
            SceneManager.LoadScene( "00 Start" );
        }
    }
}
