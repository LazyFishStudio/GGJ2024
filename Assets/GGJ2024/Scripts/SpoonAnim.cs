using UnityEngine;

public class SpoonAnim : MonoBehaviour
{
    public GameObject expPrefab;
    public float stirSpeed = 1.0f;
    public float scaleY = 0.5f;
    public float stirRoundProcess;
    public Vector3 roundPos;
    public Transform spoon;
    private float moveTime;
    private float moveTimeCount;
    private bool isMoving;
    private float moveProcess;

    void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
            StartMove(2.0f);
        }
        if (isMoving) {
            moveTimeCount += Time.deltaTime;
            if (moveTimeCount >= moveTime) {
                isMoving = false;
                GameObject.Instantiate(expPrefab, transform.parent.position, Quaternion.identity);
                EasyEvent.TriggerEvent("SpoonAnimFinish");
            }
            moveProcess = Mathf.Clamp01(moveTimeCount / moveTime);
            stirRoundProcess = (stirRoundProcess + stirSpeed * Time.deltaTime * moveProcess) % 1.0f;
            roundPos = new Vector3(Mathf.Cos(stirRoundProcess * 2.0f * Mathf.PI), Mathf.Sin(stirRoundProcess * 2.0f * Mathf.PI) * scaleY, 0);
            spoon.position = transform.position + roundPos * transform.localScale.x;
        }
    }

    public void StartMove(float moveTime) {
        this.moveTime = moveTime;
        moveTimeCount = 0;
        isMoving = true;
    }

    public void StopMove() {
        isMoving = false;
    }

    void OnDrawGizmos() {

        Gizmos.color = Color.white;
        Vector3 lastSnoopPos = Vector3.zero;
        for (int i = 0; i < 24; i++) {
            Vector3 tRoundPos = new Vector3(Mathf.Cos((i / 24.0f) * 2.0f * Mathf.PI), Mathf.Sin((i / 24.0f) * 2.0f * Mathf.PI) * scaleY, 0);
            Vector3 tSnoopPos = transform.position + tRoundPos * transform.localScale.x;
            if (i > 0) {
                Gizmos.DrawLine(lastSnoopPos, tSnoopPos);
            }
            lastSnoopPos = tSnoopPos;
        }
    }

}
