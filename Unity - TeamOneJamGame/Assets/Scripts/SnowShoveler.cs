using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowShoveler : MonoBehaviour
{
	[SerializeField]
	private GameObject snowBlock;
	[SerializeField]
	private bool hasSnow;
	[SerializeField]
	GameObject vfx;

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
		
	}

	private void OnTriggerExit(Collider other)
	{
		if ( other.tag == "Snow")
		{
			snowBlock = null;
			hasSnow = false;
		}
	}

	public void destroySnow()
	{
		Instantiate(vfx, snowBlock.transform.position, Quaternion.identity);
	}

}
