using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;


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



	NavMeshAgent llamaAgentRef;

	private void Start() {

		if( Llamas.Count == 0 )
			Llamas.AddRange( FindObjectsOfType<Llama>() );

		//foreach( Llama llama in Llamas ) {
		//	llamaAgent = llama.GetComponent<NavMeshAgent>();
		//	llamaAgent.SetDestination( new Vector3( 0, 0, 10 ) );
		//}

		InputMgr.Instance.OnMouseClickOnUILayer +=
			( Vector3 clickPoint ) => {
				foreach( Llama llama in Llamas ) {
					//llamaAgentRef = llama.GetComponent<NavMeshAgent>();
					//llamaAgentRef.SetDestination( clickPoint );
					llama.BeginAttentionOnPoint( clickPoint );
				}
			};
	}
}
