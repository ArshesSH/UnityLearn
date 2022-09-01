using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPDEnemyController : SPDCharacterController
{
    [Header("AI Settings")]
    [SerializeField]
    private GameObject targetObjects;
    [SerializeField]
    private string targetName = "Tower";
    private Transform targetTrnasform;
    [SerializeField]
    private int targetCounts = 8;

    void Start()
    {
        InitSettings();
        FindTarget();
        print(targetTrnasform);
    }

    void Update()
    {
        
    }

    protected override void SetState()
    {
        base.SetState();

    }

    protected override void SetDirection()
    {

    }

    void FindTarget()
    {
        int num = Random.Range(0, targetCounts);
        targetTrnasform = targetObjects.transform.Find(targetName + num.ToString());
    }
}
