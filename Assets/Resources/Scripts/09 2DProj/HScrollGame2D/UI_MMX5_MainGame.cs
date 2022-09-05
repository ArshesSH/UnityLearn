using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MMX5_MainGame : MonoBehaviour
{
    public Text PlayerNameText;
    public Text PlayerScoreText;
    public GameObject gameOverUI;
    public GameObject victoryUI;
    public GameObject menuObject;
    public GameObject sigmaHPUI;

    public Text userIDText;
    public Text userScoreText;


    void Start()
    {
        PlayerNameText.text = RockManGameManager.Instance.userID;
    }

    void Update()
    {
        PlayerScoreText.text = RockManGameManager.Instance.Score.ToString();

        if ( RockManGameManager.Instance.IsPlaying() )
        {
            sigmaHPUI.SetActive( true );
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

    }

}
