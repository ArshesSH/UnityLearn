using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManagement : MonoBehaviour
{
    public enum MoveMode
    {
        Forward,
        Backward
    }

    [SerializeField]
    private Vector3 frontWheelAligmentCenter;
    [SerializeField]
    private Vector3 rearWheelAligmentCenter;
    [SerializeField]
    private float maxSpeed = 5.0f;
    [SerializeField]
    private float maxAccel = 1.0f;
    [SerializeField]
    private float accelPower = 1.0f;
    [SerializeField]
    private float brakePower = 5.0f;
    [SerializeField]
    private float frictionPower = 0.01f;
    [SerializeField]
    private float aeroDynamicCoef = 0.001f;
    [SerializeField]
    private float angularSpeed = 50.0f;


    private Vector3 vel;
    private Vector3 moveDir;
    private float physicsSpeed;
    private Vector3 moveVel;
    private float curAccel = 0.0f;
    private float curFriction = 0.0f;
    private Vector3 lastPos;
    private MoveMode curMoveMode = MoveMode.Forward;
    private bool isBraking;
    private float curAngle;
    public float CurAngle
    {
        get { return curAngle; }
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        moveDir = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        CheckVelocity();
        CheckMoveMode();
        ControlCar();
        Move();
    }

    void ControlCar()
    {
        if( Input.GetKey( KeyCode.Space ) )
        {
            Braking( KeyCode.Space );
            Decelerate();
        }

        if( Input.GetKey(KeyCode.D))
        {
            RotateByRearAlignment( angularSpeed * Time.deltaTime);
        }
        if ( Input.GetKey( KeyCode.A ) )
        {
            RotateByRearAlignment( -angularSpeed * Time.deltaTime );
        }

        switch ( curMoveMode )
        {
            case MoveMode.Forward:
            {
                moveDir = transform.forward;
                Braking( KeyCode.S );
                if ( Input.GetKey( KeyCode.W ) )
                {
                    Accelerate();
                }
                else
                {
                    Decelerate();
                }
            }
            break;

            case MoveMode.Backward:
            {
                moveDir = -transform.forward;
                Braking( KeyCode.W );
                if ( Input.GetKey( KeyCode.S ) )
                {
                    Accelerate();
                }
                else
                {
                    Decelerate();
                }
            }
            break;
        }
    }

    void CheckVelocity()
    {
        vel = ( transform.position - lastPos) / Time.deltaTime;
        physicsSpeed = vel.magnitude;
        lastPos = transform.position;
    }
    void CheckMoveMode()
    {
        if ( Mathf.Approximately( physicsSpeed, 0.0f ) )
        {
            if ( Input.GetKey( KeyCode.W ) )
            {
                curMoveMode = MoveMode.Forward;
            }
            else
            {
                curMoveMode = MoveMode.Backward;
            }
        }
    }

    void Move()
    {
        if ( physicsSpeed <= maxSpeed )
        {
            moveVel += curAccel * Time.deltaTime * moveDir;
        }
        transform.position += moveVel;
    }

    void Accelerate()
    {
        curFriction = 0.0f;

        if ( curAccel <= maxAccel )
        {
            curAccel += accelPower * Time.deltaTime;
        }
    }
    void Decelerate()
    {
        curAccel = 0.0f;
        ApplyFriction();
    }
    void Braking(KeyCode key)
    {
        if ( Input.GetKey( key ) )
        {
            isBraking = true;
        }
        else
        {
            isBraking = false;
        }
    }

    void ApplyFriction()
    {
        float addedFriction = (isBraking) ? frictionPower + brakePower : frictionPower;
        float aero = (physicsSpeed * aeroDynamicCoef);
        curFriction = addedFriction * Time.deltaTime + aero;


        if ( physicsSpeed > 0.0f)
        {
            moveVel -= curFriction * Time.deltaTime * vel.normalized;


            if ( physicsSpeed < 0.1f )
            {
                moveVel = new Vector3();
            }
        }

    }


    void RotateByRearAlignment( float speed)
    {
        transform.Rotate( rearWheelAligmentCenter, speed, Space.World );
    }

    private void OnGUI()
    {
        GUI.Box( new Rect( 0.0f, 0.0f, 150.0f, 30.0f ), "Vel = " + (physicsSpeed).ToString() );
        GUI.Box( new Rect( 0.0f, 30.0f, 150.0f, 30.0f ), "Acc = " + curAccel);
        GUI.Box( new Rect( 0.0f, 60.0f, 150.0f, 30.0f ), "Fric = " + curFriction );
        GUI.Box( new Rect( 0.0f, 90.0f, 150.0f, 30.0f ), "MoveMode: " + curMoveMode );
        GUI.Box( new Rect( 0.0f, 120.0f, 150.0f, 30.0f ), "Braking: " + isBraking );
        GUI.Box( new Rect( 0.0f, 150.0f, 150.0f, 30.0f ), "Angle: " + transform.rotation.eulerAngles.y );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        var localPos = transform.localToWorldMatrix * rearWheelAligmentCenter;
        Gizmos.DrawLine( localPos, localPos + new Vector4( 0, 2, 0 ) );
    }
}
