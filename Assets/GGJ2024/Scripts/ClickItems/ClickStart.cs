using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickStart : ClickItem
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
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
