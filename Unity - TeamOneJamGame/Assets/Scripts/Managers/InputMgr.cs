using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;


public class InputMgr : Singleton<InputMgr> {


	public delegate void OnMouseClickOnUILayerDelegate( Vector3 mouseClickLocation );
	public event OnMouseClickOnUILayerDelegate OnMouseClickOnUILayer;

	private void Start() {
		//OnMouseClickOnUILayer += ( Vector3 clickPoint ) => {
		//	Debug.Log( clickPoint );
		//};
	}

	private void Update() {

		//Debug.Log( CameraMgr.Instance );

		if( Mouse.current.leftButton.isPressed )
			//if( Keyboard.current.spaceKey.wasPressedThisFrame )
			//if( Input.GetMouseButtonDown( 0 ) )
			if( Physics.Raycast(
				CameraMgr.Instance.MainCamera.ScreenPointToRay( Mouse.current.position.ReadValue() ),
				//CameraMgr.Instance.MainCamera.ScreenPointToRay( Input.mousePosition ),
				out RaycastHit hit,
				Mathf.Infinity,
				1 << LayerMgr.MouseClickSurfaceLayer
			) ) {
				OnMouseClickOnUILayer?.Invoke( hit.point );
			}
		//if( Physics.Raycast )

	}

}
