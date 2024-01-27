using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickItem : MonoBehaviour
{
	static public List<ClickItem> allClickItems = new List<ClickItem>();

	private void OnEnable() => allClickItems.Add(this);
	private void OnDisable() => allClickItems.Remove(this);

	public bool onHover = false;
	public virtual void OnHoverEnter() {
		if (!onHover) {
			onHover = true;
			transform.localScale *= 1.1f;
		}
	}
	public virtual void OnHoverExit() {
		if (onHover) {
			onHover = false;
			transform.localScale /= 1.1f;
		}
	}

	public virtual bool CheckClick() {
		return true;
	}

	public virtual void OnClick() {
		Debug.Log("Click");
	}
}
