using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region Public Fields
    public GameObject Target;
    public float Damage = 1.0f;
    public float ConstantDamage = 0.5f;
    public float StopTime = 3.0f;
    #endregion


    #region Private Fields
    NavMeshAgent agent;
    Animator animator;
    float timer = 0.0f;
    bool isStun = false;
    protected Vector3 targetPos;
    #endregion

    
    #region MonoBehaviour Callback
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(agent == null)
        {
            Debug.LogError( "EnemyController: Miss Agent Component" );
        }

        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError( "EnemyController: Miss Animator Component" );
        }
    }

    protected virtual void Update()
    {
        if(agent == null)
        {
            return;
        }
        FindTargetPosition();
        agent.destination = targetPos;
        if(isStun)
        {
            StopMove();
        }

        UpdateAnimations();
    }
    protected virtual void OnTriggerEnter( Collider other )
    {
        if ( other.gameObject.CompareTag( "Player" ) )
        {
            GameManager_MazeRunner.Instance._MazeManager.AddPlayerScore( -Damage );
        }
        if ( other.gameObject.CompareTag( "Weapon" ) )
        {
            isStun = true;
        }
        if (other.gameObject.CompareTag("Item"))
        {
            Destroy(other.gameObject);
        }
    }
    protected virtual void OnTriggerStay( Collider other )
    {
        if ( other.gameObject.CompareTag( "Player" ) )
        {
            GameManager_MazeRunner.Instance._MazeManager.AddPlayerScore( -ConstantDamage * Time.deltaTime );
        }
    }
    protected virtual void FindTargetPosition()
    {
        targetPos = Target.transform.position;
    }
    #endregion




    #region Private Methods
    void UpdateAnimations()
    {
        if(animator == null)
        {
            return;
        }

        animator.SetFloat( "Speed", agent.velocity.magnitude );
    }

    void StopMove()
    {
        timer += Time.deltaTime;
        if(timer <= StopTime)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
            timer = 0.0f;
            isStun = false;
        }
    }

    #endregion

}
