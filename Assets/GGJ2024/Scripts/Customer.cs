using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI.Core;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using DG.Tweening;

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

	private void Awake() {
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        spriteRend.sprite = characterSprites[characterSprites.Length - 1];
		branchDict = new Dictionary<string, string>();

		int length = targetSentences.Count;
		for (int idx = 0; idx < length; idx++) {
			branchDict[targetSentences[idx]] = targetBranchName[idx];
		}
	}

	private string GetBranchName(string sentence) {
		if (branchDict.TryGetValue(sentence, out string result)) {
			return result;
		}
		return defaultBranchName;
	}

	private void Start() {
		GetComponent<DialogueTreeController>().StartDialogue(GetComponent<DialogueActor>());
	}

	public void HandleEndings(string endingName) {
		GGJGameMgr.Instance.customEffect.SetActive(false);
		GGJGameMgr.Instance.customEffect.SetActive(true);

		Debug.Log(endingName);

		transform.DOMove(transform.position, 0.16666f).OnComplete(() => {
			switch (endingName) {
				case "EndingA": {
					spriteRend.sprite = characterSprites[0];
					break;
				}
				case "EndingB": {
					spriteRend.sprite = characterSprites[1];
					break;
				}
				case "EndingC": {
					spriteRend.sprite = characterSprites[2];
					break;
				}
				case "EndingD": {
					spriteRend.sprite = characterSprites[3];
					break;
				}
				case "EndingE": {
					spriteRend.sprite = characterSprites[4];
					break;
				}
				case "EndingF": {
					spriteRend.sprite = characterSprites[5];
					break;
				}
			}
			transform.DOMove(transform.position, 0.16666f).OnComplete(() => {
				EasyEvent.TriggerEvent("EndingFinish");
			});
		});
	}

	public override bool CheckAcceptDragItem(DragItem item) {
		return item is Bottle;
	}

	public override void AcceptDragItem(DragItem item) {
		Bottle bottle = item as Bottle;

		string branchName = GetBranchName(bottle.result);
		(GetComponent<DialogueTreeController>().blackboard as Blackboard).AddVariable<string>("branchName", branchName);

		Destroy(item.gameObject);
		GGJGameMgr.Instance.ClearMatsAndPotions();

		EasyEvent.TriggerEvent("PotionFinish");
	}
}
