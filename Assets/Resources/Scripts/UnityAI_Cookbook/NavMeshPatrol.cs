using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshPatrol : MonoBehaviour
{
	/*--- Public Fields ---*/
	public float PointDistance = 0.5f;
	public Transform[] PatrolPoints;


	/*--- Private Fields ---*/
	int curPoint = 0;
	NavMeshAgent agent;


	/*--- MonoBehaviour Callbacks ---*/
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.autoBraking = false;
		curPoint = FindClosestPoint();
		GoToPoint( curPoint );
	}
	void Update()
	{
		if(!agent.pathPending && agent.remainingDistance < PointDistance)
        {
			curPoint = FindClosestPoint();
			GoToPoint( CalculateNextPoint() );
        }
	}

    private void OnGUI()
    {
		GUI.Box( new Rect( 0, 0, 150, 30 ), curPoint.ToString() );
    }


    /*--- Public Methods ---*/


    /*--- Private Methods ---*/
    int FindClosestPoint()
    {
		int index = -1;
		float distance = Mathf.Infinity;
		int i;
		Vector3 agentPosition = transform.position;
		Vector3 pointPosition;
		
		for(i = 0; i < PatrolPoints.Length; ++i )
        {
			pointPosition = PatrolPoints[i].position;
			float d = Vector3.Distance( agentPosition, pointPosition );
			if(d < distance)
            {
				index = i;
				distance = d;
            }
        }

		return index;
    }
	void GoToPoint(int next)
    {
		if(next < 0 || next >= PatrolPoints.Length)
        {
			return;
        }
		agent.destination = PatrolPoints[next].position;
    }
	int CalculateNextPoint()
    {
		return (curPoint + 1) % PatrolPoints.Length;
    }
}