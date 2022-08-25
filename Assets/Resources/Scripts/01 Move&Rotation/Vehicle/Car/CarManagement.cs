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
    private float maxSpeed = 30.0f;
    [SerializeField]
    private float maxAccel = 1.0f;
    [SerializeField]
    private float accelPower = 1.0f;
    [SerializeField]
    private float brakePower = 2.0f;
    [SerializeField]
    private float frictionPower = 0.1f;
    [SerializeField]
    private float aeroDynamicCoef = 0.001f;


    private Vector3 vel;
    private Vector3 moveDir;
    private float physicsSpeed;
    private Vector3 moveVel;
    private float curAccel = 0.0f;
    private float curFriction = 0.0f;
    private float angularSpeed;
    private Vector3 lastPos;
    private MoveMode curMoveMode = MoveMode.Forward;
    private bool isBrake;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
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
        switch ( curMoveMode )
        {
            case MoveMode.Forward:
            {
                if ( Input.GetKey( KeyCode.S ) )
                {
                    Braking();
                }
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
        if( Vector3.Dot( vel, transform.forward ) >= 0.0f )
        {
            curMoveMode = MoveMode.Forward;
        }
        else
        {
            curMoveMode = MoveMode.Backward;
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

    void Braking()
    {
        if ( Input.GetKey( KeyCode.S ) )
        {
            isBrake = true;
        }
        else
        {
            isBrake = false;
        }
    }

    void ApplyFriction()
    {
        float addedFriction = (isBrake) ? frictionPower + brakePower : frictionPower;
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


    void Rotate()
    {
        transform.Rotate( rearWheelAligmentCenter, angularSpeed * Time.deltaTime, Space.Self );
    }

    private void OnGUI()
    {
        GUI.Box( new Rect( 0.0f, 0.0f, 150.0f, 30.0f ), "Vel = " + (physicsSpeed).ToString() );
        GUI.Box( new Rect( 0.0f, 30.0f, 150.0f, 30.0f ), "Acc = " + curAccel);
        GUI.Box( new Rect( 0.0f, 60.0f, 150.0f, 30.0f ), "Fric = " + curFriction );
    }
}
