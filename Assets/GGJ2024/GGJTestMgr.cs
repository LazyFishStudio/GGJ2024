using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class GGJTestMgr : SingletonMono<GGJTestMgr>
{
    public string textChatBoxContent = "���Բ��Բ���";
}

[CustomEditor(typeof(GGJTestMgr))] // ��YourComponent�滻Ϊ��Ľű�����
public class CustomInspector : Editor
{
    public override void OnInspectorGUI() {
        GGJTestMgr testMgr = GGJTestMgr.Instance;

        DrawDefaultInspector();

        if (EditorApplication.isPlaying) {
            EditorGUILayout.LabelField("Play Mode Buttons");

            if (GUILayout.Button("TestShowText")) {
                GGJGameMgr.Instance.ShowText(testMgr.textChatBoxContent);
            }

            if (GUILayout.Button("HideChatBox")) {
                GGJGameMgr.Instance.HideChatBox();
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
