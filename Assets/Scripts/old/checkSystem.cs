using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceMaster;
using UnityEngine.UI;

/* 
   攻略關卡的角色應該在資源分配畫面選擇完後，就一直存在，就算死亡也不會destroy。
   
                    ＿＿例如戰鬥時應該都在同一個scene，但戰鬥中每個階段(選技能、擲骰子、敵人攻擊、換行動角色)，應該有不同的
                   ｜   介面，光是不同角色行動，下畫面的頭像和技能按鈕就不同。所以要有這個系統來管理，現在是什麼階段，哪些
                   ｜   UI物件要顯示，哪些要隱藏。還要根據各階段通知這個check system來處理判定。
                   ｜   事件判定結束後還要透過此系統來切換scene。
                   ｜
   需要一個管理目前介面狀況的系統，基本上是個state machine，它會掌握目前攻略的情況，
   來調整介面的顯示，還有設定這個checkSystem中的 check event, actioning character, aimed barrier
   這個系統應該不會因為scene的切換而消失。
*/

public class checkSystem : MonoBehaviour {
	/*
	public int checkEvent; // 目前的事件是哪一類？ 0 = battle / 1 = social / 2 = explore
	public Character actioningCharacter; // 目前行動中的角色是哪隻？
	public Barrier aimedBarrier; // 目前要判定的對象
    public StateManager stateManager; //判定目前的state 並在攻擊完之後設定新的state
	private Skill usingSkill; // 目前使用的技能？
	private bool usedSkill = false; // 有沒有使用技能？

	public Button backButton;
	public Button nextButton;

    public Transform skillButtons;

    public Text checkValueText;
	private Animator textFeedback;

	private Dice[] dice; // 從角色中取得的骰子會放在這裡
	private int finalCheckValue = 0; // 最終用來做判定的值
	private int[] checkValue; // 每個骰子擲出來的結果放在這裡
	private int cvIndex = 0; // index for check value array

	private bool isReadyToRoll = false; // 可以開始擲了
	private bool isRolled = false; // 已經擲了


	void Start(){
		textFeedback = checkValueText.gameObject.GetComponent<Animator> ();
	}

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

	// 設定現在要判定的對象
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

		Debug.Log("Total " + finalCheckValue);
		checkValueText.text = "" + finalCheckValue;
		ShowCheckValue ();



        // 更新state: 敵人的回合
        // 用 start Coroutine 我覺得超爛的 但暫時先這樣用
        StartCoroutine(ChangeTurn());

    }

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

    private void ShowCheckValue(){
		textFeedback.SetTrigger ("Show");
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
					backButton.gameObject.SetActive (false);
				}
			}
		}
	}
	*/
}
