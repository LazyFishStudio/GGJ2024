using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI.Core;
using NodeCanvas.DialogueTrees;

public class Customer : MonoBehaviour
{
	public string customerName = "some body";
	public DialogueActor actor;
    public DialogueTree dialogueTree;
	public PotionMaterial[] mats;
    // 优先使用方法1
	public string[] targetSentence; // 通关检测方法1：考虑放入素材的顺序，丢进锅里的素材构成的咒语和配置的咒语相同
    public PotionMaterial[] targetMats; // 通关检测方法2：不考虑放入素材的顺序，丢进锅里的素材和配置的素材相同就可以
}
