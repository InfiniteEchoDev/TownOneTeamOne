using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;


[SelectionBase]
public class Llama : MonoBehaviour {


	public Vector3 SizeVariation;

	public float AttentionTime;
	public float Speed;
	public float AngularSpeed;
	public float Acceleration;



	public GameObject Model;


	private LlamaAttentionState _currAttentionState = LlamaAttentionState.Wandering;
	private LlamaAttentionState currAttentionState {
		get {
			return _currAttentionState;
		}
		set {
			if( _currAttentionState == LlamaAttentionState.Attentive && value == LlamaAttentionState.Wandering ) {
				BeginWandering();
			}

			_currAttentionState = value;
		}
	}

	private float currAttentionTimeRemaining;


	private NavMeshAgent agent;

	private void Awake() {
		SizeVariation.x = Random.Range( LlamaMgr.Instance.XYZSizeVariationRange.Min, LlamaMgr.Instance.XYZSizeVariationRange.Max );
		SizeVariation.y = Random.Range( LlamaMgr.Instance.XYZSizeVariationRange.Min, LlamaMgr.Instance.XYZSizeVariationRange.Max );
		SizeVariation.z = Random.Range( LlamaMgr.Instance.XYZSizeVariationRange.Min, LlamaMgr.Instance.XYZSizeVariationRange.Max );

		Model.transform.localScale = new Vector3( Model.transform.localScale.x * SizeVariation.x, Model.transform.localScale.y * SizeVariation.y, Model.transform.localScale.z * SizeVariation.z );
	}

	private void Start() {
		if( agent is null )
			agent = GetComponent<NavMeshAgent>();

		AttentionTime = Random.Range( AIMgr.Instance.LlamaAttentionTimeRangeGross.Min, AIMgr.Instance.LlamaAttentionTimeRangeGross.Max );
		
		Speed = Random.Range( AIMgr.Instance.LlamaSpeedRange.Min, AIMgr.Instance.LlamaSpeedRange.Max );
		agent.speed = Speed;
		AngularSpeed = Random.Range( AIMgr.Instance.LlamaAngularSpeedRange.Min, AIMgr.Instance.LlamaAngularSpeedRange.Max );
		agent.angularSpeed = AngularSpeed;
		Acceleration = Random.Range( AIMgr.Instance.LlamaAccelerationRange.Min, AIMgr.Instance.LlamaAccelerationRange.Max );
		agent.acceleration = Acceleration;
	}


	private void Update() {

		currAttentionTimeRemaining -= Time.deltaTime;

		switch( currAttentionState ) {
			case LlamaAttentionState.Unknown:
				break;
			case LlamaAttentionState.Attentive:
				if( currAttentionTimeRemaining <= 0 )
					currAttentionState = LlamaAttentionState.Wandering;
				break;
			case LlamaAttentionState.Wandering:
				if( currAttentionTimeRemaining <= 0 )
					BeginWandering();
				break;
		}
	}

	public void BeginAttentionOnPoint( Vector3 pointToAttend ) {

		agent.speed = Speed;
		agent.SetDestination( pointToAttend );

		_currAttentionState = LlamaAttentionState.Attentive;

		ResetCurrAttentionTime();

	}
	void BeginWandering() {
		Vector3 newDest = Random.insideUnitSphere * AIMgr.Instance.LlamaMaxWanderRange;

		agent.speed = Speed * .4f;
		agent.SetDestination( new Vector3( transform.position.x + newDest.x, .25f, transform.position.z + newDest.z ) );

		ResetCurrAttentionTime();
	}

	void ResetCurrAttentionTime() {
		currAttentionTimeRemaining = AttentionTime + Random.Range( AIMgr.Instance.LlamaAttentionTimeRangeFineAdjust.Min, AIMgr.Instance.LlamaAttentionTimeRangeFineAdjust.Max );
	}

}
