using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI.Core;

public class GGJGameMgr : SingletonMono<GGJGameMgr>
{
	public TypewriterCore typeWriter;
	public GameObject chatBox;

	public void ShowText(string text) {
		chatBox.SetActive(true);
		typeWriter.ShowText(text);
	}

	public void HideChatBox() {
		chatBox.SetActive(false);
	}
}
