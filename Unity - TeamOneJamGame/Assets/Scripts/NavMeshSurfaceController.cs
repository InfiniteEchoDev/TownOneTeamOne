using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Unity.AI.Navigation;


public class NavMeshSurfaceController : MonoBehaviour {

	private NavMeshSurface LlamaAgentNavMeshSurface;


	private void Awake() {
		if( LlamaAgentNavMeshSurface is null )
			LlamaAgentNavMeshSurface = GetComponent<NavMeshSurface>();
	}



	public void RebakeNavMesh() {
		AsyncOperation updateNavMesh = LlamaAgentNavMeshSurface.UpdateNavMesh( LlamaAgentNavMeshSurface.navMeshData );
	}
}
