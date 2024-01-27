using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMgr : SingletonMono<DragMgr>
{
    public enum State { Idle, Drag }
    public StateMachine<State> sm;

    public Vector3 mousePos;

    private int latestFrameCnt = 0;
    public Vector3 GetMousePos() {
        if (latestFrameCnt != Time.frameCount) {
            mousePos = Bros.Utils.UtilClass.GetMouseWorldPosition2D();
            latestFrameCnt = Time.frameCount;
        }
        return mousePos;
    }

    private void Update() {
        GetMousePos();
        sm.UpdateStateAction();
    }

    public DragItem dragItem;
    private DragItem focusDragItem;
    private SlotItem focusSlotItem;
    private void Awake() {
        sm = new StateMachine<State>(State.Idle);

        sm.GetState(State.Idle).Bind(
            onEnter: () => UpdateFocusDragItem(),
            onUpdate: () => {
                UpdateFocusDragItem();
                if (focusDragItem != null && Input.GetKeyDown(KeyCode.Mouse0)) {
                    sm.GotoState(State.Drag);
                    return;
				}
            },
            onExit: () => focusDragItem?.OnHoverExit()
        );
        sm.GetState(State.Drag).Bind(
            onEnter: () => {
                if (focusDragItem is PotionMaterial) {
                    dragItem = Instantiate(focusDragItem.gameObject, mousePos, Quaternion.identity, null).GetComponent<DragItem>();
                    dragItem.SetAttachToMouse(true);
                } else {
                    dragItem = focusDragItem;
                    dragItem.SetAttachToMouse(true);
                }
            },
            onUpdate: () => {
                UpdateFocusSlotItem();
                if (Input.GetKeyUp(KeyCode.Mouse0)) {
                    dragItem.SetAttachToMouse(false);

                    if (focusSlotItem != null) {
                        focusSlotItem.AcceptDragItem(dragItem);
                    } else if (dragItem is PotionMaterial) {
                        Destroy(dragItem.gameObject);
					}

                    sm.GotoState(State.Idle);
                    return;
                }
			},
            onExit: () => focusSlotItem?.OnHoverExit()
        );

        sm.Init();
	}

    private void UpdateFocusDragItem() {
        DragItem newFocusItem = null;
        foreach (var item in DragItem.allDragItems) {
            Collider2D collider = item.GetComponent<Collider2D>();
            if (collider.OverlapPoint(mousePos)) {
                newFocusItem = item;
            }
        }

        if (newFocusItem != focusDragItem) {
            focusDragItem?.OnHoverExit();
            focusDragItem = newFocusItem;
            focusDragItem?.OnHoverEnter();
        }
    }

    private void UpdateFocusSlotItem() {
        List<Collider2D> colliders = new List<Collider2D>();
        Collider2D dragCollider = dragItem.GetComponent<Collider2D>();
        dragCollider.OverlapCollider(new ContactFilter2D(), colliders);

        SlotItem newFocusItem = null;
        foreach (var collider in colliders) {
            SlotItem slotItem = collider.GetComponent<SlotItem>();
            if (slotItem == null || !slotItem.CheckAcceptDragItem(dragItem))
                continue;
            if (newFocusItem == null || newFocusItem.GetInstanceID() > slotItem.GetInstanceID())
                newFocusItem = slotItem;
        }

        if (newFocusItem != focusSlotItem) {
            focusSlotItem?.OnHoverExit();
            focusSlotItem = newFocusItem;
            focusSlotItem?.OnHoverEnter();
        }
    }
}
