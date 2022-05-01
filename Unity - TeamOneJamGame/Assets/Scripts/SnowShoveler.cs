using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowShoveler : MonoBehaviour
{
	[SerializeField]
	private GameObject snowBlock;
	[SerializeField]
	private bool hasSnow;
	
	public HerderController	playerController;
    
	private void OnTriggerStay(Collider other)
	{
		if ( other.tag == "Snow")
		{
			//Debug.Log("snow detected");
			snowBlock = other.gameObject;
			hasSnow = true;
		}
		if ( other.tag == "Ice" && playerController.shoveling)
		{
			other.gameObject.GetComponent<BreakIce>().BreakingIce();
		}
		if ( other.tag == "Shovel" && playerController.shoveling)
		{
			if ( other.gameObject.GetComponent<SnowShoveler>().playerController.shoveling) {
				Debug.Log("Llama call");
				AIMgr.Instance.DirectAllLlamasAttentionOnPoint( transform.position );
			}
		}
		
	}

	private void OnTriggerExit(Collider other)
	{
		if ( other.tag == "Snow")
		{
			snowBlock = null;
			hasSnow = false;
		}
	}
}
