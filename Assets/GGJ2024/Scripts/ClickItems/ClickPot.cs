using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickPot : ClickItem
{
	public GameObject potionPrefab;
	public Vector3 productBias;
	public SpoonAnim spoonAnim;

	public override bool CheckClick() {
		return GetComponent<PotionCauldron>().sentence != "";
	}

	public override void OnClick() {
		spoonAnim.StartMove(2.0f);
		transform.DOShakeScale(2.0f, 0.25f).OnComplete(() => {
			Bottle bottle = Instantiate(potionPrefab, transform.position + productBias, Quaternion.identity, null).GetComponent<Bottle>();
			bottle.result = GetComponent<PotionCauldron>().sentence;
			bottle.water.color = GetComponent<PotionCauldron>().water.color;
			GetComponent<PotionCauldron>().ClearMaterial();
		});
		
	}
}
