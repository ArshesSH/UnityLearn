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
        get { return playerX; }
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
            return xController;
        }
    }

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

    public bool CanAttackSigma()
    {
        return SigmaBv.canDamaged;
    }


}
