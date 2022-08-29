using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public enum AttackState
    {
        CanNotAttack,
        CanAttack,
        AttackNow
    }

    public float jumpPower = 10.0f;
    [SerializeField]
    private float minHeight = 5.0f;
    [SerializeField]
    private float maxHeight = 5.0f;


    [SerializeField]
    private string obstacleTagName = "Obstacle";
    [SerializeField]
    private string checkerTagName = "StartFlag";

    [SerializeField]
    private GameObject meshObj;
    [SerializeField]
    private GameObject fireParticleObj;
    [SerializeField]
    private float fireAttackCoolTimeMax = 5.0f;
    [SerializeField]
    private float attackTimeMax = 1.0f;

    public int scorePoint = 100;
    Rigidbody rb;
    ParticleSystem starParticleSys;
    ParticleSystem fireParticleSys;

    float angleMax = 20.0f;
    float rotateSpeed = 50.0f;
    float curAngle = 0.0f;
    float fireTimer = 0.0f;
    float attackTimer = 0.0f;
    public AttackState attackState = AttackState.CanNotAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        starParticleSys = meshObj.GetComponent<ParticleSystem>();
        starParticleSys.Pause();
        fireParticleSys = fireParticleObj.GetComponent<ParticleSystem>();
        fireParticleSys.Stop();
    }

    void Update()
    {
        if( GameManager_FlappyBird.Instance.IsGamePlaying() )
        {
            if ( rb.useGravity == false )
            {
                rb.useGravity = true;
                starParticleSys.Play();
            }

            // Case 1
            // Velocity는 동일한 속도가 되고 addforce는 힘을 더함
            if ( Input.GetButtonDown( "Jump" ) )
            {
                rb.velocity = new Vector3( 0, jumpPower, 0 );
            }

            switch (attackState)
            {
                case AttackState.CanNotAttack:
                {
                    if ( fireTimer < fireAttackCoolTimeMax )
                    {
                        fireTimer += Time.deltaTime;
                    }
                    else
                    {
                        attackState = AttackState.CanAttack;
                    }
                }
                break;

                case AttackState.CanAttack:
                {
                    if(Input.GetKey(KeyCode.F))
                    {
                        attackState = AttackState.AttackNow;
                    }
                }
                break;

                case AttackState.AttackNow:
                {
                    fireParticleSys.Play();
                    attackTimer += Time.deltaTime;
                    if(attackTimer >= attackTimeMax )
                    {
                        fireParticleSys.Stop();
                        attackState = AttackState.CanNotAttack;
                    }
                }
                break;
            }


        }


        //Case 2
        //if ( Input.GetButtonDown( "Jump" ) )
        //{
        //    rb.AddForce( new Vector3( 0, jumpPower * 100.0f, 0 ) );
        //}

        if( transform.position.y <= -100.0f)
        {
            Destroy( gameObject );
        }

    }

    private void FixedUpdate()
    {
        if( transform.position.y >= maxHeight )
        {
            GameManager_FlappyBird.Instance.EndGame();
        }
        else if ( transform.position.y <= minHeight )
        {
            GameManager_FlappyBird.Instance.EndGame();
        }
        RotateMesh();
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.gameObject.CompareTag( obstacleTagName ) )
        {
            GameManager_FlappyBird.Instance.EndGame();
        }
        if ( other.gameObject.CompareTag( checkerTagName ) )
        {
            GameManager_FlappyBird.Instance.AddScore( scorePoint );
        }

    }

    void RotateMesh()
    {
        if(rb.velocity.y > 0.0f)
        {
            curAngle -= rotateSpeed * 2.0f * Time.deltaTime;
        }
        else if ( rb.velocity.y < 0.0f )
        {
            curAngle += rotateSpeed * Time.deltaTime;
        }

        if(curAngle >= angleMax)
        {
            curAngle = angleMax;
        }
        else if ( curAngle <= -angleMax)
        {
            curAngle = -angleMax;
        }

        meshObj.transform.localEulerAngles = new Vector3( curAngle, 0.0f, 0.0f );
    }

}
