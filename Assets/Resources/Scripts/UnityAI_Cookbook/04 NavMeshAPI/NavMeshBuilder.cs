using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NavMeshAPI
{
	public class NavMeshBuilder : MonoBehaviour
	{
		public NavMeshSurface[] surfaces;


		public void Build()
        {
			foreach(var surface in surfaces)
            {
				surface.BuildNavMesh();
            }
        }
		public IEnumerator BuildInFrames(System.Action eventHandler)
        {
			foreach(var surface in surfaces)
            {
				surface.BuildNavMesh();
				yield return null;
            }
			if(eventHandler != null)
            {
				eventHandler.Invoke();
            }
        }
	}
}