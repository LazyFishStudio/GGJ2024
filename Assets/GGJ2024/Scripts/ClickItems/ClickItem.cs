using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickItem : MonoBehaviour
{
	static public List<ClickItem> allClickItems = new List<ClickItem>();

	private void OnEnable() => allClickItems.Add(this);
	private void OnDisable() => allClickItems.Remove(this);

	public virtual void OnHoverEnter() {
		transform.localScale *= 1.2f;
	}
	public virtual void OnHoverExit() {
		transform.localScale /= 1.2f;
	}

	public virtual void OnClick() {
		Debug.Log("Click");
	}
}
