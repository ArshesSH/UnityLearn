using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MMX5_MainGame : MonoBehaviour
{
    public enum State
    {
        WaitLoading,
        StartReady,
        ReadySound,
        EndReady,
        Warning,
        Playing
    }

    public Text PlayerNameText;
    public Text PlayerScoreText;
    public GameObject gameOverUI;
    public GameObject victoryUI;
    public GameObject menuObject;
    public GameObject sigmaHPUI;

    public Text userIDText;
    public Text userScoreText;

    public GameObject ReadyUI;
    public GameObject ReadyLeft;
    public GameObject ReadyRight;

    State curState = State.WaitLoading;

    public float LoadingTime = 1.0f;
    public float ReadyAnimDelay = 0.01f;
    public float ReadyMoveSpeed = 800.0f;
    float timer = 0.0f;

    private void Awake()
    {
    }

    void Start()
    {
        ReadyUI.transform.localScale = new Vector2( 0.0f, 1.0f );
        PlayerNameText.text = RockManGameManager.Instance.userID;
        RockManGameManager.Instance.PlayBGM( 0 );
    }

    void Update()
    {
        switch( curState )
        {
            case State.WaitLoading:
            {
                timer += Time.deltaTime;
                if(timer >= LoadingTime )
                {
                    timer = 0.0f;
                    curState = State.StartReady;
                }
            }
            break;
            case State.StartReady:
            {
                PlayReady();
            }
            break;
            case State.ReadySound:
            {
                RockManGameManager.Instance.PlaySFX( 0 );
                curState = State.EndReady;
            }
            break;
            case State.EndReady:
            {
                timer += Time.deltaTime;
                if ( timer >= 1.0f )
                {
                    SeperateReady();
                }
            }
            break;
            case State.Warning:
            {

            }
            break;
            case State.Playing:
            {
                PlayerScoreText.text = RockManGameManager.Instance.Score.ToString();

                if ( RockManGameManager.Instance.IsPlaying() )
                {
                    if(RockManGameManager.Instance.Sigma != null)
                    {
                        sigmaHPUI.SetActive( true );
                    }
                }
                if ( RockManGameManager.Instance.IsGameOver() )
                {
                    gameOverUI.SetActive( true );
                    Time.timeScale = 0.0f;
                }
                if ( RockManGameManager.Instance.IsVictory() )
                {
                    victoryUI.SetActive( true );
                    Time.timeScale = 0.0f;
                    userIDText.text = RockManGameManager.Instance.userID;
                    userScoreText.text = RockManGameManager.Instance.Score.ToString();

                }

                if ( Input.GetKeyDown( KeyCode.Escape ) )
                {
                    if ( menuObject.activeInHierarchy )
                    {
                        menuObject.SetActive( false );
                        Time.timeScale = 1.0f;
                    }
                    else
                    {
                        menuObject.SetActive( true );
                        Time.timeScale = 0.0f;
                    }
                }
            }
            break;
        }
    }

    void onClickRestart()
    {
        Time.timeScale = 1.0f;
        RockManGameManager.Instance.ResetGame();
        gameOverUI.SetActive( false );
        RockManGameManager.Instance.ChangeScene( "09_0 RockmanStart" );
    }
    void onClickContinue()
    {
        menuObject.SetActive( false );
        Time.timeScale = 1.0f;
    }

    void PlayReady()
    {
        if( ReadyUI.transform.localScale.x < 1.0f )
        {
            StrechReady();
        }
        else
        {
            timer = 0.0f;
            curState = State.ReadySound;
        }
    }

    void StrechReady()
    {
        timer += Time.deltaTime;
        if ( timer >= ReadyAnimDelay )
        {
            ReadyUI.transform.localScale = new Vector2( ReadyUI.transform.localScale.x + 0.02f, ReadyUI.transform.localScale.y );
        }
    }

    void SeperateReady()
    {
        if ( ReadyRight.transform.position.x < 1000.0f)
        {
            ReadyRight.transform.Translate( ReadyMoveSpeed * Time.deltaTime * Vector3.right );
            ReadyLeft.transform.Translate( -ReadyMoveSpeed * Time.deltaTime * Vector3.right );
        }
        else
        {
            Destroy( ReadyUI );
            timer = 0.0f;
            RockManGameManager.Instance.SetPlaying();
            RockManGameManager.Instance.StopPlayerMove( true );
            curState = State.Warning;
        }
    }

}
