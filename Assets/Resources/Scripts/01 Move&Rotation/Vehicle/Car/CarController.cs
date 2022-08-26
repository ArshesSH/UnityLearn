using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public enum MoveMode
    {
        Stop,
        Forward,
        Backward
    }

    [SerializeField]
    private GameObject rearAligment;
    [SerializeField]
    private GameObject frontLeftWheelPos;
    [SerializeField]
    private GameObject frontRightWheelPos;

    [SerializeField]
    private GameObject frontLeftWheelCenter;
    [SerializeField]
    private GameObject frontRightWheelCenter;
    [SerializeField]
    private GameObject rearLeftWheelCenter;
    [SerializeField]
    private GameObject rearRightWheelCenter;

    [SerializeField]
    private float steeringSpeed = 50.0f;
    [SerializeField]
    private float powerSteeringSpeed = 50.0f;
    [SerializeField]
    private float maxSteeringAngle = 30.0f;
    [SerializeField]
    private float steeringDeadZone = 0.1f;

    [SerializeField]
    private float pedalPower = 0.1f;
    [SerializeField]
    private float maxPedal = 0.5f;


    [SerializeField]
    private float frictionPower = 0.05f;
    [SerializeField]
    private float brakePower = 0.1f;

    [SerializeField]
    private float thrustDeadzone = 0.1f;
    [SerializeField]
    private float maxThrust = 5.0f;
    [SerializeField]
    private float maxReverse = 3.0f;

    float steeringAngle = 0.0f;
    float pedal = 0.0f;
    float friction = 0.0f;
    float accel = 0.0f;
    float thrust = 0.0f;
    bool isBraking = false;
    bool isPedal = false;
    bool isTurnLeft = false;
    bool isTurnRight = false;
    float wheelBaseDistance;
    float wheelDistFromBaseCenter;

    MoveMode moveMode;

    // Start is called before the first frame update
    void Start()
    {
        wheelBaseDistance = Mathf.Abs( frontLeftWheelPos.transform.position.z - rearAligment.transform.position.z );
        wheelDistFromBaseCenter = Mathf.Abs( frontLeftWheelPos.transform.position.x - rearAligment.transform.position.x );
        moveMode = MoveMode.Stop;
    }

    // Update is called once per frame
    void Update()
    {
        if ( thrust > thrustDeadzone )
        {
            moveMode = MoveMode.Forward;
        }
        else if( thrust < -thrustDeadzone)
        {
            moveMode = MoveMode.Backward;
        }
        else
        {
            moveMode = MoveMode.Stop;
        }
    }

    private void FixedUpdate()
    {
        UserInput();
        CalcPhysics();
        Move();
    }


    void UserInput()
    {
        if(Input.GetKey(KeyCode.A))
        {
            isTurnLeft = true;
        }
        else
        {
            isTurnLeft = false;
        }
        if ( Input.GetKey( KeyCode.D ) )
        {
            isTurnRight = true;
        }
        else
        {
            isTurnRight = false;
        }

        switch (moveMode)
        {
            case MoveMode.Stop:
            {

            }
            break;

            case MoveMode.Forward:
            {

            }
            break;
        }

        if ( Input.GetKey( KeyCode.W ) )
        {
            isPedal = true;
        }
        else
        {
            isPedal = false;
        }
        if ( Input.GetKey( KeyCode.S ) )
        {
            isBraking = true;
        }
        else
        {
            isBraking = false;
        }
    }

    void CalcPhysics()
    {
        SetSteeringAngle();
        RotateFrontWheel(); 

        if (isPedal)
        {
            Accelerate();
        }
        else
        {
            pedal = 0.0f;
        }

        Decelerate();
        CalcAccel();
        CalcThrust();
        RotateTire();
    }


    void Move()
    {
        if( thrust > thrustDeadzone)
        {
            RotateByRearAlignment();
        }
        transform.Translate( Vector3.forward * thrust * Time.deltaTime );
    }

    void Accelerate()
    {
        if(Mathf.Approximately(pedal, 0.0f))
        {
            pedal = thrustDeadzone;
        }
        pedal += pedalPower * Time.deltaTime;
        if ( pedal >= maxPedal )
        {
            pedal = maxPedal;
        }
    }
    void Decelerate()
    {
        float curFricPower = (isBraking) ? frictionPower + brakePower : frictionPower;

        if (thrust > 0.0f)
        {
            friction = curFricPower;
        }
        if ( thrust >= -thrustDeadzone && thrust <= thrustDeadzone )
        {
            friction = 0.0f;
        }
    }

    void CalcAccel()
    {
        accel = pedal - friction;
    }

    void CalcThrust()
    {
        thrust += accel;

        if( thrust >= -thrustDeadzone && thrust <= thrustDeadzone )
        {
            thrust = 0.0f;
        }

        if ( thrust >= maxThrust)
        {
            thrust = maxThrust;
        }
        else if( thrust <= -maxReverse)
        {
            thrust = -maxReverse;
        }
    }

    void SetSteeringAngle()
    {
        if ( steeringAngle < steeringDeadZone && steeringAngle > -steeringDeadZone )
        {
            steeringAngle = 0.0f;
        }

        if ( isTurnLeft )
        {
            steeringAngle -= steeringSpeed * Time.deltaTime;
        }
        else if ( steeringAngle < 0.0f )
        {
            steeringAngle += powerSteeringSpeed * Time.deltaTime;
        }
        if ( isTurnRight )
        {
            steeringAngle += steeringSpeed * Time.deltaTime;
        }
        else if ( steeringAngle > 0.0f )
        {
            steeringAngle -= powerSteeringSpeed * Time.deltaTime;
        }

        if ( steeringAngle >= maxSteeringAngle )
        {
            steeringAngle = maxSteeringAngle;
        }
        else if ( steeringAngle <= -maxSteeringAngle )
        {
            steeringAngle = -maxSteeringAngle;
        }
    }

    void RotateFrontWheel()
    {
        frontLeftWheelPos.transform.localEulerAngles = transform.up * steeringAngle;
        frontRightWheelPos.transform.localEulerAngles = transform.up * steeringAngle;
    }

    void RotateByRearAlignment()
    {
        float rotCenterDist = 1 / Mathf.Tan( Mathf.Deg2Rad * steeringAngle ) * wheelBaseDistance + wheelDistFromBaseCenter;

        if( steeringAngle < 0.0f)
        {
            transform.RotateAround( rearAligment.transform.position - new Vector3(0, rotCenterDist,0), transform.up, steeringAngle * Time.deltaTime *2.0f );
        }
        if ( steeringAngle > 0.0f )
        {
            transform.RotateAround( rearAligment.transform.position + new Vector3( 0, rotCenterDist, 0 ), transform.up, steeringAngle * Time.deltaTime * 2.0f  );
        }
    }

    void RotateTire()
    {
        frontLeftWheelCenter.transform.Rotate( Vector3.right * thrust );
        frontRightWheelCenter.transform.Rotate( Vector3.right * thrust );
        rearLeftWheelCenter.transform.Rotate( Vector3.right * thrust );
        rearRightWheelCenter.transform.Rotate( Vector3.right * thrust );
    }

    private void OnGUI()
    {
        GUI.Box( new Rect( 0.0f, 30.0f, 150.0f, 30.0f ), "Thrust: " + thrust );
        GUI.Box( new Rect( 0.0f, 60.0f, 150.0f, 30.0f ), "Acc: " + accel );
        GUI.Box( new Rect( 0.0f, 90.0f, 150.0f, 30.0f ), "Pedal: " + pedal );
        GUI.Box( new Rect( 0.0f, 120.0f, 150.0f, 30.0f ), "Friction: " + friction );
        GUI.Box( new Rect( 0.0f, 150.0f, 150.0f, 30.0f ), "SteeringAngle: " + steeringAngle );
        GUI.Box( new Rect( 0.0f, 180.0f, 150.0f, 30.0f ), "MoveMode: " + moveMode );
    }
}
