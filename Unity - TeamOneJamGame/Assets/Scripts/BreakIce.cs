using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakIce : MonoBehaviour
{
	
	[SerializeField]
	float breakCooldown = 1f;
	[SerializeField]
	int breakLimit = 4;
	[SerializeField]
	int currentBreaks = 0;
	float timer = 0f;
	bool onCooldown = false;
	[SerializeField]
	GameObject vfx;
	
	// Update is called once per frame
    void Update()
    {
	    if (onCooldown){
	    	timer -= Time.deltaTime;
	    	if ( timer < 0f ) {
	    		onCooldown = false;
	    		currentBreaks++;
				Instantiate(vfx, transform.position, Quaternion.identity);
				if ( currentBreaks >= breakLimit)
                {
					Destroy(this.gameObject);
                }
	    	}
	    }
    }
    
	public void BreakingIce() {
		if ( !onCooldown ) {
			timer = breakCooldown;
			onCooldown = true;
		}
	}
}
