using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPDEnemyController : SPDCharacterController
{
    public enum EnemyBehaviour
    {
        MoveToTarget,
        AttackTarget,
        SearchTarget,
        Victory
    }

    [Header("AI Settings")]
    private GameObject targetObjects;
    [SerializeField]
    private string targetName = "Tower";
    private string realTargetName;
    private Transform targetTransform;
    [SerializeField]
    private int targetCounts = 8;
    [SerializeField]
    private float targetDetectionRayDistance = 3.0f;
    [SerializeField]
    private GameObject targetDetectionRayPos;
    private Ray towerDetectionRay;
    RaycastHit[] rayHits;
    
    public bool isStartMove = false;

    void Start()
    {
        moveSpeed = 5;
        InitSettings();
        towerDetectionRay = new Ray(targetDetectionRayPos.transform.position, transform.forward);
        state = State.Sprint;
        targetObjects = GameObject.Find("Towers");
        FindTarget();
    }

    void Update()
    {
        if( isStartMove )
        {

            UpdateRay();
            SetState();
            PlayAnimations();
        }
    }
    

    protected override void SetState()
    {
        base.SetState();

        switch (state)
        {
            case State.Sprint:
            {
                RotateCharacter();
                if (IsRayCollisionWithTag(out rayHits, towerDetectionRay, "Tower", targetDetectionRayDistance))
                {
                    state = State.Idle;
                }
            }
            break;

            case State.Idle:
            {
                if(SPDGameManager.Instance.towerCount > 0)
                {
                    if (!IsRayCollisionWithTag(out rayHits, towerDetectionRay, "Tower", targetDetectionRayDistance))
                    {
                        FindTarget();
                        state = State.Sprint;
                    }
                    else
                    {
                        state = State.Attack;
                    }
                }
                else
                {
                    state = State.Victory;
                }

            }
            break;

            case State.Attack:
            {
                
            }
            break;
        }
    }

    protected override void SetDirection()
    {
        direction = (targetTransform.position - transform.position).normalized;
    }

    void FindTarget()
    {
        if(SPDGameManager.Instance.towerCount > 0)
        {
            do
            {
                int num = Random.Range(0, targetCounts);
                realTargetName = targetName + num.ToString();
                targetTransform = targetObjects.transform.Find(realTargetName);
                print(realTargetName);
            }
            while (targetTransform == null);

            if (targetTransform)
            {
                SetDirection();
            }
        }
    }

    void UpdateRay()
    {
        towerDetectionRay.origin = targetDetectionRayPos.transform.position;
        towerDetectionRay.direction = transform.forward;
    }

    bool IsRayCollisionWithTag(out RaycastHit[] raycastHits, Ray ray, string tag, float distance)
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


    private void OnDrawGizmos()
    {
        Debug.DrawRay(targetDetectionRayPos.transform.position, transform.forward * targetDetectionRayDistance, Color.green);
    }


}
