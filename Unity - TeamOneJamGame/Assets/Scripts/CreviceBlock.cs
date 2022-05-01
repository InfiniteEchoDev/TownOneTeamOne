using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreviceBlock : MonoBehaviour
{
	public bool shovelLaidHere = false;
	[SerializeField]
	GameObject attachPoint;
	[SerializeField]
	GameObject shovelBridge;
	
	public void PutShovel(HerderController hc){
		GameObject bridge = Instantiate(shovelBridge, attachPoint.transform.position, Quaternion.identity);
		Vector3 resetpoint = Vector3.zero;
		bridge.transform.position = resetpoint;
		//bridge.GetComponent
		hc.shovelObject.SetActive(false);
		shovelLaidHere = true;
	}
	
	
}
