using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public enum GearState
    {
        Neutral,
        Front,
        Reverse,
    }

    [SerializeField]
    protected GameObject rearAligment;
    [SerializeField]
    protected GameObject frontLeftWheelPos;
    [SerializeField]
    protected GameObject frontRightWheelPos;

    [SerializeField]
    protected GameObject frontLeftWheelCenter;
    [SerializeField]
    protected GameObject frontRightWheelCenter;
    [SerializeField]
    protected GameObject rearLeftWheelCenter;
    [SerializeField]
    protected GameObject rearRightWheelCenter;

    [SerializeField]
    protected float steeringSpeed = 50.0f;
    [SerializeField]
    protected float powerSteeringSpeed = 50.0f;
    [SerializeField]
    protected float maxSteeringAngle = 30.0f;
    [SerializeField]
    protected float steeringDeadZone = 0.1f;

    [SerializeField]
    protected float pedalPower = 0.1f;
    [SerializeField]
    protected float maxPedal = 0.5f;


    [SerializeField]
    protected float frictionPower = 0.05f;
    [SerializeField]
    protected float brakePower = 0.1f;

    [SerializeField]
    protected float thrustDeadzone = 0.1f;
    [SerializeField]
    protected float maxThrust = 5.0f;
    [SerializeField]
    protected float maxReverse = 3.0f;

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

    GearState curGear;
    Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        wheelBaseDistance = Mathf.Abs( frontLeftWheelPos.transform.position.z - rearAligment.transform.position.z );
        wheelDistFromBaseCenter = Mathf.Abs( frontLeftWheelPos.transform.position.x - rearAligment.transform.position.x );
        curGear = GearState.Neutral;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        SwitchGear();
        UserInput();
        CalcPhysics();
        Move();
    }

    protected virtual void SwitchGear()
    {
        if( thrust < thrustDeadzone && thrust > -thrustDeadzone)
        {
            curGear = GearState.Neutral;
        }

        if( curGear == GearState.Neutral)
        {
            if(Input.GetKey(KeyCode.W))
            {
                curGear = GearState.Front;
                
                SetDirToFoward();
            }
            else if(Input.GetKey(KeyCode.S))
            {
                curGear = GearState.Reverse;
                SetDirToBackward();
            }
        }
    }

    protected void SetDirToFoward()
    {
        moveDir = Vector3.forward;
    }
    protected void SetDirToBackward()
    {
        moveDir = Vector3.back;
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

        switch (curGear)
        {
            case GearState.Front:
            {
                if (Input.GetKey(KeyCode.W))
                {
                    isPedal = true;
                }
                else
                {
                    isPedal = false;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    isBraking = true;
                }
                else
                {
                    isBraking = false;
                }
            }
            break;

            case GearState.Reverse:
            {
                if (Input.GetKey(KeyCode.S))
                {
                    isPedal = true;
                }
                else
                {
                    isPedal = false;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    isBraking = true;
                }
                else
                {
                    isBraking = false;
                }
            }
            break;
        }


    }

    protected void CalcPhysics()
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


    protected void Move()
    {
        if( thrust > thrustDeadzone)
        {
            RotateByRearAlignment();
        }
        transform.Translate( moveDir * thrust * Time.deltaTime );
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
        float changedSteeringAngle = (curGear == GearState.Reverse) ? -steeringAngle : steeringAngle;
        float rotCenterDist = 1 / Mathf.Tan( Mathf.Deg2Rad * changedSteeringAngle) * wheelBaseDistance + wheelDistFromBaseCenter;
        
        if( steeringAngle < 0.0f)
        {
            transform.RotateAround( rearAligment.transform.position + (Vector3.up * rotCenterDist), transform.up, changedSteeringAngle * Time.deltaTime *2.0f );
        }
        if ( steeringAngle > 0.0f )
        {
            transform.RotateAround( rearAligment.transform.position + (Vector3.up * rotCenterDist), transform.up, changedSteeringAngle * Time.deltaTime * 2.0f  );
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
        GUI.Box( new Rect( 0.0f, 180.0f, 150.0f, 30.0f ), "Gear: " + curGear );
    }
}
