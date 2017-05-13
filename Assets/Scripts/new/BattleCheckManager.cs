using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceMaster;


public class BattleCheckManager : MonoBehaviour {
	
    public StateManager stateManager;
	public UIManager uiManager;
	public TextMesh checkValueText;

	private Character currentCharacter; // 目前行動中or被選定為攻擊對象的角色是哪隻
	private Monster monster;
	private Skill usingSkill; // 目前使用的技能

	private Dice[] dices; // 從角色中取得的骰子會放在這裡
	private int finalCheckValue = 0; // 最終用來做判定的值
	private int[] checkValue; // 每個骰子擲出來的結果放在這裡
	private int cvIndex = 0; // index for check value array

	public bool isReadyToRoll = false; // 可以開始擲了
	private bool isRolled = false; // 已經擲了


	void Start(){
	}

	// 設定目前行動的角色
	public void setCurrentCharacter(Character c){
		currentCharacter = c;
	}

	// 設定現在要判定的對象
	public void setMonster(Monster m){
		monster = m;
	}

	// 從角色取得其身上裝備的骰子，並將其生成到畫面上。
	public void setForPlayerToRoll(){
		if (!isReadyToRoll) {
			Dice[] d = currentCharacter.getBattleDice ();
			dices = new Dice[d.Length];
			for (int i = 0; i < dices.Length; i++) {
				
				// 解鎖x軸和z軸的移動
				Rigidbody rigidbodyTemp = d [i].GetComponent<Rigidbody> ();
				rigidbodyTemp.constraints = RigidbodyConstraints.None;

				// 骰子產生時不要旋轉
				Spinner spinnerTemp = d [i].GetComponent<Spinner> ();
				spinnerTemp.triggerOnStart = false;

				// 稍微調大骰子
				Transform transformTemp = d [i].GetComponent<Transform> ();
				transformTemp.localScale = new Vector3 (0.8f, 0.8f, 0.8f);

				dices [i] = GameObject.Instantiate (d [i], new Vector3 (Random.Range (-3f, 3f), -1f, Random.Range (-6f, -1.5f)), Quaternion.identity) as Dice;
				dices [i].onShowNumber.AddListener (RegisterNumber);
			}
			checkValue = new int[dices.Length];
			finalCheckValue = 0;
			isReadyToRoll = true;
			isRolled = false;
		}
	}

	public void monsterRoll(){
		Dice[] d = monster.getBattleDice();
		dices = new Dice[d.Length];
		for (int i = 0; i < dices.Length; i++) {

			// 解鎖x軸和z軸的移動
			Rigidbody rigidbodyTemp = d [i].GetComponent<Rigidbody> ();
			rigidbodyTemp.constraints = RigidbodyConstraints.None;

			// 骰子產生時不要旋轉
			Spinner spinnerTemp = d [i].GetComponent<Spinner> ();
			spinnerTemp.triggerOnStart = false;

			// 稍微調大骰子
			Transform transformTemp = d [i].GetComponent<Transform> ();
			transformTemp.localScale = new Vector3 (0.8f, 0.8f, 0.8f);

			dices [i] = GameObject.Instantiate (d [i], new Vector3 (Random.Range (-3f, 3f), -1f, Random.Range (-6f, -1.5f)), Quaternion.identity) as Dice;
			dices[i].onShowNumber.AddListener(RegisterNumber);
		}
		checkValue = new int[dices.Length];
		finalCheckValue = 0;
		isReadyToRoll = true;
		isRolled = false;

		//rollDices ();
	}

	public void destroyAllDice(){
		for (int i = 0; i < dices.Length; i++) {
			Destroy (dices[i].gameObject);
		}
	}

	// 取得最終累計的數值，丟給目標去判定是否結果。
	public void check(){

		for (int i = 0; i < checkValue.Length; i++) {
			finalCheckValue += checkValue [i];
		}

		checkValueText.text = "~" + finalCheckValue + "~";
		uiManager.playCheckValueAnimation ();

		/*
		bool isHeal = false;
		if (usedSkill) {
			finalCheckValue = usingSkill.skillWeight (checkValue);
            isHeal = usingSkill.isHealSkill;
		} else {
			for(int i=0; i<checkValue.Length; i++)
				finalCheckValue += checkValue[i];
		}

        if (isHeal)
        {
            Debug.Log("Using Heal Skill");
            actioningCharacter.getHeal(finalCheckValue);
        } else
        {
            aimedBarrier.check(finalCheckValue);
        }


        // 更新state: 敵人的回合
        // 用 start Coroutine 我覺得超爛的 但暫時先這樣用
        StartCoroutine(ChangeTurn());
		*/
    }
	
	/*
    // 等待一秒鐘 換回合
    IEnumerator ChangeTurn()
    {       
        if (!stateManager.getState().Equals(State.BattleState.BattleEnd))
        {
            // 若為戰鬥事件，且敵人未擊倒，則要繼續下一輪選技能、擲骰。 
            // 這個寫法不好，應該要讓凌駕於check system的state machine處理，
            // 由它來做介面的刷新，和行動順序。 而且check完，還要讓敵人攻擊，若敵人或目前行動角色死亡，還要重新set character, barrier,
            // 這邊只是先寫來測試而已。
            if (checkEvent == 0)
            {
                // clean dice for next turn
                for (int i = 0; i < dice.Length; i++)
                {
                    Destroy(dice[i].gameObject, 1.0f);
                }

                SpriteRenderer sr = actioningCharacter.GetComponent<SpriteRenderer>();
                sr.enabled = true;
            }

            yield return new WaitForSeconds(1);

            stateManager.SendMessage("setTurn", State.BattleState.EnemyTurn);
        }   
    }
	*/

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

	public void rollDices()
	{
		uiManager.hideBackButton ();
		if (isReadyToRoll) {
			foreach (Dice dice in dices) {
				
				Spinner spinner = dice.gameObject.GetComponent<Spinner> ();
				Thrower thrower = dice.gameObject.GetComponent<Thrower> ();

				spinner.Trigger ();
				thrower.Trigger ();
			}

			isRolled = true;
			isReadyToRoll = false;
		}
	}
}