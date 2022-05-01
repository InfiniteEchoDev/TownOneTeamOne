﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

[RequireComponent(typeof(CharacterController))]
public class HerderController : MonoBehaviour
{
	[SerializeField]
	private float playerSpeed = 2.0f;
	[SerializeField]
	private float jumpHeight = 1.0f;
	[SerializeField]
	private float gravityValue = -9.81f;
	[SerializeField]
	private bool inSnow = false;
	[SerializeField]
	private Actions ShovelingAction;

	private CharacterController controller;
	private Vector3 playerVelocity;
	private bool groundedPlayer;
	
	private Vector2 movementInput = Vector2.zero;
	private bool jumped = false;
	[SerializeField]
	private bool shoveling = false;

	private void Start()
	{
		controller = gameObject.GetComponent<CharacterController>();
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
		shoveling = context.action.IsPressed();
		
	}

	public void OnActivateWhistle( InputAction.CallbackContext context ) {
		if( context.action.triggered ) {
			AIMgr.Instance.DirectAllLlamasAttentionOnPoint( transform.position );
		}
	}

	void Update()
	{
		groundedPlayer = controller.isGrounded;
		if (groundedPlayer && playerVelocity.y < 0)
		{
			playerVelocity.y = 0f;
		}

		Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
		controller.Move(move * Time.deltaTime * playerSpeed);

		if (move != Vector3.zero)
		{
			gameObject.transform.forward = move;
		}

		// Changes the height position of the player..
		if (jumped && groundedPlayer)
		{
			playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
		}

		if (shoveling) {
			ShovelingAction.Invoke(ShovelingAction.gameObject);
		}

		playerVelocity.y += gravityValue * Time.deltaTime;
		controller.Move(playerVelocity * Time.deltaTime);
	}
}