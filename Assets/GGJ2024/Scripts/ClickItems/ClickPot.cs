using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPot : ClickItem
{
	public GameObject potionPrefab;
	public Vector3 productBias;

	public override bool CheckClick() {
		return GetComponent<PotionCauldron>().sentence != "";
	}

	public override void OnClick() {
		Bottle bottle = Instantiate(potionPrefab, transform.position + productBias, Quaternion.identity, null).GetComponent<Bottle>();
		bottle.result = GetComponent<PotionCauldron>().sentence;
		bottle.water.color = GetComponent<PotionCauldron>().water.color;
		GetComponent<PotionCauldron>().ClearMaterial();
	}
}
