﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DiceMaster;
using UnityEngine.SceneManagement;


public class StateManager : MonoBehaviour {

	public State.BattleState currentState;

	public UIManager uiManager;
	public BattleCheckManager bcManager;
	public Monster monster;
	public GameObject monsterMark;

	private DataManager dataManager;
	private BattleUnit[] battleUnits = new BattleUnit[2]; // 將所有戰鬥單位排序，放在這裡
	private int currentUnitIndex = 0; // 指向目前可行動的戰鬥單位
	private Character currentCharacter; // 目前行動的角色

    // Use this for initialization
    void Start () {
		// Battle Scene一開始，就要找到所有角色，並存成array管理。 所有character都是don't destroy。
		dataManager = (DataManager)FindObjectOfType (typeof(DataManager));
		currentCharacter = dataManager.choosedHero;

		// 從怪物池中隨機產生怪物，目前先寫死。
		monster = GameObject.Instantiate (dataManager.choosedMonster, Vector3.zero, Quaternion.identity) as Monster;
		monster.transform.position = monsterMark.transform.position;
		monster.transform.rotation = monsterMark.transform.rotation;
		monster.gameObject.transform.parent = monsterMark.transform;

		// 將本場戰鬥參戰的角色和怪物告知UI Manager 
		uiManager.setDataManager(dataManager);
		uiManager.setCurrentCharacter (currentCharacter);
		uiManager.setMonster (monster);
		bcManager.setMonster (monster);

		// 目前先設成玩家先攻
		battleUnits [0] = (BattleUnit)currentCharacter;
		battleUnits [1] = (BattleUnit)monster;

		setState (State.BattleState.SelectBattleSkill);
	}

	// 設定階段: 目前State Manager自己、UI Magnager都會call這個來設定目前的階段
	public void setState(State.BattleState state)
	{
		currentState = state;
		uiManager.currentState = currentState; // 通知UI Manager目前是什麼階段，方便設定UI

		switch (currentState) {
		// 選擇技能階段
		case State.BattleState.SelectBattleSkill:

			// 如果現在可行動的戰鬥單位是怪物，則跳過選擇技能階段
			if (string.Equals (battleUnits [currentUnitIndex].type, "Monster"))
				setState (State.BattleState.EnemyRollBattleDice);
			else {
				// 如果角色死亡，則Game Over
				if (currentCharacter.Hp <= 0) {
					setState (State.BattleState.BattleEnd);
				}else {
					uiManager.showBattleSkillPanel (); // UI: 顯示技能選擇的操作面板
				}
			}
			break;

		case State.BattleState.PlayerRollBattleDice:
			bcManager.setCurrentCharacter (currentCharacter);
			bcManager.setForPlayerToRoll (1); // 1表示第一次擲骰子
			uiManager.showDiceRollingPanel ();
			break;

		case State.BattleState.EnemyRollBattleDice:
			bcManager.monsterRoll ();
			uiManager.showEnemyRollingPanel ();
			break;
		
		case State.BattleState.SelectGambleSkill:
			uiManager.showGambleSkillPanel ();
			break;

		case State.BattleState.PlayerRollBattleDice2:
            uiManager.showDiceRolling2Panel();
			bcManager.setForPlayerToRoll (2); // 2表示第二次擲骰子

			break;

		case State.BattleState.EnemyAttack:
            StartCoroutine(MonsterAttackRoutine());
            break;
		
		case State.BattleState.PlayerAttack:
            StartCoroutine(PlayerAttackRoutine());
			break;

		case State.BattleState.BattleEnd:
			currentCharacter.transform.parent = null;

			if(currentCharacter.Hp > 0)
				SceneManager.LoadScene("Loot", LoadSceneMode.Single);
			else
				Debug.Log ("SHOW BATTLE END!!!!"); // 顯示戰鬥結束的畫面
			break;
		
		default:
			Debug.Log ("setState - ErrorState");
			break;
		}
	}

    IEnumerator PlayerAttackRoutine()
    {
		uiManager.showPlayerAttack();
		bcManager.destroyAllDice(); // 清掉骰子模型

        yield return new WaitForSeconds(2);
        monster.gameObject.GetComponents<AudioSource>()[0].Play(0);
        uiManager.showMonsterHurt();
        yield return new WaitForSeconds(1.5f);
        // demo用
        if(monster.Hp <= 0)
        {
			// 提醒一下，換scene前一定要移除角色parent，不然就算有Don't Destroy，也因為父親被砍，而消失。
			// 只有戰敗死亡的時候，才不用管，反正都要重頭來過
			currentCharacter.transform.parent = null;
            SceneManager.LoadScene("Loot", LoadSceneMode.Single);
        }

        uiManager.updateMonsterUI();
        uiManager.updateCharacterUI();
        uiManager.showNextButton();
        currentUnitIndex = (currentUnitIndex + 1) % battleUnits.Length;
    }

    IEnumerator MonsterAttackRoutine()
    {
        uiManager.showEnemyAttack();
		bcManager.destroyAllDice(); // 清掉骰子模型

        yield return new WaitForSeconds(1);
        uiManager.showPlayerHurt();
        monster.gameObject.GetComponents<AudioSource>()[1].Play(0);
        yield return new WaitForSeconds(0.5f);
        currentCharacter.gameObject.GetComponents<AudioSource>()[0].Play(0);
        yield return new WaitForSeconds(0.5f);

        uiManager.updateCharacterUI();
        uiManager.showNextButton(); 
        currentUnitIndex = (currentUnitIndex + 1) % battleUnits.Length;
    }

    // 能給別人用的function
    public Character getCharacter()
	{
		return currentCharacter;
	}
}
