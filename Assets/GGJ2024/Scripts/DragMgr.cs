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
        GGJGameMgr.Instance.tooltip.SetCornerToPos(Bros.UI2D.Tooltip.CornerOption.LeftBottom, DragMgr.Instance.GetMousePos());
    }

    public DragItem dragItem;
    private DragItem focusDragItem;
    private SlotItem focusSlotItem;
    private ClickItem focusClickItem;

    private void Awake() {
        sm = new StateMachine<State>(State.Idle);

        sm.GetState(State.Idle).Bind(
            onEnter: () => UpdateFocusDragItem(),
            onUpdate: () => {
                UpdateFocusDragItem();
                UpdateFocusClickItem();
                if (focusDragItem != null && Input.GetKeyDown(KeyCode.Mouse0)) {
                    dragItem = focusDragItem;
                    sm.GotoState(State.Drag);
                    return;
				}
                if (focusClickItem != null && Input.GetKeyDown(KeyCode.Mouse0)) {
                    focusClickItem?.OnHoverExit();
                    focusClickItem.OnClick();
                }
            },
            onExit: () => { focusDragItem?.OnHoverExit(); focusDragItem = null; }
        );
        sm.GetState(State.Drag).Bind(
            onEnter: () => {
                if (dragItem is PotionMaterial) {
                    dragItem = Instantiate(dragItem.gameObject, mousePos, Quaternion.identity, null).GetComponent<DragItem>();
                    dragItem.SetAttachToMouse(true);
                } else {
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
            onExit: () => { focusSlotItem?.OnHoverExit(); focusSlotItem = null; }
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

    private void UpdateFocusClickItem() {
        ClickItem newFocusItem = null;
        foreach (var item in ClickItem.allClickItems) {
            Collider2D collider = item.GetComponent<Collider2D>();
            if (collider.OverlapPoint(mousePos)) {
                newFocusItem = item;
            }
        }

        if (newFocusItem != focusClickItem) {
            focusClickItem?.OnHoverExit();
            focusClickItem = newFocusItem;
            focusClickItem?.OnHoverEnter();
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
