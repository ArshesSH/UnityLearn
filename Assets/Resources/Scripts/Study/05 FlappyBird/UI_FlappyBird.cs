using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FlappyBird : MonoBehaviour
{
    [SerializeField]
    private GameObject birdObj;

    Bird bird;

    private void Start()
    {
        bird = birdObj.GetComponent<Bird>();
    }

    private void OnGUI()
    {
        if(GameManager_FlappyBird.Instance.IsGameWaiting())
        {
            if ( GUI.Button( new Rect( 600, 30, 150, 30 ), "Game Start" ) )
            {
                GameManager_FlappyBird.Instance.StartGame();
            }
        }

        if( GameManager_FlappyBird.Instance.IsGamePlaying())
        {
            GUI.Box( new Rect( 600, 60, 150, 30 ), "Score : " + GameManager_FlappyBird.Instance.Score );

            GUI.Box( new Rect( 600, 90, 150, 30 ), bird.attackState.ToString() );
        }

        if(GameManager_FlappyBird.Instance.IsGameEnd())
        {
            GUI.Box( new Rect( 600, 60, 150, 30 ), "Final Score : " + GameManager_FlappyBird.Instance.Score );
            if(GUI.Button( new Rect( 350, 0, 150, 60 ), "Restart"))
            {
                GameManager_FlappyBird.Instance.ResetGame();
                GameManager_FlappyBird.Instance.ChangeScene( "06 FlappyBird" );
            }
        }

        

    }
}
