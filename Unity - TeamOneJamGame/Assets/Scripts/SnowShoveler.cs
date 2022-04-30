using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowShoveler : MonoBehaviour
{
	[SerializeField]
	private GameObject snowBlock;
	[SerializeField]
	private bool hasSnow;
    
	private void OnTriggerStay(Collider other)
	{
		if ( other.tag == "Snow")
		{
			Debug.Log("snow detected");
			snowBlock = other.gameObject;
			hasSnow = true;
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
