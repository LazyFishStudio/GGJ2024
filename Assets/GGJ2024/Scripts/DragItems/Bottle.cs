using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bottle : DragItem
{
    public string result = "";

	public override void OnHoverEnter() {
		transform.localScale *= 1.1f;

		GGJGameMgr.Instance.tooltip.gameObject.SetActive(true);
		GGJGameMgr.Instance.tooltip.SetupText(result);
	}
	public override void OnHoverExit() {
		transform.localScale /= 1.1f;

		GGJGameMgr.Instance.tooltip.gameObject.SetActive(false);
	}
}
