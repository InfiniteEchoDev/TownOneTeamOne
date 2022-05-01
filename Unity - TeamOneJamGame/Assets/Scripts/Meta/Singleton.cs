using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Singleton<SingletonClass> : MonoBehaviour where SingletonClass : MonoBehaviour {
	public static SingletonClass Instance { get; private set; }

	protected virtual void Awake() {
		if( Instance is not null ) {
			Destroy( gameObject );
				return;
		}

		Instance = this as SingletonClass;
	}

	protected virtual void OnApplicationQuit() {
		Instance = null;
		Destroy( gameObject );
	}
}
