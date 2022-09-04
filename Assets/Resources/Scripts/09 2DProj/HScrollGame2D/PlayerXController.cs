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
    }
    
    [Header( "Player Settings" )]
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
    private float gravity = 9.8f;
    [SerializeField]
    private float maxGravitySpeed = 400.0f;

    [Header( "Ground Check" )]
    [SerializeField]
    private GameObject groundRayStartPos;
    [SerializeField]
    private GameObject groundRayEndPos;
    [SerializeField]
    private string groundLayerName;

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    RaycastHit2D[] groundHits;

    float dashTimer = 0.0f;

    public PlayerState curState;
    bool isOnGround = false;
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
    }

    void Update()
    {
        
        if (!IsCurState("X_Intro"))
        {
            isOnGround = CheckGround();
            if(!isOnGround )
            {
                curState = PlayerState.Airbone;
            }

            PlayerInput();
            animator.SetBool("IsOnGround", isOnGround);
            animator.SetBool("IsDashNow", curState == PlayerState.Dash);
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
        if(Input.GetKeyDown(KeyCode.X))
        {
            JumpPlayerX();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Shoot");
        }

        if(Input.GetButton("Dash"))
        {
            isDashKeyDown = true;
            dashTimer += Time.deltaTime;
            if( dashTimer <= dashMaxTime)
            {
                curState = PlayerState.Dash;
            }
            else
            {
                curState = PlayerState.Idle;
            }
        }
        if(Input.GetButtonUp("Dash"))
        {
            isDashKeyDown = false;
            curState = PlayerState.Idle;
            dashTimer = 0.0f;
        }
    }

    public void ApplyGravity()
    {
        if(!CheckGround())
        {
            float gravitySpeed = Mathf.Min(rb.velocity.y - gravity, maxGravitySpeed);
            rb.velocity = new Vector2(rb.velocity.x, gravitySpeed);
        }
    }

    void MovePlayerX()
    {
        if( curState == PlayerState.Dash )
        {
            rb.velocity = new Vector2( spriteRenderer.flipX ? dashSpeed : -dashSpeed , rb.velocity.y);
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

            rb.velocity = new Vector2(moveSpeed * x, rb.velocity.y);
        }
    }
    



    void JumpPlayerX()
    {
        if(CheckGround())
        {
            rb.AddForce(jumpSpeed * Vector2.up, ForceMode2D.Impulse);
        }
    }

    bool CheckGround()
    {
        if( groundRayStartPos != null && groundRayEndPos != null)
        {
            groundHits = Physics2D.LinecastAll(groundRayStartPos.transform.position, groundRayEndPos.transform.position);
            foreach( var hit in groundHits )
            {
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    


    private void OnDrawGizmos()
    {
        
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(0.0f, 0.0f, 150.0f, 30.0f), "OnGround: " + isOnGround.ToString());
        GUI.Box(new Rect(0.0f, 30.0f, 150.0f, 30.0f), "State: " + curState);
        GUI.Box(new Rect(0.0f, 60.0f, 150.0f, 30.0f), "dashTime: " + dashTimer.ToString());
    }


}
