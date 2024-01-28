using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRetry : ClickItem
{
	public override void OnHoverEnter() {
		if (!onHover) {
			onHover = true;
			transform.localScale *= 1.05f;
		}
	}
	public override void OnHoverExit() {
		if (onHover) {
			onHover = false;
			transform.localScale /= 1.05f;
		}
	}

	public override void OnClick() {
		GGJGameMgr.Instance.HandleRestartButton();
	}
}
