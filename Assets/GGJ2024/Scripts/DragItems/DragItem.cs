using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
	static public List<DragItem> allDragItems = new List<DragItem>();
	private void OnEnable() => allDragItems.Add(this);
	private void OnDisable() => allDragItems.Remove(this);

	private SpriteRenderer render;
	protected virtual void Awake() {
		render = GetComponent<SpriteRenderer>();
		if (render == null)
			render = GetComponentInChildren<SpriteRenderer>();
	}

	public virtual void OnHoverEnter() {
		if (render != null)
			render.color = Color.red;
	}
	public virtual void OnHoverExit() {
		if (render != null)
			render.color = Color.white;
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
