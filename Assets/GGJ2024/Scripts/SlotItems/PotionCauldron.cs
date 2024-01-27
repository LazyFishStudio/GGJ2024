using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class PotionCauldron : SlotItem {
    public TextMeshProUGUI textMesh;

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

    public override bool CheckAcceptDragItem(DragItem item) {
        if (item is PotionMaterial)
            return true;
        if (item is Bottle && (item as Bottle).result == "")
            return true;
        return false;
    }

    public override void AcceptDragItem(DragItem item) {
        Bottle bottle = item as Bottle;
        PotionMaterial potionMat = item as PotionMaterial;
        if (bottle != null) {
            bottle.result = sentence;
            ClearMaterial();
        } else if (potionMat != null) {
            AddMaterial(potionMat);
            potionMat.gameObject.SetActive(false);
        }
    }

    private void Update() {
        textMesh.text = sentence;
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
