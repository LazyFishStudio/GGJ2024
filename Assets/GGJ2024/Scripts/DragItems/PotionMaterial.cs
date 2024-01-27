using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionMaterial : DragItem {
	public string word;

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
		transform.localScale *= 1.2f;

		GGJGameMgr.Instance.tooltip.gameObject.SetActive(true);
		GGJGameMgr.Instance.tooltip.SetupText(word);
	}
	public override void OnHoverExit() {
		transform.localScale /= 1.2f;

		GGJGameMgr.Instance.tooltip.gameObject.SetActive(false);
	}
}
 