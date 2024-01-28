using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI.Core;
using NodeCanvas.DialogueTrees;
using System.Linq;
using Bros.UI2D;
using TMPro;

/// <summary>
/// 流程：
///     1.获取顾客
///     2.顾客进门（关卡开始）
///     3.玩家制作药水
///     4.等待玩家提交药水
///     5.判断药水是否符合条件
///     6.顾客离开
///     7.获取新顾客（下一关）
/// </summary>
public partial class GGJGameMgr : SingletonMono<GGJGameMgr>
{
	public Tooltip tooltip;
	public PotionCauldron pot;
	public GameObject itemBox;
	public TextMeshProUGUI itemName;
	public TextMeshProUGUI itemDesc;
	public Transform customerHandle;

	public GameObject customEffect;


	private void Awake() {
		InitGameFlowSM();
		RegisterGameFlows();
	}

	private void Start() {
		customerIndex = -1;
		HandleNextLevel();
	}

	private void Update() {
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
	}

	public enum GameFlowState {
		Idle, // 游戏流程未开始时
		CustomerEnter, // 获取顾客，播放进门动画
		MakingPotion, // 玩家制作药水，提交药水，药水的结果检测
		CustomerLaughing // 玩家满足了顾客，播放顾客的演出和对话，然后顾客离开
	}

	[Space]
	[Header("Game Flow")]
	public StateMachine<GameFlowState> gameFlowSM;
	public int maxMatNumInCauldron = 3;

	// 顾客顺序，关卡顺序
	public GameObject[] customerPrefabs;
	public Customer curCustomer;
	public int customerIndex;

	private void InitGameFlowSM() {
		gameFlowSM = new StateMachine<GameFlowState>(GameFlowState.Idle);

		gameFlowSM.GetState(GameFlowState.CustomerEnter).Bind(
			onEnter: () => {
				curCustomer = CreateAndGetNextCustomer();
			},
			onUpdate: () => {
				// wait animation finish
			},
			onExit: () => {

			}
		);

		gameFlowSM.GetState(GameFlowState.MakingPotion).Bind(
			onEnter: () => {

			},
			onUpdate: () => {
				// wait trigger potion sold
			},
			onExit: () => {

			}
		);

		gameFlowSM.GetState(GameFlowState.CustomerLaughing).Bind(
			onEnter: () => {
				// 播放顾客 Laughing 动画
				// 播放剧情
				// 播放顾客离开动画
			},
			onUpdate: () => {
				// 等待动画播放完毕后进入下一个关卡
			},
			onExit: () => {

			}
		);

		gameFlowSM.Init();
	}

	public Customer CreateAndGetNextCustomer() {

		if (customerIndex >= customerPrefabs.Length) {
			// 游戏结束
			Debug.Log("all customer pass game finish");
			return null;
		}

		customerIndex++;
		GameObject customerObj = GameObject.Instantiate(customerPrefabs[customerIndex], customerHandle.position, Quaternion.identity, customerHandle);
		Customer customer = customerObj.GetComponent<Customer>();

		return customer;
	}

	public void ClearMatsAndPotions() {
		pot.ClearMaterial();
		List<GameObject> pendingDelete = new List<GameObject>();
		foreach (var mat in DragItem.allDragItems) {
			if (mat is PotionMaterial || mat is Bottle) {
				pendingDelete.Add(mat.gameObject);
			}
		}
		foreach (var mat in pendingDelete) {
			Destroy(mat.gameObject);
		}
	}

	private void ClearCurrentLevel() {
		ClearMatsAndPotions();
		if (curCustomer != null)
			Destroy(curCustomer.gameObject);
		curCustomer = null;
	}

	private Customer CreateCurrentCustom() {
		GameObject customerObj = GameObject.Instantiate(customerPrefabs[customerIndex], customerHandle.position, Quaternion.identity, customerHandle);
		return customerObj.GetComponent<Customer>();
	}
}

public partial class GGJGameMgr : SingletonMono<GGJGameMgr>
{
	private void RegisterGameFlows() {
		EasyEvent.RegisterCallback("NextLevel", HandleNextLevel);
		EasyEvent.RegisterCallback("RestartLevel", HandleRestartLevel);
		EasyEvent.RegisterCallback("GameFinish", HandleGameFinish);
		EasyEvent.RegisterCallback("ShowMats", HandleShowMats);
		EasyEvent.RegisterCallback("EndingA", HandleEndingA);
		EasyEvent.RegisterCallback("EndingB", HandleEndingB);
		EasyEvent.RegisterCallback("EndingC", HandleEndingC);
		EasyEvent.RegisterCallback("EndingD", HandleEndingD);
		EasyEvent.RegisterCallback("EndingE", HandleEndingE);
		EasyEvent.RegisterCallback("EndingF", HandleEndingF);
	}

	public void HandlePause() {

	}

	public void HandleGameFinish() {

	}

	public void HandleExitGame() {
		Application.Quit();
	}

	public void HandleNextLevel() {
		ClearCurrentLevel();
		ClearAchievements();

		customerIndex++;
		curCustomer = CreateCurrentCustom();
		RegisterAchievements();
	}

	public void HandleRestartButton() {
		restartButton.SetActive(false);
		curCustomer = CreateCurrentCustom();
	}

	public GameObject restartButton;
	public void HandleRestartLevel() {
		ClearCurrentLevel();
		restartButton.SetActive(true);
	}

	public void HandleShowMats() {
		foreach (var mat in curCustomer.mats) {
			Instantiate(mat);
		}
	}

	public void HandleEndingA() {
		curCustomer.HandleEndings("EndingA");
	}
	public void HandleEndingB() {
		curCustomer.HandleEndings("EndingB");
	}
	public void HandleEndingC() {
		curCustomer.HandleEndings("EndingC");
	}
	public void HandleEndingD() {
		curCustomer.HandleEndings("EndingD");
	}
	public void HandleEndingE() {
		curCustomer.HandleEndings("EndingE");
	}
	public void HandleEndingF() {
		curCustomer.HandleEndings("EndingF");
	}

	public void RegisterAchievements() {
		foreach (var item in curCustomer.achievements) {
			Achievement achievement = Instantiate(item).GetComponent<Achievement>();
			EasyEvent.RegisterCallback(string.Format("Achievement[{0}]", achievement.achieveName), achievement.UnlockAchievement);
		}
	}

	public void ClearAchievements() {
		foreach (var achievement in FindObjectsOfType<Achievement>()) {
			if (achievement.childSprite != null)
				Destroy(achievement.childSprite.gameObject);

			Destroy(achievement.gameObject);
		}
	}
}