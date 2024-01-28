using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NodeCanvas.DialogueTrees;


public class GGJTestMgr : SingletonMono<GGJTestMgr>
{
    public string textChatBoxContent = "²âÊÔ²âÊÔ²âÊÔ";
    public DialogueTreeController controller;
    public DialogueTree testDialogueTree;
    public DialogueActorAsset testActor;
}

#if UNITY_EDITOR
[CustomEditor(typeof(GGJTestMgr))] // ½«YourComponentÌæ»»ÎªÄãµÄ½Å±¾Ãû³Æ
public class GGJTestMgrEditor : Editor
{
    public override void OnInspectorGUI() {
        GGJTestMgr testMgr = GGJTestMgr.Instance;

        DrawDefaultInspector();

        if (EditorApplication.isPlaying) {
            EditorGUILayout.LabelField("Play Mode Buttons");

            if (GUILayout.Button("TestDialogue")) {
                testMgr.controller.StartDialogue();
                // ChatMgr.Instance.ShowText(testMgr.textChatBoxContent, );
            }

            if (GUILayout.Button("HideChatBox")) {
                ChatMgr.Instance.HideChatBox();
            }

            EditorGUILayout.LabelField("All DragItem");
            EditorGUILayout.BeginScrollView(new Vector2(0, 200), GUILayout.Height(100));
            foreach(var item in DragItem.allDragItems) {
                EditorGUILayout.ObjectField(item.name, item, typeof(DragItem), true);
            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.LabelField("All SlotItem");
            EditorGUILayout.BeginScrollView(new Vector2(0, 200), GUILayout.Height(100));
            foreach (var item in SlotItem.allSlotItems) {
                EditorGUILayout.ObjectField(item.name, item, typeof(SlotItem), true);
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif
