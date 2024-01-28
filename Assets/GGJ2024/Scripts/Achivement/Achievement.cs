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

    public Transform childSprite;

    private bool unlocked = false;
    public void UnlockAchievement() {
        Debug.Log("UnlockAchievement");
        if (!unlocked) {
            unlocked = true;

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
        } else {
            transform.DOMove(transform.position, 0.05f).OnComplete(() => {
                EasyEvent.TriggerEvent("AchievementFinish");
            });
        }
    }
}

#if UNITY_EDITOR
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
#endif
