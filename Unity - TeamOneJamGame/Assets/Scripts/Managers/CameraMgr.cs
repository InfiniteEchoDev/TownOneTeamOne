using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class CameraMgr : Singleton<CameraMgr> {

	public Camera MainCamera;


	private new void Awake() {
		base.Awake();

		if( MainCamera is null )
			MainCamera = Camera.main;
	}

}
