using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigmaBehaviour : MonoBehaviour
{
    public enum Behaviours
    {
        Decide,
        CreateElectricAttack,
        MoveToPlayer,
        OpenMouth,
        Damaged,
        FadeOut,
    }

    [Header("Status")]
    public float maxHP = 30.0f;
    public float curHP = 30.0f;
    public float moveSpeed = 100.0f;
    public float trackingDistance = 500.0f;
    public float decideTime = 3.0f;
    public float moveWaitTime = 5.0f;
    public float mouthOpenTime = 2.0f;
    public float fadeWaitTime = 1.0f;

    [SerializeField]
    private Vector3 trackingOffset;

    public GameObject sigmaSpawnPos;
    public GameObject electricBallSpawnPos1;
    public GameObject electricBallSpawnPos2;
    public GameObject electricBall;

    public bool canDamaged = false;

    Rigidbody2D rb;
    Animator animator;
    Behaviours bv = Behaviours.Decide;
    float time = 0.0f;

    GameObject playerX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        curHP = maxHP;
        playerX = RockManGameManager.Instance.PlayerX;
    }

    void Update()
    {
        switch (bv)
        {
            case Behaviours.Decide:
            {
                Decide();
            }
            break;
            case Behaviours.CreateElectricAttack:
            {
                Attack();
            }
            break;
            case Behaviours.OpenMouth:
            {
                OpenMouth();
            }
            break;
            case Behaviours.FadeOut:
            {
                FadeOut();
            }
            break;
        }
    }

    private void FixedUpdate()
    {
        switch(bv)
        {
            case Behaviours.MoveToPlayer:
            {
                MoveToPlayer();
            }
            break;
        }
    }


    void Decide()
    {
        time += Time.deltaTime;

        if( time >= decideTime)
        {
            time = 0.0f;
            animator.SetTrigger("Ready");
            bv = Behaviours.CreateElectricAttack;
        }
    }

    void Attack()
    {
        Instantiate(electricBall, electricBallSpawnPos1.transform.position, electricBallSpawnPos1.transform.rotation);
        Instantiate(electricBall, electricBallSpawnPos2.transform.position, electricBallSpawnPos2.transform.rotation);

        bv = Behaviours.MoveToPlayer;
    }

    void MoveToPlayer()
    {
        time += Time.deltaTime;
        if(time >= moveWaitTime)
        {
            if (Vector2.Distance(playerX.transform.position, transform.position) >= trackingDistance)
            {
                Vector3 direction = ((playerX.transform.position + trackingOffset) - transform.position).normalized;
                rb.MovePosition(transform.position + moveSpeed * Time.deltaTime * direction);
            }
            else
            {
                time = 0.0f;
                animator.SetTrigger("Open");
                bv = Behaviours.OpenMouth;
            }
        }
    }

    void OpenMouth()
    {
        time += Time.deltaTime;
        if(time >= 0.5f)
        {
            canDamaged = true;
        }
        if(time>= mouthOpenTime)
        {
            time = 0.0f;
            bv = Behaviours.FadeOut;
            animator.SetTrigger("FadeOut");
        }
    }

    void FadeOut()
    {
        time += Time.deltaTime;
        if (time >= fadeWaitTime)
        {
            transform.position = sigmaSpawnPos.transform.position;
            if (time >= fadeWaitTime * 2)
            {
                time = 0.0f;
                bv = Behaviours.Decide;
                animator.SetTrigger("Idle");
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            if( canDamaged )
            {
                curHP -= 1.0f;
                canDamaged = false;
                animator.SetTrigger("Damaged");
            }
        }
    }


    private void OnGUI()
    {
        GUI.Box(new Rect(500.0f, 0.0f, 150.0f, 30.0f), "Behaviour: " + bv);
        GUI.Box(new Rect(500.0f, 30.0f, 150.0f, 30.0f), "CanDamaged: " + canDamaged);
    }
}
