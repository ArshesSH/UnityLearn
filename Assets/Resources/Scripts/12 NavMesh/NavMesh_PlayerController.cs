using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh_PlayerController : MonoBehaviour
{
    public GameObject playerModel;
    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = playerModel.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                //agent.destination = hit.point;
                agent.SetDestination( hit.point );
            }
        }

    }

    private void FixedUpdate()
    {
        animator.SetFloat( "Speed", agent.velocity.magnitude );
    }

}
