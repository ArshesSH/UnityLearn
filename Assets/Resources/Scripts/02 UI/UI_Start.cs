using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Start : MonoBehaviour
{

    private void OnGUI()
    {
        if ( GUI.Button( new Rect( 200.0f, 200.0f, 150.0f, 30.0f ), "Game Start" ) )
        {
        }
    }
}
