using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeItem : MonoBehaviour
{
    #region Public Fields
    public float Point = 1.0f;
    public string PlayerTagName = "Player";
    public string EnemyTagName = "Enemy";

    #endregion


    #region Private Fields
    #endregion


    #region MonoBehaviour Methods
    private void Start()
    {
	
    }
    private void Update()
    {
	
    }

    private void OnTriggerEnter( Collider other )
    {
        if(other.gameObject.CompareTag(PlayerTagName))
        {
            GameManager_MazeRunner.Instance._MazeManager.AddPlayerScore( Point );
            GameManager_MazeRunner.Instance._MazeManager.IsItemExist = false;
            Destroy( gameObject );
        }
    }

    #endregion


    #region Public Methods
    #endregion


    #region Private Methods
    #endregion
}
