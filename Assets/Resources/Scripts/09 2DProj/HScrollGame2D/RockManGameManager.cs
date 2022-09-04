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
    PlayerXController xController;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        playerX = GameObject.Find("PlayerX");
        xController = playerX.GetComponent<PlayerXController>();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public bool IsPlayerFacingRight()
    {
        return xController.IsFacingRight();
    }


}
