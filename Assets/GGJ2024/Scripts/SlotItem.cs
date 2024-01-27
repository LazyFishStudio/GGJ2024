using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
	static public List<SlotItem> allSlotItems = new List<SlotItem>();

	private void OnEnable() => allSlotItems.Add(this);
	private void OnDisable() => allSlotItems.Remove(this);

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

	public virtual bool CheckAcceptDragItem(DragItem item) {
		return true;
	}
	public virtual void AcceptDragItem(DragItem item) {
		Destroy(item.gameObject);
	}
}
