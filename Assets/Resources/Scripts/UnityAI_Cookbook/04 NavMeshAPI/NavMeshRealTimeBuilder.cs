using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using NavBuilder = UnityEngine.AI.NavMeshBuilder;

namespace NavMeshAPI
{
	[DefaultExecutionOrder( -102 )]
	public class NavMeshRealTimeBuilder : MonoBehaviour
	{
		public Transform Agent;
		public Vector3 BoxSize = new Vector3( 50.0f, 20.0f, 50.0f );
		[Range( 0.01f, 1.0f )]
		public float SizeChange = 0.1f;

		NavMeshData navMesh;
		AsyncOperation async;
		NavMeshDataInstance navMeshInstance;
		List<NavMeshBuildSource> buildSources = new List<NavMeshBuildSource>();


        private void Awake()
        {
			if (Agent == null)
            {
				Agent = transform;
            }
        }
        private IEnumerator Start()
		{
			while ( true )
			{
				UpdateNavMesh( true );
				yield return async;
			}
		}
        private void OnEnable()
        {
			navMesh = new NavMeshData();
			navMeshInstance = NavMesh.AddNavMeshData( navMesh );
			if ( Agent == null )
			{
				Agent = transform;
			}
			UpdateNavMesh( false );
        }
        private void OnDisable()
        {
			navMeshInstance.Remove();	
        }


        static Vector3 Quantize(Vector3 a, Vector3 q)
        {
			float x = q.x * Mathf.Floor( a.x / q.x );
			float y = q.y * Mathf.Floor( a.y / q.y );
			float z = q.z * Mathf.Floor( a.z / q.z );
			return new Vector3( x, y, z );
		}
		Bounds QuantizedBounds()
        {
			var center = Agent ? Agent.position : transform.position;
			return new Bounds( Quantize( center, BoxSize * SizeChange ), BoxSize );
        }
		void UpdateNavMesh( bool isAsync = false )
		{
			NavMeshSourceTag.Collect( ref buildSources );
			NavMeshBuildSettings settings = NavMesh.GetSettingsByID( 0 );
			Bounds bounds = QuantizedBounds();
			if ( isAsync )
			{
				async = NavBuilder.UpdateNavMeshDataAsync( navMesh, settings, buildSources, bounds );
			}
			else
            {
				NavBuilder.UpdateNavMeshData( navMesh, settings, buildSources, bounds );
			}
        }
		void OnDrawGizmosSelected()
		{
			if ( navMesh )
			{
				Gizmos.color = Color.green;
				Gizmos.DrawWireCube( navMesh.sourceBounds.center, navMesh.sourceBounds.size );
			}

			Gizmos.color = Color.yellow;
			var bounds = QuantizedBounds();
			Gizmos.DrawWireCube( bounds.center, bounds.size );

			Gizmos.color = Color.green;
			var center = Agent ? Agent.position : transform.position;
			Gizmos.DrawWireCube( center, BoxSize );
		}
	}
}