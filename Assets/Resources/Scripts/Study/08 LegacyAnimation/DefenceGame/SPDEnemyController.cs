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
    [SerializeField]
    private int point = 1;
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
    bool isIncreasedScore = false;
    public bool isStartWithSprint = true;

    void Start()
    {
        moveSpeed = 5;
        InitSettings();
        towerDetectionRay = new Ray(targetDetectionRayPos.transform.position, characterObj.transform.forward);
        state = isStartWithSprint ? State.Sprint : State.Idle;
        targetObjects = GameObject.Find("Towers");
        FindTarget();
    }

    void Update()
    {
        UpdateRay();
        if ( isStartMove )
        {
            SetState();
        }
        PlayAnimations();
    }

    protected override void SetState()
    {
        base.SetState();
        if (SPDGameManager.Instance.towerCount > 0)
        {
            switch (state)
            {
                case State.Sprint:
                {
                    RotateCharacter();
                    if (targetObjects.transform.Find(realTargetName) == null)
                    {
                        FindTarget();
                    }

                    if (IsRayCollisionWithTag(out rayHits, towerDetectionRay, "Tower", targetDetectionRayDistance))
                    {
                        state = State.Idle;
                    }

                }
                break;

                case State.Idle:
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
                break;

                case State.Die:
                {
                    if (!isIncreasedScore)
                    {
                        SPDGameManager.Instance.score += 1;
                        SPDGameManager.Instance.IncreaseDifficulty();
                        isIncreasedScore = true;
                    }
                }
                break;
            }
        }
        else
        {
            state = State.Victory;
        }
    }

    protected override void SetDirection()
    {
        direction = (targetTransform.position - characterObj.transform.position).normalized;
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
        towerDetectionRay.direction = characterObj.transform.forward;
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
