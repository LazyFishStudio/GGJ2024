using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionMaterial : DragItem {
	public string word;

	private TextMeshPro textMesh;
	protected override void Awake() {
		base.Awake();
		textMesh = GetComponentInChildren<TextMeshPro>();
	}

	protected virtual void Update() {
		base.Update();
		if (textMesh != null)
			textMesh.text = word;
	}
}
 