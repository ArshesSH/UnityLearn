using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ��ȯ�� ���� �ʿ���
//using UnityEngine.SceneManagement;

public class UI_Start : MonoBehaviour
{
    private void OnGUI()
    {
        if ( GUI.Button( new Rect( 400.0f, 250.0f, 150.0f, 30.0f ), "Game Start" ) )
        {
            GameManager.Instance.ChangeScene( "03 Game" );
        }
    }
}
