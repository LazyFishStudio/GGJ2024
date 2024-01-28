using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class Achievement : MonoBehaviour
{
    public string achieveName;

    public GameObject placeholder;
    public GameObject achievePrefab;
    public Sprite achieveSprite;
    public float achieveStartScale;
    public float achieveBiggScale;
    public float achieveNormalScale;
    public float scaleTime = 0.5f;
    public float stopTime = 0.5f;
    public float moveTime = 0.5f;

    private Transform childSprite;
    private void OnDestory() {
        if (childSprite != null)
            Destroy(childSprite.gameObject);
	}

    public void UnlockAchievement() {
        Transform achievement = Instantiate(achievePrefab).transform;
        achievement.GetComponent<SpriteRenderer>().sprite = achieveSprite;
        achievement.localScale = Vector3.one * achieveStartScale;
        childSprite = achievement;

        achievement.DOScale(Vector3.one * achieveBiggScale, scaleTime).OnComplete(() => {
            achievement.DOScale(Vector3.one * achieveBiggScale, stopTime).OnComplete(() => {
                placeholder.SetActive(false);
                achievement.DOScale(Vector3.one * achieveNormalScale, moveTime);
                achievement.DOMove(placeholder.transform.position, moveTime).OnComplete(() => {
                    EasyEvent.TriggerEvent("AchievementFinish");
                });
            });
        });
    }
}

[CustomEditor(typeof(Achievement))]
public class AchievementEditor : Editor
{
    public override void OnInspectorGUI() {
        Achievement achievement = (Achievement)target;

        DrawDefaultInspector();

        if (EditorApplication.isPlaying) {
            EditorGUILayout.LabelField("Play Mode Buttons");

            if (GUILayout.Button("TestAchievement")) {
                achievement.UnlockAchievement();
            }
        }
    }
}

