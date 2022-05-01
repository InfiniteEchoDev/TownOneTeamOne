using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;


public enum LlamaBehaviourState {
	Unknown = 0,
	Walking,
	Idle,
	Grazing,
}

[SelectionBase]
public class Llama : MonoBehaviour {


	public Vector3 SizeVariation;

	public float AttentionTime;
	public float Speed;
	public float AngularSpeed;
	public float Acceleration;





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


	private LlamaBehaviourState _currBehaviourState = LlamaBehaviourState.Idle;
	private LlamaBehaviourState currBehaviourState {
		get {
			return _currBehaviourState;
		}
		set {
			_currBehaviourState = value;
			UpdateAnimState();
		}
	}



	[Header( "Obj Refs" )]
	public GameObject Model;



	private float currAttentionTimeRemaining;


	private NavMeshAgent agent;
	private Animator llanimator;

	private void Awake() {
		if( llanimator is null )
			llanimator = GetComponent<Animator>();
	}

	private void Start() {
		SizeVariation.x = Random.Range( LlamaMgr.Instance.XZSizeVariationRange.Min, LlamaMgr.Instance.XZSizeVariationRange.Max );
		SizeVariation.y = Random.Range( LlamaMgr.Instance.YSizeVariationRange.Min, LlamaMgr.Instance.YSizeVariationRange.Max );
		SizeVariation.z = Random.Range( LlamaMgr.Instance.XZSizeVariationRange.Min, LlamaMgr.Instance.XZSizeVariationRange.Max );

		Model.transform.localScale = new Vector3( Model.transform.localScale.x * SizeVariation.x, Model.transform.localScale.y * SizeVariation.y, Model.transform.localScale.z * SizeVariation.z );


		if( agent is null )
			agent = GetComponent<NavMeshAgent>();

		AttentionTime = Random.Range( AIMgr.Instance.LlamaAttentionTimeRangeGross.Min, AIMgr.Instance.LlamaAttentionTimeRangeGross.Max );
		
		Speed = Random.Range( AIMgr.Instance.LlamaSpeedRange.Min, AIMgr.Instance.LlamaSpeedRange.Max );
		agent.speed = Speed;
		AngularSpeed = Random.Range( AIMgr.Instance.LlamaAngularSpeedRange.Min, AIMgr.Instance.LlamaAngularSpeedRange.Max );
		agent.angularSpeed = AngularSpeed;
		Acceleration = Random.Range( AIMgr.Instance.LlamaAccelerationRange.Min, AIMgr.Instance.LlamaAccelerationRange.Max );
		agent.acceleration = Acceleration;



		// Replace llama models randomly
		ReplaceModel( LlamaMgr.Instance.LlamaModels[Random.Range( 0, LlamaMgr.Instance.LlamaModels.Count - 1 )] );
	}


	private void Update() {

		currAttentionTimeRemaining -= Time.deltaTime;

		switch( currAttentionState ) {
			case LlamaAttentionState.Unknown:
				break;
			case LlamaAttentionState.Attentive:
				if( currAttentionTimeRemaining <= 0 ) {
					currAttentionState = LlamaAttentionState.Wandering;
					//currBehaviourState = LlamaBehaviourState.Walking;
					BeginAnything();
				}
				break;
			case LlamaAttentionState.Wandering:
				if( currAttentionTimeRemaining <= 0 ) {
					BeginAnything();
				}
				break;
		}
	}

	public void BeginAttentionOnPoint( Vector3 pointToAttend ) {

		agent.speed = Speed;
		agent.SetDestination( pointToAttend );

		_currAttentionState = LlamaAttentionState.Attentive;
		currBehaviourState = LlamaBehaviourState.Walking;

		ResetCurrAttentionTime();

	}
	public void BeginAnything() {
		float behaviourChoice = Random.Range( 0f, 1f );

		//Debug.Log( "Curr behaviour State: " + currBehaviourState );

		if( behaviourChoice < .5f ) {
			BeginWandering();
		} else if( behaviourChoice > .75f ) {
			PauseMovement();
			currBehaviourState = LlamaBehaviourState.Idle;
		} else {
			PauseMovement();
			currBehaviourState = LlamaBehaviourState.Grazing;
		}
	}
	void BeginWandering() {
		Vector3 newDest = Random.insideUnitSphere * AIMgr.Instance.LlamaMaxWanderRange;

		agent.speed = Speed * .4f;
		agent.SetDestination( new Vector3( transform.position.x + newDest.x, .25f, transform.position.z + newDest.z ) );

		currBehaviourState = LlamaBehaviourState.Walking;
		ResetCurrAttentionTime();
	}

	void PauseMovement() {
		agent.ResetPath();
	}

	void ResetCurrAttentionTime() {
		currAttentionTimeRemaining = AttentionTime + Random.Range( AIMgr.Instance.LlamaAttentionTimeRangeFineAdjust.Min, AIMgr.Instance.LlamaAttentionTimeRangeFineAdjust.Max );
	}




	void ReplaceModel( GameObject toReplaceModelWith ) {

		GameObject newModel = Instantiate( toReplaceModelWith, Model.transform.position, Model.transform.rotation, Model.transform.parent );
		newModel.transform.localScale = Model.transform.localScale;

		Destroy( Model );

		Model = newModel;
		Model.SetActive( true );

		llanimator.Rebind();

	}




	void UpdateAnimState() {
		switch( currBehaviourState ) {
			case LlamaBehaviourState.Unknown:
				llanimator.SetTrigger( "BeginIdle" );
				break;
			case LlamaBehaviourState.Walking:
				llanimator.SetTrigger( "BeginWalking" );
				break;
			case LlamaBehaviourState.Idle:
				llanimator.SetTrigger( "BeginIdle" );
				break;
			case LlamaBehaviourState.Grazing:
				llanimator.SetTrigger( "BeginGrazing" );
				break;
		}
	}
}
