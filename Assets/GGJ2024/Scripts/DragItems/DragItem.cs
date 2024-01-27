using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
	static public List<DragItem> allDragItems = new List<DragItem>();
	private void OnEnable() => allDragItems.Add(this);
	private void OnDisable() => allDragItems.Remove(this);

	public virtual void OnHoverEnter() {
		transform.localScale *= 1.1f;
	}
	public virtual void OnHoverExit() {
		transform.localScale /= 1.1f;
	}

	private bool attached = false;
	public void SetAttachToMouse(bool isAttach) {
		attached = isAttach;
	}

	protected virtual void Update() {
		if (attached) {
			transform.position = DragMgr.Instance.GetMousePos();
		}
	}
}
