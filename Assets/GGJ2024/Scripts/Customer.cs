using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI.Core;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

public class Customer : SlotItem
{
	public PotionMaterial[] mats;
	public List<GameObject> achievements;

	public string defaultBranchName = "Failed";
	public List<string> targetSentences;
	public List<string> targetBranchName;

    public Sprite[] characterSprites;
    private SpriteRenderer spriteRend;

	private Dictionary<string, string> branchDict;

	private void OnDestroy() {
		foreach (var item in FindObjectsOfType<Achievement>())
			Destroy(item.gameObject);
	}

	private void Awake() {

        spriteRend = GetComponentInChildren<SpriteRenderer>();
        spriteRend.sprite = characterSprites[2];
		branchDict = new Dictionary<string, string>();

		int length = targetSentences.Count;
		for (int idx = 0; idx < length; idx++) {
			Debug.Log("[" + targetSentences[idx] + "] : " + targetBranchName[idx]);
			branchDict[targetSentences[idx]] = targetBranchName[idx];
		}

		EasyEvent.RegisterOnceCallback("ShowMats", ShowMats);
		EasyEvent.RegisterOnceCallback("NextLevel", () => { GGJGameMgr.Instance.NextLevel(); });
		EasyEvent.RegisterOnceCallback("RestartLevel", () => { GGJGameMgr.Instance.RestartLevel(); });
	}

	private string GetBranchName(string sentence) {
		Debug.Log("[" + sentence + "]");
		if (branchDict.TryGetValue(sentence, out string result)) {
			return result;
		}
		return defaultBranchName;
	}

	private void Start() {
		foreach (var achievement in achievements) {
			Instantiate(achievement);
		}
		GetComponent<DialogueTreeController>().StartDialogue(GetComponent<DialogueActor>());
	}

    private void Update() {
        EasyEvent.RegisterCallback("EndingA", () => spriteRend.sprite = characterSprites[0]);
        EasyEvent.RegisterCallback("EndingB", () => spriteRend.sprite = characterSprites[1]);
        EasyEvent.RegisterCallback("EndingC", () => spriteRend.sprite = characterSprites[2]);
    }

	protected virtual void ShowMats() {
		foreach (var mat in mats) {
			Instantiate(mat);
		}
	}

	public override bool CheckAcceptDragItem(DragItem item) {
		return item is Bottle;
	}

	public override void AcceptDragItem(DragItem item) {
		Bottle bottle = item as Bottle;

		string branchName = GetBranchName(bottle.result);
		(GetComponent<DialogueTreeController>().blackboard as Blackboard).AddVariable<string>("branchName", branchName);

		Destroy(item.gameObject);

		EasyEvent.TriggerEvent("PotionFinish");
	}
}
