using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickExit : ClickItem
{
	public GameObject hoverObj;

	public override void OnHoverEnter() {
		if (!onHover) {
			onHover = true;
			hoverObj.SetActive(true);
		}
	}
	public override void OnHoverExit() {
		if (onHover) {
			onHover = false;
			hoverObj.SetActive(false);
		}
	}

	public override void OnClick() {
		Application.Quit();
	}
}
