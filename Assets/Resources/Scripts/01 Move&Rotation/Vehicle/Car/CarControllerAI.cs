using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerAI : CarController
{
    [Header("AI")]
    [SerializeField]
    private GameObject frontRayPos;
    [SerializeField]
    private GameObject rightRayPos;
    [SerializeField]
    private GameObject leftRayPos;
    [SerializeField]
    public float frontRayDistance = 50.0f;
    [SerializeField]
    public float sideRayDistance = 200.0f;
    [Range(0, 90)]
    public float sideRayAngle = 60.0f;
    [SerializeField]
    private float correctionFactor = 0.1f;
    [SerializeField]
    private float turnSpeed = 5.0f;

    public bool isStartCar = false;

    private Ray frontRay;
    private RaycastHit frontRayHit;
    private RaycastHit[] frontRayHits;

    private Ray rightRay;
    private RaycastHit rightRayHit;
    private RaycastHit[] rightRayHits;

    private Ray leftRay;
    private RaycastHit leftRayHit;
    private RaycastHit[] leftRayHits;

    float rightDist;
    float leftDist;

    // Start is called before the first frame update
    void Start()
    {
        isStartCar = true;
        isPedal = true;
        curGear = GearState.Front;
        frontRay = new Ray(frontRayPos.transform.position, transform.forward);
        rightRay = new Ray(rightRayPos.transform.position, Quaternion.Euler(0.0f, sideRayAngle, 0.0f) * transform.forward);
        leftRay = new Ray(leftRayPos.transform.position, Quaternion.Euler(0.0f, -sideRayAngle, 0.0f) * transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        InputAI();
    }

    private void FixedUpdate()
    {
       
        UpdateRay();
        CalcPhysics();
        Move();
    }

    void UpdateRay()
    {
        frontRay.origin = frontRayPos.transform.position;
        frontRay.direction = transform.forward;
        rightRay.origin = rightRayPos.transform.position;
        rightRay.direction = Quaternion.Euler(0.0f, sideRayAngle, 0.0f) * transform.forward;
        leftRay.origin = rightRayPos.transform.position;
        leftRay.direction = Quaternion.Euler(0.0f, -sideRayAngle, 0.0f) * transform.forward;

        //RayFindByTag(out frontRayHits, frontRay, "Obstacle", frontRayDistance);
        //GetDistanceFrom(out rightRayHits, rightRay, "Obstacle", sideRayDistance);
        //GetDistanceFrom(out leftRayHits, leftRay, "Obstacle", sideRayDistance);
    }

    void InputAI()
    {
        SteeringAI();
        ThrottleAI();
    }

    void SteeringAI()
    {
        rightDist = GetDistanceFrom(out rightRayHits, rightRay, "Obstacle", sideRayDistance);
        leftDist = GetDistanceFrom(out leftRayHits, leftRay, "Obstacle", sideRayDistance);

        float dist = rightDist - leftDist;

        if (dist > correctionFactor)
        {
            isTurnLeft = false;
            isTurnRight = true;
        }
        else if (dist < correctionFactor)
        {
            isTurnRight = false;
            isTurnLeft = true;
        }
        else
        {
            isTurnRight = false;
            isTurnLeft = false;
        }
    }

    void ThrottleAI()
    {
        if(IsRayCollisionWithTag(out frontRayHits, frontRay, "Obstacle", frontRayDistance) && thrust >= turnSpeed)
        {
            isPedal = false;
            isBraking = true;
        }
        else
        {
            isBraking = false;
            isPedal = true;
        }
    }

    protected override void SwitchGear()
    {
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
    
    bool IsRayCollisionWithTag( out RaycastHit[] raycastHits, Ray ray, string tag, float distance)
    {
        raycastHits = Physics.RaycastAll(ray, distance);
        foreach (var rayHit in raycastHits)
        {
            if (rayHit.collider.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    float GetDistanceFrom(out RaycastHit[] raycastHits, Ray ray, string tag, float distance)
    {
        raycastHits = Physics.RaycastAll(ray, distance);
        foreach (var rayHit in raycastHits)
        {
            if (rayHit.collider.CompareTag(tag))
            {
                return rayHit.distance;
            }
        }
        return sideRayDistance;
    }


    private void OnDrawGizmos()
    {
        DrawGizmoRay(frontRayHits, frontRayPos.transform, frontRay.direction, frontRayDistance);
        DrawGizmoRay(rightRayHits, rightRayPos.transform, rightRay.direction, sideRayDistance);
        DrawGizmoRay(leftRayHits, leftRayPos.transform, leftRay.direction, sideRayDistance);
    }
    void DrawGizmoRay(RaycastHit[] raycastHits, Transform originPos, Vector3 dir, float distance)
    {
        if (raycastHits != null)
        {
            foreach (var rayHit in raycastHits)
            {
                if (rayHit.collider != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(rayHit.point, 0.1f);
                    // Draw to point
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(originPos.position, originPos.position + dir * distance);

                    // Draw Normal
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(rayHit.point, rayHit.point + rayHit.normal);

                    // Draw Reflect
                    Gizmos.color = Color.magenta;
                    Vector3 reflect = Vector3.Reflect(dir, rayHit.normal);
                    Gizmos.DrawLine(rayHit.point, rayHit.point + reflect);
                }
            }
        }
        else
        {
            Debug.DrawRay(originPos.position, dir * distance, Color.green);
        }
    }

    protected override void OnGUI()
    {
        GUI.Box(new Rect(150.0f, 30.0f, 150.0f, 30.0f), "Thrust: " + thrust);
        GUI.Box(new Rect(150.0f, 60.0f, 150.0f, 30.0f), "Acc: " + accel);
        GUI.Box(new Rect(150.0f, 90.0f, 150.0f, 30.0f), "Pedal: " + pedal);
        GUI.Box(new Rect(150.0f, 120.0f, 150.0f, 30.0f), "Friction: " + friction);
        GUI.Box(new Rect(150.0f, 150.0f, 150.0f, 30.0f), "SteeringAngle: " + steeringAngle);
        GUI.Box(new Rect(150.0f, 180.0f, 150.0f, 30.0f), "Gear: " + curGear);
        GUI.Box(new Rect(150.0f, 210.0f, 150.0f, 30.0f), "LDist: " + leftDist);
        GUI.Box(new Rect(150.0f, 240.0f, 150.0f, 30.0f), "RDist: " + rightDist);
    }
}
