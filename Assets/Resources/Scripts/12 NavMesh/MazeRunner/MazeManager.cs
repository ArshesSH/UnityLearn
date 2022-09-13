using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    #region Public Fields
    [Header("Item Setting")]
    [SerializeField]
    private GameObject itemObj;

    [SerializeField]
    private Transform[] itemSpawnPoints;

    public bool IsItemExist = false;

    [Header("Player Setting")]
    public float PlayerScore = 5.0f;

    #endregion


    #region Private Fields
    #endregion


    #region MonoBehaviour Methods

    private void Start()
    {
        if (itemObj == null)
        {
            Debug.LogError("MazeManager: No Item Object set!");
        }
    }
    private void Update()
    {
        if (!IsItemExist)
        {
            SpawnItem();
        }

    }

    #endregion


    #region Public Methods

    public void SpawnItem()
    {
        if (itemObj == null)
        {
            return;
        }
        int posIdx = Random.Range(0, itemSpawnPoints.Length);
        GameObject obj = Instantiate(itemObj, itemSpawnPoints[posIdx]);
        IsItemExist = true;
    }
    public void AddPlayerScore(float score)
    {
        PlayerScore += score;
    }
    public bool IsDead()
    {
        return PlayerScore <= 0.0f;
    }

    public bool IsVictory()
    {
        return PlayerScore >= 10.0f;
    }

    #endregion


    #region Private Methods



    #endregion

}
