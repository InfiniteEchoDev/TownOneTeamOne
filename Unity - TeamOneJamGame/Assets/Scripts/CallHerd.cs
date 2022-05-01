using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Variables;

public class CallHerd : MonoBehaviour
{
	[SerializeField]
	private Actions JumpAction;
	public HerderController	playerController;
	
	bool jumping = false;
    
	private void OnTriggerStay(Collider other)
	{
		if ( other.tag == "CallHerd" && playerController.shoveling && !jumping)
		{
			if ( other.gameObject.GetComponent<CallHerd>().playerController.shoveling) {
				Debug.Log("Llama call");
				jumping = true;
				JumpAction.Invoke(JumpAction.gameObject);
				AIMgr.Instance.DirectAllLlamasAttentionOnPoint( transform.position );
			}
		}
		
	}



}
