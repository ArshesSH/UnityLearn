using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_MazeRunner : MonoBehaviour
{
    #region Public Fields
    public static GameManager_MazeRunner Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject gameObject = new GameObject( "_GameManager" );
                instance = gameObject.AddComponent<GameManager_MazeRunner>();
            }
            return instance;
        }
    }

    public GameObject MazeManagerObj
    {
        get
        {
            if(mazeManagerObj == null)
            {
                mazeManagerObj = GameObject.Find( "MazeManager" );
            }
            return mazeManagerObj;
        }
    }

    public MazeManager _MazeManager
    {
        get
        {
            if(mazeManager == null)
            {
                mazeManager = MazeManagerObj.GetComponent<MazeManager>();
            }
            return mazeManager;
        }
    }

    public float PlayerScore
    {
        get
        {
            return _MazeManager.PlayerScore;
        }
    }

    public GameObject Item
    {
        get
        {
            if(item == null)
            {
                item = GameObject.FindWithTag("Item");
            }
            return item;
        }
    }

    #endregion

    #region Private Fields

     static GameManager_MazeRunner instance;

    GameObject mazeManagerObj;
    MazeManager mazeManager;
    GameObject item;

    #endregion


    #region MonoBehaviour Methods

    private void Awake()
    {
        DontDestroyOnLoad( this.gameObject );
    }

    #endregion


    #region Public Methods

    public void ChangeScene( string sceneName )
    {
        SceneManager.LoadScene( sceneName );
    }


    #endregion


    #region Private Methods
    #endregion
}
