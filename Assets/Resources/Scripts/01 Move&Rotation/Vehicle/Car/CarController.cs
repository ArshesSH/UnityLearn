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

    [Header("Wheel Setting Objects")]
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

    [Header("Steering Settings")]
    [SerializeField]
    protected float steeringSpeed = 50.0f;
    [SerializeField]
    protected float reverseSteeringSpeed = 50.0f;
    [SerializeField]
    protected float maxSteeringAngle = 30.0f;
    [SerializeField]
    protected float steeringDeadZone = 0.1f;

    [Header("Pedals Settings")]
    [SerializeField]
    protected float pedalPower = 0.1f;
    [SerializeField]
    protected float maxPedal = 0.5f;
    [SerializeField]
    protected float brakePower = 0.3f;

    [Header("Engine Settings")]
    [SerializeField]
    protected float maxThrust = 20.0f;
    [SerializeField]
    protected float maxReverse = 3.0f;
    [SerializeField]
    protected float thrustDeadzone = 0.5f;

    [Header("Physics Settings")]
    [SerializeField]
    protected float frictionPower = 0.05f;
    [SerializeField]
    protected float collisionFrictionPower = 0.5f;
    [Range(0.0f, 1.0f)]
    public float collisionLimit = 0.5f;



    float steeringAngle = 0.0f;
    float pedal = 0.0f;
    float friction = 0.0f;
    float accel = 0.0f;
    float thrust = 0.0f;
    bool isBraking = false;
    bool isPedal = false;
    bool isTurnLeft = false;
    bool isTurnRight = false;
    bool isCollision = false;
    float wheelBaseDistance;
    float wheelDistFromBaseCenter;

    GearState curGear;
    Vector3 moveDir = Vector3.forward;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        //if (thrust > thrustDeadzone)
        //{
        //    curGear = GearState.Front;
        //}
        //else if (thrust < -thrustDeadzone)
        //{
        //    curGear = GearState.Reverse;
        //}
        //else
        //{
        //    curGear = GearState.Neutral;
        //}

        if (thrust <= thrustDeadzone && thrust >= -thrustDeadzone)
        {
            curGear = GearState.Neutral;
        }

        switch (curGear)
        {
            case GearState.Neutral:
            {
                if (Input.GetKey(KeyCode.W))
                {
                    SetGearStateFront();
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    SetGearStateReverse();
                }
            }
            break;
            case GearState.Front:
            {
            }
            break;

            case GearState.Reverse:
            {
            }
            break;
        }
    }

    protected void SetGearStateFront()
    {
        curGear = GearState.Front;
        moveDir = Vector3.forward;
    }
    protected void SetGearStateReverse()
    {
        curGear = GearState.Reverse;
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
        //if( isCollision)
        //{
        //    curFricPower = collisionFrictionPower;
        //}

        if (thrust > thrustDeadzone)
        {
            friction = curFricPower;
        }
        else if( thrust < -thrustDeadzone)
        {
            friction = -curFricPower;
        }
        if (thrust >= -thrustDeadzone && thrust <= thrustDeadzone)
        {
            curFricPower = 0.0f;
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

        if (thrust >= -thrustDeadzone && thrust <= thrustDeadzone)
        {
            thrust = 0.0f;
        }
        if( thrust >= maxThrust)
        {
            thrust = maxThrust;
        }
        else if (thrust <= -maxReverse)
        {
            thrust = -maxReverse;
        }

        //float curMaxThrust = maxThrust;

        //if( curGear == GearState.Reverse )
        //{
        //    curMaxThrust = maxReverse;
        //}
        //curMaxThrust = (isCollision) ? curMaxThrust * collisionLimit : curMaxThrust;

        //if ( thrust >= curMaxThrust)
        //{
        //    thrust = curMaxThrust;
        //}
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
            steeringAngle += reverseSteeringSpeed * Time.deltaTime;
            if( steeringAngle >= 0.0f)
            {
                steeringAngle = 0.0f;
            }
        }
        if ( isTurnRight )
        {
            steeringAngle += steeringSpeed * Time.deltaTime;
        }
        else if ( steeringAngle > 0.0f )
        {
            steeringAngle -= reverseSteeringSpeed * Time.deltaTime;
            if (steeringAngle <= 0.0f)
            {
                steeringAngle = 0.0f;
            }
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
            var rotateAxis = rearAligment.transform.position + (Vector3.up * rotCenterDist);
            float rotateSpeed = changedSteeringAngle * Time.deltaTime * 2.0f;

            transform.RotateAround(rotateAxis, transform.up, rotateSpeed);
        }
        if ( steeringAngle > 0.0f )
        {
            var rotateAxis = rearAligment.transform.position + (Vector3.up * rotCenterDist);
            float rotateSpeed = changedSteeringAngle * Time.deltaTime * 2.0f;

            transform.RotateAround(rotateAxis, transform.up, rotateSpeed);
        }
    }

    void RotateTire()
    {
        frontLeftWheelCenter.transform.Rotate( Vector3.right * thrust );
        frontRightWheelCenter.transform.Rotate( Vector3.right * thrust );
        rearLeftWheelCenter.transform.Rotate( Vector3.right * thrust );
        rearRightWheelCenter.transform.Rotate( Vector3.right * thrust );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            transform.Translate( thrust * Time.deltaTime * -moveDir);
            thrust = 0.0f;
            isCollision = true;
            print("colison!");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isCollision = false;
            print("colison out!");
        }
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
