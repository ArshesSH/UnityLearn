using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SPDGameManager : MonoBehaviour
{ 
    private static SPDGameManager sInstance;

    public static SPDGameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject gameObject = new GameObject("_GameManager");
                sInstance = gameObject.AddComponent<SPDGameManager>();
            }
            return sInstance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    [SerializeField]
    private int towerCountMax = 8;
    public int towerCount = 8;
    [SerializeField]
    private float genMaxTime = 5.0f;
    public float genTime = 5.0f;
    public int score = 0;

    public void IncreaseDifficulty()
    {
        if( genTime >= 0.5f)
        {
            genTime = genMaxTime - score * 0.1f;
        }
    }

    public bool IsGameEnd()
    {
        return towerCount == 0;
    }

    public void ResetGame()
    {
        genTime = genMaxTime;
        score = 0;
        towerCount = towerCountMax;
    }

}
