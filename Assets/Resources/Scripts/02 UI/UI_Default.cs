using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Default : MonoBehaviour
{
    private int count = 0;

    private void OnGUI()
    {
        string str = "Test Text";
        string str2 = "Test Text2";
        string str3 = "Click Count : " + count.ToString();
        
        // Print Text
        GUI.TextArea( new Rect( 200.0f, 50.0f, 100.0f, 30.0f ), str );
        GUI.TextField( new Rect( 200.0f, 100.0f, 100.0f, 30.0f ), str2 );


        // Interact with gui
        GUI.Box( new Rect( 200.0f, 170.0f, 150.0f, 30.0f ), str3 );
        if( GUI.Button( new Rect( 200.0f, 200.0f, 150.0f, 30.0f ), "버튼을 클릭하시오" ) )
        {
            count++;
        }

    }
}
