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

	public virtual void OnHoverEnter() {
		transform.localScale *= 1.1f;
	}
	public virtual void OnHoverExit() {
		transform.localScale /= 1.1f;
	}

	public virtual bool CheckAcceptDragItem(DragItem item) {
		return true;
	}
	public virtual void AcceptDragItem(DragItem item) {
		Destroy(item.gameObject);
	}
}
