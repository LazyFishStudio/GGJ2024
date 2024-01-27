using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI.Core;
using NodeCanvas.DialogueTrees;
using TMPro;

public class ChatMgr : SingletonMono<ChatMgr>
{
	public GameObject chatBox;
	public TextMeshProUGUI nameText;
	public TypewriterCore typeWriter;

	private void Awake() {
		EasyEvent.RegisterCallback("HideChatBox", () => { chatBox.SetActive(false); });
	}

	private void OnEnable() {
		DialogueTree.OnSubtitlesRequest += OnSubtitlesRequest;
		DialogueTree.OnMultipleChoiceRequest += OnMultipleChoiceRequest;
		typeWriter.onTextShowed.AddListener(OnTextShowed);
	}
	private void OnDisable() {
		DialogueTree.OnSubtitlesRequest -= OnSubtitlesRequest;
		DialogueTree.OnMultipleChoiceRequest -= OnMultipleChoiceRequest;
		typeWriter.onTextShowed.RemoveListener(OnTextShowed);
	}

	public void HideChatBox() {
		chatBox.SetActive(false);
		nameText.text = "";
		typeWriter.GetComponent<TextMeshProUGUI>().text = "";
	}

	public void ShowText(string actorName, string text, System.Action continueCallback) {
		curCallback = continueCallback;

		chatBox.SetActive(true);

		nameText.text = actorName;
		typeWriter.ShowText(text);
	}

	private System.Action curCallback = null;
	private void OnTextShowed() {
		EasyEvent.RegisterOnceCallback("ChatContinue", () => { curCallback?.Invoke(); });
	}

	private void OnSubtitlesRequest(SubtitlesRequestInfo info) {
		ShowText(info.actor.name, info.statement.text, continueCallback: () => info.Continue());
	}

	private void OnMultipleChoiceRequest(MultipleChoiceRequestInfo info) {
		info.SelectOption(0);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			if (typeWriter.isShowingText) {
				typeWriter.SkipTypewriter();
			} else {
				EasyEvent.TriggerEvent("ChatContinue");
			}
		}
	}
}
