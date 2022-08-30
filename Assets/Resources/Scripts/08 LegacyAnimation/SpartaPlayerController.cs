using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpartaPlayerController : MonoBehaviour
{
    [SerializeField]
    protected GameObject weapon;
    public float runSpeed = 6.0f;
    public float rotateSpeed = 360.0f;
    public float hitBoxOnDelay = 0.1f;
    public int hp = 3;
    public bool isDead = false;

    private bool isAttackNow = false;

    protected Animation spartanKingAnim;
    protected BoxCollider weaponCollider;

    CharacterController pcControl;
    Vector3 velocity;

    void Start()
    {
        spartanKingAnim = gameObject.GetComponentInChildren<Animation>();
        spartanKingAnim.wrapMode = WrapMode.Loop;
        spartanKingAnim.Play( "idle" );

        pcControl = GetComponent<CharacterController>();
        weaponCollider = weapon.GetComponent<BoxCollider>();
        weaponCollider.enabled = false;
    }
    void Update()
    {
        //PlayAnimation_1();
        //PlayAnimation_2();
        //PlayAnimation_3();
        ControlPlayer();
        CheckHP();
        //ControlCharacter();
        //ControlCharacter_Slerp();
    }

    void PlayAnimation_1()
    {
        //if(Input.GetKeyDown(KeyCode.F))
        if ( Input.GetMouseButton( 0 ) )
        {
            spartanKingAnim.Play( "attack" );
        }
        else if ( Input.GetMouseButton( 1 ) )
        {
            spartanKingAnim.Play( "resist" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha0 ) )
        {
            spartanKingAnim.Play( "idle" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
        {
            spartanKingAnim.Play( "walk" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
        {
            spartanKingAnim.Play( "run" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha3 ) )
        {
            spartanKingAnim.Play( "charge" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha4 ) )
        {
            spartanKingAnim.Play( "idlebattle" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha5 ) )
        {
            spartanKingAnim.Play( "resist" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha6 ) )
        {
            spartanKingAnim.Play( "victory" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha7 ) )
        {
            spartanKingAnim.Play( "salute" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha8 ) )
        {
            spartanKingAnim.Play( "die" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha9 ) )
        {
            spartanKingAnim.Play( "diehard" );
        }
    }

    void PlayAnimation_2()
    {
        if ( Input.GetKeyDown( KeyCode.F ) )
        {
            spartanKingAnim.wrapMode = WrapMode.Once;
            //spartanKingAnim.Play( "attack" );

            spartanKingAnim.CrossFade( "attack", 0.3f );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha0 ) )
        {
            spartanKingAnim.wrapMode = WrapMode.Loop;
            spartanKingAnim.Play( "idle" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
        {
            spartanKingAnim.wrapMode = WrapMode.Loop;
            spartanKingAnim.Play( "walk" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
        {
            spartanKingAnim.wrapMode = WrapMode.Loop;
            spartanKingAnim.Play( "run" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha3 ) )
        {
            spartanKingAnim.wrapMode = WrapMode.Loop;
            spartanKingAnim.Play( "charge" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha4 ) )
        {
            spartanKingAnim.wrapMode = WrapMode.Loop;
            spartanKingAnim.Play( "idlebattle" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha5 ) )
        {
            spartanKingAnim.wrapMode = WrapMode.Once;
            spartanKingAnim.Play( "resist" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha6 ) )
        {
            spartanKingAnim.wrapMode = WrapMode.Once;
            spartanKingAnim.Play( "victory" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha7 ) )
        {
            spartanKingAnim.wrapMode = WrapMode.Once;
            spartanKingAnim.Play( "salute" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha8 ) )
        {
            spartanKingAnim.wrapMode = WrapMode.Once;
            spartanKingAnim.Play( "die" );
        }
        if ( Input.GetKeyDown( KeyCode.Alpha9 ) )
        {
            spartanKingAnim.wrapMode = WrapMode.Once;
            spartanKingAnim.Play( "diehard" );
        }
    }

    // 애니메이션 실행 후 돌아가기 (코루틴)
    void PlayAnimation_3()
    {
        if ( Input.GetKeyDown( KeyCode.F ) )
        {
            StartCoroutine( "AttackToIdle" );
        }
    }


    void ControlCharacter()
    {
        velocity = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );

        velocity *= runSpeed;
        if ( velocity.magnitude > 0.5f )
        {
            spartanKingAnim.CrossFade( "run", 0.3f );
            transform.LookAt( transform.position + velocity );
        }
        else
        {
            spartanKingAnim.CrossFade( "idle", 0.3f );
        }


        //pcControl.Move( velocity * Time.deltaTime + Physics.gravity * Time.deltaTime);
        pcControl.SimpleMove( velocity + Physics.gravity );
    }

    void ControlPlayer()
    {
        if ( Input.GetKey( KeyCode.F ) )
        {
            StartCoroutine( "AttackToIdle" );
        }
        else if (!isAttackNow)
        {
            ControlCharacter_Slerp();   
        }
    }

    void ControlCharacter_Slerp()
    {
        Vector3 direction = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );

        
        if ( Input.GetKeyDown( KeyCode.F ) )
        {
            StartCoroutine( "AttackToIdle" );
        }
        else
        {
            if ( direction.sqrMagnitude > 0.01f )
            {
                spartanKingAnim.CrossFade( "run", 0.3f );

                Vector3 forward = Vector3.Slerp( transform.forward, direction,
                    rotateSpeed * Time.deltaTime / Vector3.Angle( transform.forward, direction ) );

                transform.LookAt( transform.position + forward );
            }
            else
            {
                spartanKingAnim.CrossFade( "idle", 0.3f );
            }

            pcControl.Move( direction * runSpeed * Time.deltaTime + Physics.gravity * 0.01f );
        }
    }

    IEnumerator AttackToIdle()
    {
        if ( !spartanKingAnim.IsPlaying( "attack" ) )
        {
            isAttackNow = true;
            spartanKingAnim.wrapMode = WrapMode.Once;
            spartanKingAnim.CrossFade( "attack", 0.3f );

            float delayTime = spartanKingAnim.GetClip( "attack" ).length - 0.3f; // playtime - crossfadetime

            yield return new WaitForSeconds( hitBoxOnDelay );
            weaponCollider.enabled = true;
            yield return new WaitForSeconds( delayTime - hitBoxOnDelay );
            weaponCollider.enabled = false;
            isAttackNow = false;
            spartanKingAnim.wrapMode = WrapMode.Loop;
            spartanKingAnim.CrossFade( "idle", 0.3f );
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        if(other.gameObject.CompareTag("Weapon"))
        {
            if( hp > 0 )
            {
                hp--;
            }
        }
    }

    protected void CheckHP()
    {
        if(hp <= 0 && !isDead)
        {
            isDead = true;
            spartanKingAnim.wrapMode = WrapMode.Once;
            spartanKingAnim.Play( "die" );
        }
    }


}