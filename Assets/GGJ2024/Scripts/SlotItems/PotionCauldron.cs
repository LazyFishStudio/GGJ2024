using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using Unity.VisualScripting;

public class PotionCauldron : SlotItem {
    public TextMeshProUGUI textMesh;

	public string sentence;
	public List<PotionMaterial> matList;
	public int maxMatNum = 3; 
    [HideInInspector]
    public PotionMaterial testMat;
    public Color waterColor;
    public SpriteRenderer water;
    public FMODUnity.StudioEventEmitter popEmitter;

    private void Awake() {
        textMesh.text = "";
    }

	public void AddMaterial(PotionMaterial mat) {
		if (matList == null) {
			matList = new();
		}
		if (matList.Count >= maxMatNum || mat == null) {
			return;
		}
		sentence = sentence + mat.word + " ";
		matList.Add(mat);
        textMesh.text = "<wave>" + sentence + "</wave>";
        water.color = Color.HSVToRGB(Random.Range(0, 1.0f), 1.0f, 1.0f);
        popEmitter.Play();
	}

    public void ClearMaterial() {
        sentence = "";
        textMesh.text = "";
        matList.Clear();
        // water.color = waterColor;
    }

    public override bool CheckAcceptDragItem(DragItem item) {
        if (item is PotionMaterial)
            return true;
        return false;
    }

    public override void AcceptDragItem(DragItem item) {
        PotionMaterial potionMat = item as PotionMaterial;
        if (potionMat != null) {
            AddMaterial(potionMat);
            potionMat.gameObject.SetActive(false);
        }
    }

    private void Update() {
        
    }
}

#if UNITY_EDITOR
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
#endif
