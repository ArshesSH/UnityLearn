using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh_PlayerController : MonoBehaviour
{
    public GameObject playerModel;
    public NavMeshAgent agent;
    Animator animator;
    RaycastHit rayHit = new RaycastHit();

    void Start()
    {
        if(agent == null)
        {
            Debug.LogError( "Can't Find Agent" );
        }
        animator = playerModel.GetComponent<Animator>();
    }

    void Update()
    {
        //if(Input.GetMouseButton(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        //    RaycastHit hit;

        //    if(Physics.Raycast(ray.origin, ray.direction, out hit))
        //    {
        //        //agent.destination = hit.point;
        //        agent.SetDestination( hit.point );
        //    }
        //}

        if ( Input.GetMouseButtonDown( 0 ) && !Input.GetKey( KeyCode.LeftShift ) )
        {
            var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            if ( Physics.Raycast( ray.origin, ray.direction, out rayHit ) )
                agent.destination = rayHit.point;
        }
    }

    private void FixedUpdate()
    {
        animator.SetFloat( "Speed", agent.velocity.magnitude );
    }

}
