using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bottle : DragItem
{
    public string result = "";

	private TextMeshPro textMesh;
	private void Awake() {
		textMesh = GetComponentInChildren<TextMeshPro>();
	}

	protected override void Update() {
		base.Update();
		if (textMesh != null) {
			if (result == "") {
				textMesh.text = "¿ÕÆ¿";
			} else {
				textMesh.text = result;
			}
		}
	}
}
