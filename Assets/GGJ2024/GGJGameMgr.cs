using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI.Core;
using NodeCanvas.DialogueTrees;
using System.Linq;
using Bros.UI2D;

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
public class GGJGameMgr : SingletonMono<GGJGameMgr>
{
	public Tooltip tooltip;
	public PotionCauldron pot;

	private void Awake() {
		customerIndex = -1;
		InitGameFlowSM();
	}

	private void Start() {
		gameFlowSM.GotoState(GameFlowState.CustomerEnter);
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
		GameObject customerObj = GameObject.Instantiate(customerPrefabs[customerIndex]);
		Customer customer = customerObj.GetComponent<Customer>();

		return customer;
	}

	private void ClearCurrentLevel() {
		Destroy(curCustomer.gameObject);
		pot.ClearMaterial();

		List<PotionMaterial> pendingDelete = new List<PotionMaterial>();
		foreach (var mat in DragItem.allDragItems) {
			if (mat is PotionMaterial) {
				pendingDelete.Add(mat as PotionMaterial);
			}
		}
		foreach (var mat in pendingDelete) {
			Destroy(mat.gameObject);
		}
	}

	public void NextLevel() {
		ClearCurrentLevel();

		curCustomer = CreateAndGetNextCustomer();
	}

	public void RestartLevel() {
		ClearCurrentLevel();

		GameObject customerObj = GameObject.Instantiate(customerPrefabs[customerIndex]);
		curCustomer = customerObj.GetComponent<Customer>();
	}
}

