using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_FlappyBird : MonoBehaviour
{
    private int score = 0;
    bool isAttackNow = false;
    public bool IsAttackNow
    {
        get { return isAttackNow; }
    }
    public int Score
    {
        get { return score; }
    }

    public enum GameState
    {
        WaitForGameStart,
        Playing,
        GameEnd
    }
    GameState curGameState = GameState.WaitForGameStart;

    // Signletone
    private static GameManager_FlappyBird sInstance;

    public static GameManager_FlappyBird Instance
    {
        get
        {
            if ( sInstance == null )
            {
                GameObject gameObject = new GameObject( "_GameManager" );
                sInstance = gameObject.AddComponent<GameManager_FlappyBird>();
            }
            return sInstance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad( this.gameObject );
    }

    public void AddScore(int score_in)
    {
        score += score_in;
    }

    public void StartGame()
    {
        curGameState = GameState.Playing;
    }
    public void EndGame()
    {
        curGameState = GameState.GameEnd;
    }
    public void ResetGame()
    {
        score = 0;
        curGameState = GameState.Playing;
    }

    public bool IsGameWaiting()
    {
        return curGameState == GameState.WaitForGameStart;
    }
    public bool IsGamePlaying()
    {
        return curGameState == GameState.Playing;
    }
    public bool IsGameEnd()
    {
        return curGameState == GameState.GameEnd;
    }
    public void ChangeScene( string sceneName )
    {
        SceneManager.LoadScene( sceneName );
    }

    public void SetIsAttackNow(bool flag = true)
    {
        isAttackNow = flag;
    }
}
