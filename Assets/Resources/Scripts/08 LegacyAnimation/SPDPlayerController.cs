using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPDPlayerController : MonoBehaviour
{
    public enum State
    {
        Idle,
        IdleBattle,
        Attack,
        Walk,
        Run,
        Die
    }

    [Header( "Weapon Settings" )]
    [SerializeField]
    protected GameObject WeaponCollider;


    [Header("Character Settings")]
    [SerializeField]
    protected int maxHP = 3;
    protected int curHP;

    [SerializeField]
    protected float moveSpeed = 6.0f;
    [SerializeField]
    protected float rotateSpeed = 360.0f;
    [SerializeField]
    protected float camZoomFactor = 20.0f;


    private Camera cam;
    protected State state;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        PlayerInput(); 
    }

    private void PlayerInput()
    {
        ZoomCamera();
        RotateCamera();
    }


    private void RotateCamera()
    {
        cam.transform.RotateAround( transform.position, Vector3.up, Input.GetAxisRaw( "Mouse X" ) );
    }

    private void ZoomCamera()
    {
        cam.fieldOfView -= (camZoomFactor * Input.GetAxis( "Mouse ScrollWheel" ));
        // °¢Á¦ÇÑ
        if ( cam.fieldOfView < 20 )
        {
            cam.fieldOfView = 20;
        }
        if ( cam.fieldOfView > 80 )
        {
            cam.fieldOfView = 80;
        }
    }

    private void OnGUI()
    {

    }

}
