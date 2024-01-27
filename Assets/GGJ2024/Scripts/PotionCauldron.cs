using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PotionCauldron : MonoBehaviour {
	public string sentence;
	public List<PotionMaterial> matList;
	public int maxMatNum = 3; 
    [HideInInspector]
    public PotionMaterial testMat;

	public void AddMaterial(PotionMaterial mat) {
		if (matList == null) {
			matList = new();
		}
		if (matList.Count >= maxMatNum || mat == null) {
			return;
		}
		sentence = sentence + mat.word;
		matList.Add(mat);
	}

    public void ClearMaterial() {
        sentence = "";
        matList.Clear();
    }

}

[CustomEditor(typeof(PotionCauldron))]
public class PotionCauldronEditor : Editor {

    public override void OnInspectorGUI() {
        
        DrawDefaultInspector();

        PotionCauldron cauldron = ((PotionCauldron)target);
        if (EditorApplication.isPlaying) {
            cauldron.testMat = (PotionMaterial)EditorGUILayout.ObjectField("TestMat", cauldron.testMat, typeof(PotionMaterial), true);
            if (GUILayout.Button("Test Add TestMat Into Cauldorn")) {
                cauldron.AddMaterial(cauldron.testMat);
            }
            if (GUILayout.Button("Test Clear All Mat In Cauldorn")) {
                cauldron.ClearMaterial();
            }
        }

    }

}
