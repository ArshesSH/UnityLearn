using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditor.SceneManagement;

public class CheatWindow : EditorWindow
{
    #region Public Fields
    #endregion


    #region Private Fields

    static int selectIdx = 0;

    int getInt = 0;
    string getString = "";

    string[] cheatList = new string[] { "치트", "골드생성", "포인트 생성", };
    #endregion


    #region Public Methods
    #endregion


    #region Private Methods
    [MenuItem("MyStudyMenu/SubMenu/치트 명령창1", false, 0)]
    static public void OpenCheatWindow_1()
    {
        CheatWindow cheatWindow = EditorWindow.GetWindow<CheatWindow>(false, "CheatWindow", true);
    }
    private void OnGUI()
    {
        GUILayout.Space( 10.0f );
        int getIdx = EditorGUILayout.Popup( selectIdx, cheatList, GUILayout.MaxWidth(200.0f) );

        if ( selectIdx != getIdx )
        {
            selectIdx = getIdx;
        }
        string cheatText = "";
        GUILayout.BeginHorizontal( GUILayout.MaxWidth( 300.0f ) );
        {
            switch ( selectIdx )
            {
                case 0:
                {
                    GUILayout.Label( "치트키 입력", GUILayout.Width( 70.0f ) );
                    getString = EditorGUILayout.TextField( getString, GUILayout.Width( 100.0f ) );
                    cheatText = string.Format( "치트키:{0}", getString );
                }
                break;
                case 1:
                {
                    GUILayout.Label( "골드", GUILayout.Width( 70.0f ) );
                    getString = EditorGUILayout.TextField( getInt.ToString(), GUILayout.Width( 100.0f ) );
                    int.TryParse( getString, out getInt );
                    cheatText = string.Format( "골드:{0}", getInt );
                }
                break;
                case 2:
                {
                    GUILayout.Label( "포인트", GUILayout.Width( 70.0f ) );
                    getString = EditorGUILayout.TextField( getInt.ToString(), GUILayout.Width( 100.0f ) );
                    int.TryParse( getString, out getInt );
                    cheatText = string.Format( "포인트:{0}", getInt );
                }
                break;
            }
        }
        GUILayout.EndHorizontal();

        //-------
        GUILayout.Space( 20.0f );
        GUILayout.BeginHorizontal( GUILayout.MaxWidth( 800.0f ) );
        {
            GUILayout.BeginVertical( GUILayout.MaxWidth( 300.0f ) );
            {
                GUILayout.BeginHorizontal( GUILayout.MaxWidth( 300.0f ) );
                {
                    if(GUILayout.Button("\n적용\n", GUILayout.Width(100.0f)))
                    {
                        getInt = 0;
                        getString = "";
                        Debug.Log( cheatText );
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal( GUILayout.MaxWidth( 300.0f ) );
                {
                    if ( GUILayout.Button( "\n백그라운드\n실행\n", GUILayout.Width( 100.0f ) ) )
                    {
                        Application.runInBackground = true;
                    }
                    if ( GUILayout.Button( "\n백그라운드\n실행해제\n", GUILayout.Width( 100.0f ) ) )
                    {
                        Application.runInBackground = false;
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }
    #endregion
}
