using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSlider : MonoBehaviour
{
    #region Public Fields

    

    #endregion


    #region Private Fields

    [Tooltip( "Pixel offset from the player target" )]
    [SerializeField]
    private Vector3 screenOffset = new Vector3( 0f, 30f, 0f );

    [Tooltip( "UI Slider to display Player's Health" )]
    [SerializeField]
    private Slider playerHealthSlider;

    [SerializeField]
    private GameObject camObj;
    Camera cam;

    GameObject playerObj;


    float controllerHeight;

    Vector3 targetPosition;

    #endregion


    #region MonoBehaviour Methods

    private void Awake()
    {
        transform.SetParent( GameObject.Find( "Canvas" ).GetComponent<Transform>(), false );
        camObj = GameObject.Find( "Camera" );
        if(camObj == null)
        {
            Debug.LogError( "Missing Camera" );
        }

        cam = camObj.GetComponent<Camera>();
        if ( cam == null )
        {
            Debug.LogError( "Missing Camera Component" );
        }
    }
    private void Update()
    {
        if ( playerHealthSlider != null )
        {
            playerHealthSlider.value = GameManager_MazeRunner.Instance.PlayerScore;
        }
    }

    private void LateUpdate()
    {
        if ( playerObj.transform != null )
        {
            targetPosition = playerObj.transform.localPosition;
            targetPosition.y += controllerHeight;
            transform.position = cam.WorldToScreenPoint( targetPosition ) + screenOffset;
        }
    }

    #endregion


    #region Public Methods
    public void SetTarget(RunnerController runnerController)
    {
        if( runnerController == null)
        {
            Debug.LogError( "ScoreSlider: <Color=Red><a>Missing</a></Color> MazeRunnerController Set Target obj is null", this );
            return;
        }

        playerObj = runnerController.gameObject;

        CharacterController controller = playerObj.GetComponent<CharacterController>();
        if(controller != null)
        {
            controllerHeight = controller.height;
        }

    }
    #endregion


    #region Private Methods
    #endregion
}
