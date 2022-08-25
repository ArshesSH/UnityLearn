using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad( this.gameObject );
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene( sceneName );
    }


    // Signletone
    private static GameManager sInstance;

    public static GameManager Instance
    {
        get
        {
            if ( sInstance == null )
            {
                GameObject gameObject = new GameObject( "_GameManager" );
                sInstance = gameObject.AddComponent<GameManager>();
            }
            return sInstance;
        }
    }

    public int Score = 0;
    public string userID = "";
    
}
