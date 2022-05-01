using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelPickup : MonoBehaviour
{
	[SerializeField]
	GameObject shovelBridge;
	
	public void GetShovel(HerderController hc){
		hc.shovelObject.SetActive(true);
		
	}
}
