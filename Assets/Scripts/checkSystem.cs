using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceMaster;

public class checkSystem : MonoBehaviour {

	public int checkEvent; // 目前的事件是哪一類？ 0 = battle / 1 = social / 2 = explore
	public Character actioningCharacter; // 目前行動中的角色是哪隻？
	public Barrier aimedBarrier; // 目前要判定的對象
	private Skill usingSkill; // 目前使用的技能？
	private bool usedSkill = false; // 有沒有使用技能？

	private Dice[] dice; // 從角色中取得的骰子會放在這裡
	private int finalCheckValue = 0; // 最終用來做判定的值
	private int[] checkValue; // 每個骰子擲出來的結果放在這裡
	private int cvIndex = 0; // index for check value array

	private bool isReadyToRoll = false; // 可以開始擲了
	private bool isRolled = false; // 已經擲了

	// 設定這個check system適用於哪個事件的
	public void setCheckEvent(int ce){
		checkEvent = ce;
	}

	// 設定目前行動的角色，戰鬥事件應該有個排隊系統來設定目前行動的角色，其他事件則應該有地方讓玩家自己選擇要由哪個角色來行動。
	public void setCharacter(Character ac){
		actioningCharacter = ac;
	}

	// 玩家挑選的角色技能。
	public void setSkill(Skill us){
		usingSkill = us;
		usedSkill = true;
	}

	public void setBarrier(Barrier ab){
		aimedBarrier = ab;
	}

	// 從角色取得其身上裝備的骰子，並將其生成到畫面上。 什麼事件就取什麼骰子。
	public void setToRoll(){
		Dice[] d = actioningCharacter.getDice (checkEvent);
		dice = new Dice[d.Length];
		for (int i = 0; i < dice.Length; i++) {
			dice[i] = GameObject.Instantiate(d[i],  new Vector3(Random.Range(-1.3f, 1.3f), 0.5f, Random.Range(-3f, -1f)), Quaternion.identity) as Dice;
			dice[i].onShowNumber.AddListener(RegisterNumber);
		}
		checkValue = new int[dice.Length];
		finalCheckValue = 0;
		isReadyToRoll = true;
	}

	public void destroyAllDice(){
		for (int i = 0; i < dice.Length; i++) {
			Destroy (dice[i].gameObject);
		}
	}

	// 取得最終累計的數值，丟給目標去判定是否結果。
	public void check(){
		if (usedSkill) {
			finalCheckValue = usingSkill.skillWeight (checkValue);
		} else {
			for(int i=0; i<checkValue.Length; i++)
				finalCheckValue += checkValue[i];
		}
		aimedBarrier.check (finalCheckValue);
		Debug.Log("Total " + finalCheckValue);
	}

	// 骰子停止後callback其值，並存在check value陣列中，爾後可以傳給Skill做加權修正。
	public void RegisterNumber(int number)
	{
		if (isRolled) {
			Debug.Log("Got " + number);

			if (cvIndex < checkValue.Length - 1) {
				checkValue [cvIndex] = number;
				cvIndex++;
			} else if (cvIndex == checkValue.Length - 1) {
				checkValue [cvIndex] = number;
				isRolled = false;
				check ();
				cvIndex = 0;
			} else {
				isRolled = false;
				cvIndex = 0;
			}
		}
	}

	// 按下滑鼠後才讀取數值，避免一開始生成骰子時因碰撞而取得錯誤的數值。
	void Update()
	{
		if (isReadyToRoll) {
			if (Input.GetKeyDown (KeyCode.R) || Input.GetMouseButtonDown (0)) {
				if (Input.mousePosition.x > 0 && Input.mousePosition.x < 750 && Input.mousePosition.y > 20 && Input.mousePosition.y < 570) {
					isRolled = true;
					isReadyToRoll = false;
				}
			}
		}
	}
}
