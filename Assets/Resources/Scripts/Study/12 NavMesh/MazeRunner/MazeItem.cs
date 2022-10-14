using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeItem : MonoBehaviour
{
    #region Public Fields
    public float Point = 1.0f;
    public string PlayerTagName = "Player";
    public string EnemyTagName = "Enemy";
    public float RotateSpeed = 10.0f;
    #endregion


    #region Private Fields

    Vector3 rotateVector;

    #endregion


    #region MonoBehaviour Methods
    private void Start()
    {
        rotateVector = new Vector3(0, RotateSpeed, 0);
    }
    private void Update()
    {
        transform.Rotate(rotateVector * Time.deltaTime);
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
