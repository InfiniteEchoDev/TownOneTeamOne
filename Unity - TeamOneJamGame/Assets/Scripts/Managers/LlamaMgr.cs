using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class LlamaMgr : Singleton<LlamaMgr> {

	public (float Min, float Max) XZSizeVariationRange = (.75f, 1.25f);
	public (float Min, float Max) YSizeVariationRange = (.75f, 1.5f);


	[Header( "Obj Refs" )]
	public GameObject LlamaModelParent;
	public List<GameObject> LlamaModels;
}
