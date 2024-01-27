using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;
using NodeCanvas;

public class SlotItem : MonoBehaviour
{
	static public List<SlotItem> allSlotItems = new List<SlotItem>();

	private void OnEnable() => allSlotItems.Add(this);
	private void OnDisable() => allSlotItems.Remove(this);

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

	public virtual bool CheckAcceptDragItem(DragItem item) {
		return true;
	}
	public virtual void AcceptDragItem(DragItem item) {
		Destroy(item.gameObject);
	}
}
