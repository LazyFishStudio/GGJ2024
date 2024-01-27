using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionMaterial : DragItem {
	public string word;
	public string itemName;
	public string itemDesc;

	private TextMeshPro textMesh;
	private void Awake() {
		textMesh = GetComponentInChildren<TextMeshPro>();
	}

	protected override void Update() {
		base.Update();
		if (textMesh != null)
			textMesh.text = word;
	}

	public override void OnHoverEnter() {
		transform.localScale *= 1.1f;

		GGJGameMgr.Instance.tooltip.gameObject.SetActive(true);
		GGJGameMgr.Instance.tooltip.SetupText(word);
		GGJGameMgr.Instance.itemBox.SetActive(true);
		GGJGameMgr.Instance.itemName.text = itemName;
		GGJGameMgr.Instance.itemDesc.text = itemDesc;
	}

	public override void OnHoverExit() {
		transform.localScale /= 1.1f;

		GGJGameMgr.Instance.tooltip.gameObject.SetActive(false);
		GGJGameMgr.Instance.itemBox.SetActive(false);
	}
}
 