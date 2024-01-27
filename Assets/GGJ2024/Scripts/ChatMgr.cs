using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI.Core;
using NodeCanvas.DialogueTrees;
using TMPro;

public class ChatMgr : SingletonMono<ChatMgr>
{
	public GameObject chatBox;
	public GameObject continueText;
	public TextMeshProUGUI nameText;
	public TypewriterCore typeWriter;

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
	}

	public void ShowText(string actorName, string text, System.Action continueCallback) {
		curCallback = continueCallback;

		nameText.text = actorName;
		chatBox.SetActive(true);
		continueText.SetActive(false);
		typeWriter.ShowText(text);
	}

	private System.Action curCallback = null;
	private void OnTextShowed() {
		continueText.SetActive(true);
		EasyEvent.RegisterOnceCallback("ChatContinue", curCallback);
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
