using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Variables;

[RequireComponent(typeof(CharacterController))]
public class HerderController : MonoBehaviour
{
	[SerializeField]
	private float playerSpeed = 2.0f;
	[SerializeField]
	private float snowSpeed = 1.0f;
	[SerializeField]
	private float jumpHeight = 1.0f;
	[SerializeField]
	private float gravityValue = -9.81f;
	[SerializeField]
	private bool inSnow = false;
	[SerializeField]
	private Actions ShovelingAction;
	[SerializeField]
	private Transform skinLocation;

	private CharacterController controller;
	private Vector3 playerVelocity;
	private bool groundedPlayer;
	
	private Vector2 movementInput = Vector2.zero;
	private bool jumped = false;
	
	public bool hasShovel = true;
	public bool shoveling = false;
	public GameObject shovelObject;
	
	float currentSpeeed;
	
	private void Start()
	{
		controller = gameObject.GetComponent<CharacterController>();
		currentSpeeed = playerSpeed;
	}
	
	public void OnMove(InputAction.CallbackContext context) {
		movementInput = context.ReadValue<Vector2>();
	}
	
	public void OnJump(InputAction.CallbackContext context) {
		jumped = context.ReadValue<bool>();
		jumped = context.action.triggered;
	}
	
	public void OnShovel(InputAction.CallbackContext context) {
		//shoveling = context.ReadValue<bool>();
		if ( hasShovel )
			shoveling = context.action.IsPressed();
		
	}
	
	public void OnSpawnCharacter(GameObject spawnObject) {
		GameObject spawnedSkin = Instantiate(spawnObject,skinLocation);
			Vector3 newPosition = Vector3.zero;
			spawnedSkin.transform.localPosition = newPosition;
			
	}

	public void OnActivateWhistle( InputAction.CallbackContext context ) {
		if( context.action.triggered ) {
			AIMgr.Instance.DirectAllLlamasAttentionOnPoint( transform.position );
		}
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Snow")
		{
			currentSpeeed = snowSpeed;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Snow")
		{
			currentSpeeed = playerSpeed;
		}
	}
	
	public void ResumeSpeed(){
		currentSpeeed = playerSpeed;
	}

	void Update()
	{
		groundedPlayer = controller.isGrounded;
		if (groundedPlayer && playerVelocity.y < 0)
		{
			playerVelocity.y = 0f;
		}

		Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
		controller.Move(move * Time.deltaTime * currentSpeeed);

		if (move != Vector3.zero)
		{
			gameObject.transform.forward = move;
		}

		// Changes the height position of the player..
		if (jumped && groundedPlayer)
		{
			playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
		}

		if (shoveling && hasShovel) {
			ShovelingAction.Invoke(ShovelingAction.gameObject);
		}

		playerVelocity.y += gravityValue * Time.deltaTime;
		controller.Move(playerVelocity * Time.deltaTime);
	}
}