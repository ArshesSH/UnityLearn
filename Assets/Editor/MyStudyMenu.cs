using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditor.SceneManagement;

public class MyStudyMenu : MonoBehaviour
{
    #region Public Fields
    #endregion


    #region Private Fields
    #endregion


    #region MonoBehaviour Callbacks
    private void Start()
    {
	
    }
    private void Update()
    {
	
    }
    #endregion


    #region Public Methods
    #endregion


    #region Private Methods
    [MenuItem( "MyStudyMenu/Clear PlayerPrefs %#]" )]
    private static void ClearPlayerPrefsAll()
    {
        PlayerPrefs.DeleteAll();
        print( "ClearPlayerPrefsAll Called" );
    }

    [MenuItem( "MyStudyMenu/SubMenu/Select" )]
    private static void SubMenuSelect()
    {
        print( "SubMenuSelect Called" );
    }

    [MenuItem( "MyStudyMenu/SubMenu/HotKeyTest1 %#[" )]
    private static void SubMenu_HotKey_1()
    {
        // % - ctrl, # - shift, & - alt
        // 즉 이 단축키는 ctrl + shift + [ 인거임
        print( "SubMenu_HotKey_1 called" );
    }

    [MenuItem("Assets/Load Selected Scene")]
    private static void LoadSelectedScene()
    {
        var selected = Selection.activeObject;

        if(EditorApplication.isPlaying)
        {
            EditorSceneManager.LoadScene( AssetDatabase.GetAssetPath( selected ) );
        }
        else
        {
            EditorSceneManager.OpenScene( AssetDatabase.GetAssetPath( selected ) );
        }

    }

    #endregion
}
