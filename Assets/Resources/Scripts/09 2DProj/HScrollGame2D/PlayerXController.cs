using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXController : MonoBehaviour
{
    public enum PlayerState
    {
        Intro,
        Idle,
        Walk,
        Airbone,
        Dash,
        WallCling,
    }

    [Header("Player Settings")]
    [SerializeField]
    private float maxHP = 15.0f;
    public float curHP;
    [SerializeField]
    private float moveSpeed = 200.0f;
    [SerializeField]
    private float dashSpeed = 400.0f;
    [SerializeField]
    private float dashMaxTime = 3.0f;
    [SerializeField]
    private float jumpSpeed = 100.0f;
    [SerializeField]
    private float wallClingSpeed = 100.0f;
    [SerializeField]
    private float gravity = 9.8f;
    [SerializeField]
    private float maxGravitySpeed = 400.0f;

    [SerializeField]
    private GameObject busterPosLeft;
    [SerializeField]
    private GameObject busterPosRight;
    [SerializeField]
    private GameObject busterObj;
    [SerializeField]
    private float busterDuration = 0.3f;

    [Header("Ground Check")]
    [SerializeField]
    private GameObject groundRayStartPos;
    [SerializeField]
    private GameObject groundRayEndPos;
    [SerializeField]
    private string groundLayerName;
    bool isOnGround = false;

    [Header("Wall Check")]
    [SerializeField]
    private GameObject wallCheckLeft;
    [SerializeField]
    private GameObject wallCheckRight;
    [SerializeField]
    private float wallCheckDistance = 3.0f;
    Transform wallRayPos;
    Vector2 wallRayDir;
    RaycastHit2D[] wallHits;
    bool isOnWallSide = false;


    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    RaycastHit2D[] groundHits;

    float dashTimer = 0.0f;

    public PlayerState curState;
    bool isDashKeyDown = false;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        curHP = maxHP;
        curState = PlayerState.Intro;
        wallRayPos = wallCheckRight.transform;
    }

    void Update()
    {
        if (!IsCurState("X_Intro"))
        {

            PlayerInput();
            isOnGround = CheckGround();
            UpdateWallCheck();
            isOnWallSide = CheckWall();

            if (!isOnGround)
            {
                if (isOnWallSide && rb.velocity.y <= 0.0f)
                {
                    curState = PlayerState.WallCling;
                }
                else
                {
                    curState = PlayerState.Airbone;
                }
            }




            animator.SetBool("IsOnGround", isOnGround);
            animator.SetBool("IsDashNow", curState == PlayerState.Dash);
            animator.SetBool("IsWallCling", !isOnGround && isOnWallSide);
        }

    }

    private void FixedUpdate()
    {
        ApplyGravity();
        if (!IsCurState("X_Intro"))
        {
            MovePlayerX();
        }
    }

    bool IsCurState(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            JumpPlayerX();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Attack();
        }

        if (Input.GetButton("Dash"))
        {
            isDashKeyDown = true;
            dashTimer += Time.deltaTime;
            if (dashTimer <= dashMaxTime)
            {
                curState = PlayerState.Dash;
            }
            else
            {
                curState = PlayerState.Idle;
            }
        }
        if (Input.GetButtonUp("Dash"))
        {
            isDashKeyDown = false;
            curState = PlayerState.Idle;
            dashTimer = 0.0f;
        }
    }

    public void ApplyGravity()
    {
        if (curState == PlayerState.Airbone || curState == PlayerState.Intro)
        {
            float gravitySpeed = Mathf.Min(rb.velocity.y - gravity, maxGravitySpeed);
            rb.velocity = new Vector2(rb.velocity.x, gravitySpeed);
        }
        else if(curState == PlayerState.WallCling)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallClingSpeed);
        }
    }

    void MovePlayerX()
    {
        if (curState == PlayerState.Dash)
        {
            rb.velocity = new Vector2(spriteRenderer.flipX ? dashSpeed : -dashSpeed, rb.velocity.y);
        }
        else
        {
            float x = Input.GetAxis("Horizontal");
            animator.SetFloat("Horizontal", Mathf.Abs(x));
            if (x < 0.0f)
            {
                spriteRenderer.flipX = false;
                curState = PlayerState.Walk;
            }
            else if (x > 0.0f)
            {
                spriteRenderer.flipX = true;
                curState = PlayerState.Walk;
            }
            else
            {
                curState = PlayerState.Idle;
            }

            if(curState == PlayerState.WallCling)
            {
                if(IsFacingRight())
                {
                    x = Mathf.Min(0.0f, x);
                }
                else
                {
                    x = Mathf.Max(0.0f, x);
                }
            }

            rb.velocity = new Vector2(moveSpeed * x, rb.velocity.y);
        }
    }

    void JumpPlayerX()
    {
        if (CheckGround())
        {
            rb.AddForce(jumpSpeed * Vector2.up, ForceMode2D.Impulse);
        }
    }

    bool CheckGround()
    {
        if (groundRayStartPos != null && groundRayEndPos != null)
        {
            groundHits = Physics2D.LinecastAll(groundRayStartPos.transform.position, groundRayEndPos.transform.position);
            foreach (var hit in groundHits)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    IEnumerator SpawnBuster()
    {
        GameObject buster = null;

        if(busterPosRight != null && busterPosLeft != null)
        {
            buster = Instantiate(busterObj, spriteRenderer.flipX ? busterPosRight.transform.position : busterPosLeft.transform.position,
                busterPosLeft.transform.rotation);
        }
        yield return new WaitForSeconds(busterDuration);
        yield return new WaitForSeconds(1.0f);
    }

    void Attack()
    {
        StartCoroutine("SpawnBuster");
        animator.SetTrigger("Shoot");
    }

    void UpdateWallCheck()
    {
        if (IsFacingRight())
        {
            wallRayPos = wallCheckRight.transform;
            wallRayDir = Vector2.right;
        }
        else
        {
            wallRayPos = wallCheckLeft.transform;
            wallRayDir = Vector2.left;
        }
    }

    bool CheckWall()
    {
        wallHits = Physics2D.RaycastAll(wallRayPos.position, wallRayDir, wallCheckDistance);
        foreach (var hit in wallHits )
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                return true;
            }
        }
        return false;
    }


    public bool IsFacingRight()
    {
        return spriteRenderer.flipX;
    }
    private void OnDrawGizmos()
    {
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(0.0f, 0.0f, 150.0f, 30.0f), "OnGround: " + isOnGround.ToString());
        GUI.Box(new Rect(0.0f, 30.0f, 150.0f, 30.0f), "State: " + curState);
        GUI.Box(new Rect(0.0f, 60.0f, 150.0f, 30.0f), "dashTime: " + dashTimer.ToString());
        GUI.Box(new Rect(0.0f, 90.0f, 150.0f, 30.0f), "IsWallSide: " + isOnWallSide.ToString());
        GUI.Box(new Rect(0.0f, 120.0f, 150.0f, 30.0f), "Vel X: " + rb.velocity.x);
        GUI.Box(new Rect(150.0f, 120.0f, 150.0f, 30.0f), "Vel Y: " + rb.velocity.y);
    }


}
