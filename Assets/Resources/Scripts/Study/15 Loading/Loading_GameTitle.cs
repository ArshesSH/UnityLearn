using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loading_GameTitle : MonoBehaviour
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
    void GoNextScene()
    {
        Debug.Log( "GoNextScene - change Loading Scene" );
        GameManager.Instance.nextSceneName = "09_0 RockmanStart";
        SceneManager.LoadScene( "15_1 Loading" );
    }
    #endregion
}
