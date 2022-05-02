using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using UnityEngine.InputSystem;


public enum LlamaAttentionState {
	Unknown = 0,
	Attentive,
	Wandering,
}
public class AIMgr : Singleton<AIMgr> {

	public List<Llama> Llamas = new();


	public (float Min, float Max) LlamaAttentionTimeRangeGross = ( 5f, 10 );
	public (float Min, float Max) LlamaAttentionTimeRangeFineAdjust = ( -2f, 2f );
	public (float Min, float Max) LlamaSpeedRange = ( 1f, 3f );
	public (float Min, float Max) LlamaAngularSpeedRange = ( 150f, 260f );
	public (float Min, float Max) LlamaAccelerationRange = ( 3, 6 );


	public float LlamaMaxWanderRange = 5;



	[Header( "Obj Refs" )]
	public NavMeshSurfaceController LlamaAgentNavMeshSurfaceController;


	private void Start() {

		if( Llamas.Count == 0 )
			Llamas.AddRange( FindObjectsOfType<Llama>() );

		InputMgr.Instance.OnMouseClickOnUILayer +=
			( Vector3 clickPoint ) => {
				DirectAllLlamasAttentionOnPoint( clickPoint );
			};

		UpdateLlamaAgentNavMeshData();
	}

	public void DirectAllLlamasAttentionOnPoint( Vector3 pointForAttention ) {
		foreach( Llama llama in Llamas ) {
			llama.BeginAttentionOnPoint( pointForAttention );
		}
	}


	public void UpdateLlamaAgentNavMeshData() {
		LlamaAgentNavMeshSurfaceController.RebakeNavMesh();
	}

	public void UpdateLlamaAgentNavMeshDataNextFrame() {
		StartCoroutine( UpdateLlamaAgentNavMeshDataNextFrameCoroutine() );
	}
	IEnumerator UpdateLlamaAgentNavMeshDataNextFrameCoroutine() {
		yield return 0;

		UpdateLlamaAgentNavMeshData();
	}


}
