using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : UCSSU_Controller
{
    #region Public Fields

    [Header( "UI Setting" )]
    [SerializeField]
    private GameObject scoreSliderPrefab;

    #endregion


    #region Private Fields


    #endregion


    #region MonoBehaviour Methods
    protected override void Start()
    {
        base.Start();

        if ( scoreSliderPrefab != null )
        {
            GameObject _uiGo = Instantiate( scoreSliderPrefab );
            _uiGo.SendMessage( "SetTarget", this, SendMessageOptions.RequireReceiver );
        }
        else
        {
            Debug.LogWarning( "<Color=Red><a>Missing</a></Color> scoreSliderPrefab reference on player Prefab.", this );
        }
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
    }


    #endregion


    #region Public Methods
    #endregion


    #region Private Methods
    #endregion
}
