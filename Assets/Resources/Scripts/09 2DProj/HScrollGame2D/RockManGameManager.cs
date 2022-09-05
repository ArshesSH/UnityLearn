using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockManGameManager : MonoBehaviour
{
    private static RockManGameManager sInstance;
    public static RockManGameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject gameObject = new GameObject("_GameManager");
                sInstance = gameObject.AddComponent<RockManGameManager>();
            }
            return sInstance;
        }
    }

    GameObject playerX;
    public GameObject PlayerX
    {
        get
        {
            if(playerX == null)
            {
                playerX = GameObject.Find("PlayerX");
            }
            return playerX;
        }
    }

    PlayerXController xController;
    public PlayerXController XController
    {
        get
        {
            if ( xController == null )
            {
                xController = PlayerX.GetComponent<PlayerXController>();
            }
            return xController;
        }
    }

    GameObject sigma;
    public GameObject Sigma
    {
        get
        {
            if(sigma == null)
            {
                sigma = GameObject.Find("SigamHead");
            }
            return sigma;
        }
    }
    SigmaBehaviour sigmaBehaviour;
    public SigmaBehaviour SigmaBv
    {
        get
        {
            if(sigmaBehaviour == null)
            {
                sigmaBehaviour = Sigma.GetComponent<SigmaBehaviour>();
            }
            return sigmaBehaviour;
        }
    }

    GameObject soundManagerObj;
    public GameObject SoundMangerObj
    {
        get
        {
            if(soundManagerObj == null)
            {
                soundManagerObj = GameObject.Find( "SoundManager" );
            }
            return soundManagerObj;
        }
    }

    RockManSoundManager soundManager;
    public RockManSoundManager SoundManager
    {
        get
        {
            if(soundManager == null)
            {
                soundManager = SoundMangerObj.GetComponent<RockManSoundManager>();
            }
            return soundManager;
        }
    }

    public enum GameState
    {
        Ready,
        Playing,
        GameOver,
        Victory
    }

    public string userID;
    public int Score = 3000;
    public bool isSigmaStartDestroy = false;
    public bool isSigmaDestroy = false;

    GameState gameState = GameState.Ready;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }   

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public bool IsPlayerFacingRight()
    {
        return xController.IsFacingRight();
    }

    public bool CanAttackSigma()
    {
        return SigmaBv.canDamaged;
    }


    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameState = GameState.GameOver;
    }

    public void ResetGame()
    {
        Time.timeScale = 1.0f;
        gameState = GameState.Ready;
        sigma = null;
        playerX = null;
        xController = null;
        sigmaBehaviour = null;
    }

    public bool IsPlaying()
    {
        return gameState == GameState.Playing;
    }
    public bool IsGameOver()
    {
        return gameState == GameState.GameOver;
    }
    public bool IsVictory()
    {
        return gameState == GameState.Victory;
    }
    public void SetPlaying()
    {
        gameState = GameState.Playing;
    }
    public void SetGameOver()
    {
        gameState = GameState.GameOver;
    }
    public void SetVictory()
    {
        gameState = GameState.Victory;
    }

    public bool PlayBGM(int index)
    {
        return SoundManager.PlayBGM( index );
    }
    public bool PlaySFX(int index)
    {
        return SoundManager.PlaySFX( index );
    }

    public bool PlayBGM( string name )
    {
        return SoundManager.PlayBGM( name );
    }
    public bool PlaySFX(string name)
    {
        return SoundManager.PlaySFX( name );
    }

}
