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

    PlayerXController xController;
    public PlayerXController XController
    {
        get
        {
            if(xController == null)
            {
                xController = playerX.GetComponent<PlayerXController>();
            }
            return xController;
        }
    }

    public string userID;
    public int Score = 3000;
    public bool isGameOver = false;
    public bool isSigmaStartDestroy = false;
    public bool isSigmaDestroy = false;
    public bool isVictory = false;

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
        isGameOver = true;
    }

    public void ResetGame()
    {
        Time.timeScale = 1.0f;
        isGameOver = false;
        sigma = null;
        playerX = null;
        xController = null;
        sigmaBehaviour = null;
    }

}
