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
    private GameObject frontLeftWheelPos;
    [SerializeField]
    private GameObject frontRightWheelPos;
    [SerializeField]
    private GameObject rearAligment;
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
    private float steeringSpeed = 50.0f;
    [SerializeField]
    private float steeringAngleMax = 30.0f;
    [SerializeField]
    private float steeringDeadZone = 0.02f;


    private Vector3 vel;
    private Vector3 moveDir;
    private float physicsSpeed;
    private Vector3 moveVel;
    private float curAccel = 0.0f;
    private float curFriction = 0.0f;
    private Vector3 lastPos;
    private MoveMode curMoveMode = MoveMode.Forward;
    private bool isBraking;
    private float steeringAngle = 0.0f;

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
    }

    void ControlCar()
    {
        if( Input.GetKey( KeyCode.Space ) )
        {
            Braking( KeyCode.Space );
            Decelerate();
        }

        RotateControl();

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
        RotateByRearAlignment();

        if ( physicsSpeed <= maxSpeed )
        {
            moveVel += curAccel * Time.deltaTime * moveDir;
        }

        transform.position += moveVel;
    }

    void Accelerate()
    {
        //curFriction = 0.0f;

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

    void RotateControl()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (steeringAngle >= -steeringAngleMax)
            {
                steeringAngle -= steeringSpeed * Time.deltaTime;
            }
        }
        else if (steeringAngle < 0.0f)
        {
            steeringAngle += steeringSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if( steeringAngle <= steeringAngleMax)
            {
                steeringAngle += steeringSpeed * Time.deltaTime;
            }
        }
        else if (steeringAngle > 0.0f)
        {
            steeringAngle -= steeringSpeed * Time.deltaTime;
        }


        if(steeringAngle >= -steeringDeadZone && steeringAngle <= steeringDeadZone)
        {
            steeringAngle = 0.0f;
        }

    }

    void SteeringFrontWheel()
    {
        frontLeftWheelPos.transform.localEulerAngles = transform.up * steeringAngle;
        frontRightWheelPos.transform.localEulerAngles = transform.up * steeringAngle;
    }

    void RotateByRearAlignment()
    {
        transform.RotateAround( rearAligment.transform.position, transform.up, steeringAngle * Time.deltaTime * physicsSpeed);
    }

    private void OnGUI()
    {
        GUI.Box( new Rect( 0.0f, 0.0f, 150.0f, 30.0f ), "Vel = " + (physicsSpeed).ToString() );
        GUI.Box( new Rect( 0.0f, 30.0f, 150.0f, 30.0f ), "Acc = " + curAccel);
        GUI.Box( new Rect( 0.0f, 60.0f, 150.0f, 30.0f ), "Fric = " + curFriction );
        GUI.Box( new Rect( 0.0f, 90.0f, 150.0f, 30.0f ), "MoveMode: " + curMoveMode );
        GUI.Box( new Rect( 0.0f, 120.0f, 150.0f, 30.0f ), "Braking: " + isBraking );
        GUI.Box( new Rect( 0.0f, 150.0f, 150.0f, 30.0f ), "SteeringAngle: " + steeringAngle );
    }

    void RotateWheel(GameObject target, float targetVel)
    {
        target.transform.Rotate(target.transform.right * targetVel * Time.deltaTime);
        target.transform.rotation.Set(GetRangedAngle(target.transform.eulerAngles.x), 0.0f, 0.0f, target.transform.rotation.w);
    }
    
    float GetRangedAngle(float angle)
    {
        float rangedAngle = angle % 360.0f;
        if (rangedAngle < 0.0f)
        {
            rangedAngle += 360.0f;
        }
        return rangedAngle;
    }

    private void FixedUpdate()
    {
        ControlCar();
        ApplyFriction();
        SteeringFrontWheel();
        Move();
    }
}

