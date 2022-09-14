using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerController : MonoBehaviour
{
    #region Public Fields

    [Header("Character Setting")]
    [SerializeField]
    float moveSpeed;

    #endregion


    #region Protected Fields

    protected float horizontal;
    protected float vertical;

    #endregion


    #region Private Fields



    #endregion


    #region MonoBehaviour Callbacks
    private void Start()
    {
	    
    }
    private void Update()
    {
	    
    }
    #endregion


    #region Public Methods
    #endregion

    #region Protected Methods
    protected virtual void GetHorizontal()
    {
        horizontal = Input.GetAxis( "Horizontal" );
    }
    protected virtual void GetVertical()
    {
        vertical = Input.GetAxis( "Vertical" );
    }
    #endregion

    #region Private Methods
    #endregion
}
