using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SPDCharacterController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walk,
        Run,
        Sprint,
        Attack,
        Die,
        Victory
    }

    [Header( "Weapon Settings" )]
    [SerializeField]
    protected GameObject weaponObject;
    protected Collider weaponCollider;

    [Header( "Character Settings" )]

    [SerializeField]
    protected int maxHP = 3;
    protected int curHP;
    [SerializeField]
    protected float walkSpeed = 4.0f;
    [SerializeField]
    protected float runSpeed = 8.0f;
    [SerializeField]
    protected float sprintSpeed = 12.0f;
    [SerializeField]
    protected float rotateSpeed = 360.0f;
    [SerializeField]
    protected float attackDelay = 1.0f;
    protected float attackHitDelay = 0.3f;

    protected CharacterController controller;
    protected Animation spartaAnim;
    protected State state;
    protected Vector3 direction;
    protected float moveSpeed;

    void Start()
    {
        InitSettings();
    }
    void Update()
    {
        
    }

    protected void InitSettings()
    {
        controller = GetComponentInParent<CharacterController>();
        spartaAnim = GetComponent<Animation>();
        weaponCollider = weaponObject.GetComponent<BoxCollider>();
        weaponCollider.enabled = false;
        curHP = maxHP;
    }

    virtual protected void SetState()
    { 
        if(IsDead())
        {
            state = State.Die;
            Destroy( gameObject, 5.0f );
        }

    }
    abstract protected void SetDirection();

    protected void RotateCharacter()
    {
        if ( direction.sqrMagnitude > 0.01f )
        {
            Vector3 forward = Vector3.Slerp( transform.forward, direction,
                    rotateSpeed * Time.deltaTime / Vector3.Angle( transform.forward, direction ) );
            transform.LookAt( transform.position + forward );
            MoveCharacter();
        }
        else
        {
            state = State.Idle;
        }
    }
    protected void MoveCharacter()
    {
        switch ( state )
        {
            case State.Walk:
            {
                moveSpeed = walkSpeed;
            }
            break;
            case State.Run:
            {
                moveSpeed = runSpeed;
            }
            break;
            case State.Sprint:
            {
                moveSpeed = sprintSpeed;
            }
            break;
        }
        controller.Move( moveSpeed * Time.deltaTime * direction );
    }

    protected void PlayAnimations()
    {
        switch ( state )
        {
            case State.Idle:
            {
                PlayIdle();
            }
            break;
            case State.Walk:
            {
                PlayWalk();
            }
            break;
            case State.Run:
            {
                PlayRun();
            }
            break;
            case State.Sprint:
            {
                PlaySprint();
            }
            break;
            case State.Attack:
            {
                StartCoroutine( "PlayAttack" );
            }
            break;
            case State.Die:
            {
                PlayDead();
            }
            break;
            case State.Victory:
            {
                PlayVictory();
            }
            break;
        }
    }
    protected void PlayIdle()
    {
        spartaAnim.wrapMode = WrapMode.Loop;
        spartaAnim.CrossFade( "idle", 0.3f );
    }
    protected void PlayWalk()
    {
        spartaAnim.wrapMode = WrapMode.Loop;
        spartaAnim.CrossFade( "walk", 0.3f );
    }
    protected void PlayRun()
    {
        spartaAnim.wrapMode = WrapMode.Loop;
        spartaAnim.CrossFade( "run", 0.3f );
    }
    protected void PlaySprint()
    {
        spartaAnim.wrapMode = WrapMode.Loop;
        spartaAnim.CrossFade( "charge", 0.3f );
    }
    protected void PlayDead()
    {
        spartaAnim.wrapMode = WrapMode.Once;
        spartaAnim.CrossFade( "die", 0.3f );
    }

    protected void PlayVictory()
    {
        spartaAnim.wrapMode = WrapMode.Loop;
        spartaAnim.CrossFade("victory", 0.3f);
    }

    IEnumerator PlayAttack()
    {
        if ( !spartaAnim.IsPlaying( "attack" ) )
        {
            spartaAnim.wrapMode = WrapMode.Once;
            spartaAnim.CrossFade( "attack", 0.3f );

            float delayTime = spartaAnim.GetClip( "attack" ).length - 0.3f; // playtime - crossfadetime

            yield return new WaitForSeconds( attackHitDelay );
            weaponCollider.enabled = true;
            yield return new WaitForSeconds( attackDelay - attackHitDelay );
            weaponCollider.enabled = false;
            state = State.Idle;
            spartaAnim.wrapMode = WrapMode.Loop;
            spartaAnim.CrossFade( "idle", 0.3f );
        }
    }

    protected void DecreaseHP()
    {
        if(curHP > 0)
        {
            curHP--;
        }
    }

    protected bool IsDead()
    {
        return curHP <= 0;
    }
    

    private void OnTriggerEnter( Collider other )
    {
        if(other.gameObject.CompareTag("Weapon"))
        {
            DecreaseHP();
        }
    }
}
