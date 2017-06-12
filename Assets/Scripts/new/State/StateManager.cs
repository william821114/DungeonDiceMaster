using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DiceMaster;
using UnityEngine.SceneManagement;


public class StateManager : MonoBehaviour {

	public State.BattleState currentState;

	public UIManager uiManager;
	public BattleCheckManager bcManager;
    public AudioManager audioManager;
	public Monster monster;
	public GameObject monsterMark;
    public int turn;
    public GameObject normalAttackPrefab;
    public GameObject HealPrefab;

    public ScreenEffectManager screenEffectManager;

	private DataManager dataManager;
	private BattleUnit[] battleUnits = new BattleUnit[2]; // 將所有戰鬥單位排序，放在這裡
	private int currentUnitIndex = 0; // 指向目前可行動的戰鬥單位
	private Character currentCharacter; // 目前行動的角色
    

    // Use this for initialization
    void Start () {
		// Battle Scene一開始，就要找到所有角色，並存成array管理。 所有character都是don't destroy。
		dataManager = (DataManager)FindObjectOfType (typeof(DataManager));
		currentCharacter = dataManager.choosedHero;
        turn = 0;

        // 找到Audio manager
        audioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));

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
                {
                    //執行turn end來解除腳色骰子的封印狀態
                    currentCharacter.turnEnd();

                    setState(State.BattleState.EnemyRollBattleDice);
                }
                    
			else {
				// 如果角色死亡，則Game Over
				if (currentCharacter.Hp <= 0) {
					setState (State.BattleState.BattleEnd);
				}
                else {
                    //執行turn end來解除骰子的封印狀態
                    Debug.Log(monster.name);
                    monster.turnEnd();
                    // 回合數 + 1
                    turn += 1;

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
			bcManager.setForPlayerToRoll (2); // 2表示第二次擲骰子
            uiManager.showDiceRolling2Panel();

			break;

		case State.BattleState.EnemyAttack:
            StartCoroutine(MonsterAttackRoutine());
            break;
		
		case State.BattleState.PlayerAttack:
            StartCoroutine(PlayerAttackRoutine());
			break;

		case State.BattleState.BattleEnd:
			currentCharacter.transform.SetParent (dataManager.transform, false);
			currentCharacter.transform.localPosition = new Vector3 (0f, 10f, 0f);
            currentCharacter.resetDiceState();


            if (currentCharacter.Hp > 0)
            {
                screenEffectManager.fadeOutToLoot();
            }
			else
            {
                screenEffectManager.fadeOutToGameOver();
            }
				
			break;
		
		default:
			Debug.Log ("setState - ErrorState");
			break;
		}
	}

    IEnumerator PlayerAttackRoutine()
    {
        SkillEffect se = bcManager.getBattleSkillEffect();

        uiManager.showPlayerAttack();
		bcManager.destroyAllDice(); // 清掉骰子模型

        uiManager.showPlayerSkillActivate();

        if (se == null)
            yield return new WaitForSeconds(1.5f);
        else
            yield return new WaitForSeconds(1);

        if(bcManager.turnDamage > 0)
        {
            uiManager.showPlayerAttackAnim();
            
            yield return new WaitForSeconds(0.5f);
            audioManager.playPlayerAttack();
            if (se != null && se.isSkillActivated && se.isDamage)
            {
                Instantiate(bcManager.getBattleSkill().skillprefab, monster.transform.position, Quaternion.identity);
            }

            else
            {
                Instantiate(normalAttackPrefab, monster.transform.position, Quaternion.identity);
            }
            uiManager.showMonsterHurt();
            yield return new WaitForSeconds(1);

        }

        if (se != null && se.isHeal)
        {
            Instantiate(HealPrefab, currentCharacter.transform.position, Quaternion.identity);
            uiManager.showPlayerRecoverHp();
            yield return new WaitForSeconds(1);
        }
        
        // demo用
        if (monster.Hp <= 0)
        {
			// 提醒一下，換scene前一定要將角色parent移回dataManager，不然就算有Don't Destroy，也因為父親被砍，而消失。
			// 只有戰敗死亡的時候，才不用管，反正都要重頭來過
			currentCharacter.transform.SetParent(dataManager.transform, false);
			currentCharacter.transform.localPosition = new Vector3 (0f, 10f, 0f);
			screenEffectManager.fadeOutToLoot();
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
        
        if(monster.mse.isSkillActivated)
        {
            uiManager.showMonsterSkillActivated();
            yield return new WaitForSeconds(0.5f);
        }

        if(monster.mse.isDamage)
        {
            uiManager.showMonsterAttack();     

            //迴避
            if (monster.mse.isAtkDodged)
            {
                uiManager.showPlayerDodge();
                audioManager.playPlayerDodge();
                yield return new WaitForSeconds(0.5f);
                audioManager.playMonsterAttackDodged();
                yield return new WaitForSeconds(0.5f);
                
            } else
            {
                yield return new WaitForSeconds(0.5f);
                uiManager.showPlayerHurt();

                if (monster.mse.isSkillActivated)
                {
                    Instantiate(monster.mse.usingSkill.skillprefab, currentCharacter.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(normalAttackPrefab, currentCharacter.transform.position, Quaternion.identity);
                }

                audioManager.playMonsterAttack();
                yield return new WaitForSeconds(0.5f);
                audioManager.playPlayerHurt();
                yield return new WaitForSeconds(0.5f);
            }    
        }

        if(monster.mse.isHeal)
        {
            uiManager.showMonsterRecoverHp();
            Instantiate(HealPrefab, monster.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            uiManager.updateMonsterUI();
        }

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
