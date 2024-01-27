using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
	static public List<DragItem> allDragItems = new List<DragItem>();
	private void OnEnable() => allDragItems.Add(this);
	private void OnDisable() => allDragItems.Remove(this);

	private SpriteRenderer render;
	private void Awake() {
		render = GetComponent<SpriteRenderer>();
	}

	public void OnHoverEnter() {
		render.color = Color.red;
	}
	public void OnHoverExit() {
		render.color = Color.white;
	}

	private bool attached = false;
	public void SetAttachToMouse(bool isAttach) {
		attached = isAttach;
	}

	private void Update() {
		if (attached) {
			transform.position = DragMgr.Instance.GetMousePos();
		}
	}
}
