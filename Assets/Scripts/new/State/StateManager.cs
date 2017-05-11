using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DiceMaster;
using System.Linq;

public class StateManager : MonoBehaviour {

	public State.BattleState currentState;

	public UIManager uiManager;
	public Monster monster;


	private BattleUnit[] battleUnits; // 將所有戰鬥單位排序，放在這裡
	private Character[] characters;
	private Dice[] dice;
	private int cvIndex = 0; // index for check value array


	// Use this for initialization
	void Start () {
		// Battle Scene一開始，就要找到所有角色，並存成array管理。 所有character都是don't destroy。
		characters = (Character[])FindObjectsOfType (typeof(Character));

		// 從怪物池中隨機產生怪物，目前先寫死。

		// 將本場戰鬥參戰的角色和怪物告知UI Manager 
		uiManager.setCharacters(characters);
		uiManager.setMonster (monster);

		// UI: 顯示一開始的下畫面角色圖
		uiManager.showCharacterPieces ();

		// 開始擲順序
		prepareForRollOrder();
	}

	public void prepareForRollOrder(){
		currentState = State.BattleState.RollOrder;

		// UI: 顯示骰子下板
		uiManager.showOrderDicePanel();

		// 產生骰子，並管理成array。
		// dice[0]為怪獸骰，dice[1]~[character.Length]為角色骰
		dice = new Dice[characters.Length + 1];

		for (int i = 0; i < dice.Length; i++) {
			Vector3 dicePosition = new Vector3(0f,0f,0f);

			switch (i) {
			case 0:
				dice [i] = monster.getExploreDice ();
				dicePosition = new Vector3 (0.06f, 5f, 1.25f);
				break;

			case 1:
				dice [i] = characters [i-1].getExploreDice();
				dicePosition = new Vector3 (-2.49f, 5f, -4.95f);
				break;
			
			case 2:
				dice [i] = characters [i-1].getExploreDice();
				dicePosition = new Vector3 (0.03f, 5f, -4.95f);
				break;
			
			case 3:
				dice [i] = characters [i-1].getExploreDice();
				dicePosition = new Vector3 (2.54f, 5f, -4.95f);
				break;
			
			default:
				Debug.Log ("PrepareForRollOrder - Error");
				break;
			}

			// 鎖住x軸和z軸的移動，確保骰子在panel範圍中
			Rigidbody rigidbodyTemp = dice [i].GetComponent<Rigidbody> ();
			rigidbodyTemp.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

			// 讓骰子產生時就旋轉
			Spinner spinnerTemp = dice [i].GetComponent<Spinner> ();
			spinnerTemp.triggerOnStart = true;

			// 生成骰子註冊callback function
			dice [i] = GameObject.Instantiate(dice[i],  dicePosition, Quaternion.identity) as Dice;
			dice [i].onShowNumber.AddListener(RegisterOrderNumber);
		}
	}
		
	public void RegisterOrderNumber(int number)
	{
		// cvIndex用來計算有幾個骰子已經停止轉動，當全部都停止轉動時呼叫sortOrder來排戰鬥順序
		Debug.Log ("Order Number: " + number);

		if (cvIndex < dice.Length - 1) {
			cvIndex++;
		}else if (cvIndex == dice.Length - 1) {
			sortOrder ();
		}
	}

	public void sortOrder(){
		battleUnits = new BattleUnit[dice.Length];

		for (int i = 0; i < dice.Length; i++) {

			if (i == 0) {
				battleUnits [i] = (BattleUnit)monster;
			} else {
				battleUnits [i] = (BattleUnit)characters[i-1];
			}

			// 替每個戰鬥單位設定剛剛擲出來的結果
			battleUnits [i].orderValue = dice [i].value;
		}

		// 根據擲出來的結果由大至小排列
		battleUnits = battleUnits.OrderByDescending(unit => unit.orderValue).ToArray();

		// 替每個戰鬥單位填上順位，方便UI Manager顯示。
		for (int i = 0; i < dice.Length; i++) {
			battleUnits [i].order = i + 1;
			Debug.Log(battleUnits[i].order +" "+ battleUnits[i].name+ " value: "+ battleUnits[i].orderValue);
		}

		// UI: 顯示擲骰子結果
		uiManager.showOrderValue();
	}


	/*public State.BattleState currentState;

    //暫時寫在這 應該有更好的寫法
    public Image BattleResult;
    public Text BattleResultText;


    public Button backButton;
    public Button nextButton;

    public Transform skillButtons;

    // Use this for initialization
    void Start () {
		
	}
	
    void setTurn(State.BattleState state)
    {
        currentState = state;

        // find enemy and heros and notify the change of state
        GameObject[] battlingObjects = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject bObj in battlingObjects)
        {
            Debug.Log(bObj.name);
            bObj.SendMessage("onStateChange", currentState);
        }
    }

    public void setBattleEnd(bool win)
    {
        currentState = State.BattleState.BattleEnd;
        BattleResult.gameObject.SetActive(true);
        BattleResultText.text = (win ? "Win" : "Lose");

    }

    public void prepareForPlayerTurn()
    {
        nextButton.gameObject.SetActive(true);
        skillButtons.gameObject.SetActive(true);
    }

    public State.BattleState getState()
    {
        return currentState;
    }*/

}
