using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI.Core;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

public class Customer : SlotItem
{
	public PotionMaterial[] mats;
    // 优先使用方法1
	public string[] targetSentence; // 通关检测方法1：考虑放入素材的顺序，丢进锅里的素材构成的咒语和配置的咒语相同
    public PotionMaterial[] targetMats; // 通关检测方法2：不考虑放入素材的顺序，丢进锅里的素材和配置的素材相同就可以

	protected override void Awake() {
		base.Awake();
		EasyEvent.RegisterOnceCallback("ShowMats", ShowMats);
	}

	private void Start() {
		GetComponent<DialogueTreeController>().StartDialogue(GetComponent<DialogueActor>());
	}

	protected virtual void ShowMats() {
		foreach (var mat in mats) {
			Instantiate(mat);
		}
	}

	public override bool CheckAcceptDragItem(DragItem item) {
		return item is Bottle && (item as Bottle).result != "";
	}

	public override void AcceptDragItem(DragItem item) {
		Bottle bottle = item as Bottle;

		(GetComponent<DialogueTreeController>().blackboard as Blackboard).AddVariable<string>("branchName", "小丑");

		Destroy(item.gameObject);

		EasyEvent.TriggerEvent("PotionFinish");
	}
}
