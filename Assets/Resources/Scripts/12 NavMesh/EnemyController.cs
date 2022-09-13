using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region Public Fields
    public GameObject Target;
    #endregion


    #region Private Fields
    NavMeshAgent agent;
    Animator animator;
    #endregion

    
    #region MonoBehaviour Callback
    void Start()
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

    void Update()
    {
        if(agent == null)
        {
            return;
        }

        agent.destination = Target.transform.position;


        UpdateAnimations();
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
    #endregion

}
