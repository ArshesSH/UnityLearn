using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXController : MonoBehaviour
{
    public enum PlayerState
    {
        Intro,
        Idle,
        Airbone,
   
    }
    
    [Header( "Player Settings" )]
    [SerializeField]
    private float maxHP = 15.0f;
    private float curHP;
    [SerializeField]
    private float moveSpeed = 200.0f;
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

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    RaycastHit2D groundHit;

    public PlayerState curState;

    void Start()
    {
        curState = PlayerState.Intro;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        curState = CheckGround() ? PlayerState.Idle : PlayerState.Airbone;
    }

    private void FixedUpdate()
    {
        MovePlayerX();
        JumpPlayerX();
        ApplyGravity();
        print( rb.velocity.y );
    }

    void ApplyGravity()
    {
        if(curState == PlayerState.Airbone)
        {
            float gravitySpeed = Mathf.Min( rb.velocity.y - gravity, maxGravitySpeed );
            rb.velocity = new Vector2( rb.velocity.x, gravitySpeed );
        }
    }

    void MovePlayerX()
    {
        float x = Input.GetAxis( "Horizontal" );
        if( x < 0.0f)
        {
            spriteRenderer.flipX = false;
        }
        else if( x > 0.0f)
        {
            spriteRenderer.flipX = true;
        }

        rb.velocity = new Vector2(moveSpeed * x, rb.velocity.y);
    }

    void JumpPlayerX()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            print( "jump" );
            rb.AddForce( jumpSpeed * Vector2.up, ForceMode2D.Impulse );
        }
    }

    bool CheckGround()
    {
        if( groundRayStartPos != null && groundRayEndPos != null)
        {
            groundHit = Physics2D.Linecast( groundRayStartPos.transform.position, groundRayEndPos.transform.position );
            if ( groundHit.collider.gameObject.layer == LayerMask.NameToLayer( groundLayerName ) )
            {
                return true;
            }
            return false;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        
    }

}
