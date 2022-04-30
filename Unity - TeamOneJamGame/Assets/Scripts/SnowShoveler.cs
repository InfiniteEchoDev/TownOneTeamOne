using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowShoveler : MonoBehaviour
{
	[SerializeField]
	private GameObject snowBlock;
	[SerializeField]
	private bool hasSnow;
    
	private void OnTriggerEnter(Collider other)
	{
		if ( other.tag == "Snow")
		{
			snowBlock = other.gameObject;
		}
	}
}
