using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceMaster;


public class BattleCheckManager : MonoBehaviour {
	
    public StateManager stateManager;
	public UIManager uiManager;
	public TextMesh checkValueText;
	public ValueTextManager checkValueTextManager, gamebleValueTextManager;
	public Dice normalD6;
	public Collider flickArea;


	private Character currentCharacter; // 目前行動中or被選定為攻擊對象的角色是哪隻
	private Monster monster;
	private Skill usingBattleSkill; // 目前使用的戰鬥技能
    private SkillEffect usingBattleSkillEffect; // 戰鬥技能在本回合中造成的效果
	private int usingGambleSkillIndex; // 目前使用的賭博技能，因為賭技通用且固定，直接用int來表示，另一個原因是賭技歧異度大，不太適合統一繼承。

	private Dice[] dices; // 從角色中取得的骰子會放在這裡
	private int finalCheckValue = 0; // 最終用來做判定的值
	private int[] checkValue; // 每個骰子擲出來的結果放在這裡
	private int cvIndex = 0; // index for check value array

	public bool isReadyToRoll = false; // 可以開始擲了
	private bool isRolled = false; // 已經擲了
	private int rollState = 1; 	// 紀錄這是第幾次擲骰子，
								// 1表示選完battle skill後的擲骰，2表示選完gamble skill後的擲骰，3表示怪物擲骰子。

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

	public void setUsingBattleSkill(Skill bs){
		usingBattleSkill = bs;

		if (usingBattleSkill)
			Debug.Log ("battle skill - set: " +usingBattleSkill.name);
		else
			Debug.Log ("battle skill - unset");
	}

	public void setUsingGambleSkill(int gsIndex){
		usingGambleSkillIndex = gsIndex;

		Debug.Log ("gamble skill - "+usingGambleSkillIndex);
	}

	// 從角色取得其身上裝備的骰子，並將其生成到畫面上。
	public void setForPlayerToRoll(int rollState){
		this.rollState = rollState;

		if (rollState == 1) {
			if (!isReadyToRoll) {

				Dice[] d = currentCharacter.getBattleDice ();
				dices = new Dice[d.Length];
				for (int i = 0; i < dices.Length; i++) {
					
					// 解鎖移動
					Rigidbody rigidbodyTemp = d [i].GetComponent<Rigidbody> ();
					rigidbodyTemp.constraints = RigidbodyConstraints.None;
					/*
					// 骰子產生時不要旋轉
					Spinner spinnerTemp = d [i].GetComponent<Spinner> ();
					spinnerTemp.triggerOnStart = false;

					// 稍微調大骰子
					Transform transformTemp = d [i].GetComponent<Transform> ();
					transformTemp.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
					*/
					dices [i] = GameObject.Instantiate (d [i], new Vector3 (Random.Range (-2.5f, 2.5f), -1f, Random.Range (-5.5f, -2f)), Quaternion.identity) as Dice;
					dices [i].onShowNumber.AddListener (RegisterNumber);
				}
				checkValue = new int[dices.Length];
				finalCheckValue = 0;
				isReadyToRoll = true;
				isRolled = false;
			}
		} else if (rollState == 2) {
			// 處理賭技效果，根據情況改變isReadyToRoll, isRolled, Dice[]個數等等細節
			switch (usingGambleSkillIndex) {
			case 0:
				// 奪取：隨機偷取敵人一顆骰子一回合，敵人攻擊下次攻擊少一顆骰子，這次多擲一顆敵人取來的骰子
				// 作法：設定怪物flag，讓他下次回合少一個骰子，monster.getBattleDice要改寫一下（加個判斷式）
				// 		然後以擲順序骰的呈現方式，顯示出圓形的dice panel，取一個敵人的骰子，出現一個按鈕，點撃後讓它旋轉落下取值。
				//		最後把將這個值加到this.checkValue[]和this.finalCheckValue中，再呼叫check()做最後判定
				break;

			case 1:
				// 盜夢空間：全部小於3的骰子，通通變成3
				// 作法：檢查this.Dice[].value，如果值小於3，則把它改成3。
				// 		另外，要在每個骰子prefab綁上3D Text，位置設定在骰子正上方
				//		(也許要把骰子設為3D Text的子物件，這樣骰子轉動才不會影響到Text的位置)
				//		設定TextMesh.text = 3，對有改變的骰子呼叫動畫播放，讓這個TextMesh淡入淡出。
				//		(PS. TextMesh要綁上我寫的script - textRendererChange，因為要改動MeshRenderer的sorting layer，
				//		Unity只有SpriteRenderer可以在insperctor中設定layer)
				isReadyToRoll = false;
				isRolled = true;
				for (int i = 0; i < dices.Length; i++) {
					if (dices [i].value < 3) {
						dices [i].value = 3;

						Quaternion rotationTemp = Quaternion.identity;;
						rotationTemp.eulerAngles = new Vector3(90f,0f,0f);
							
						ValueTextManager gvt;
						gvt = GameObject.Instantiate (gamebleValueTextManager, Vector3.zero, rotationTemp) as ValueTextManager;
						gvt.transform.SetParent(dices [i].gameObject.transform, true);
						gvt.transform.localPosition = Vector3.zero;
						gvt.showGambleSkillValue (3);
					}

					if (checkValue [i] < 3)
						checkValue [i] = 3;
				}
				check ();
				break;

			case 2:
				// 口袋夾層：多值一顆普通D6
				// 作法：以擲順序骰的呈現方式，顯示出圓形的dice panel，取一個普通D6，讓它自動轉取值。
				//		最後把將這個值加到this.checkValue[]和this.finalCheckValue中，再呼叫check()做最後判定

				int[] temp = checkValue;
				checkValue = new int[dices.Length + 1];
				for (int i = 0; i < checkValue.Length - 1; i++)
					checkValue [i] = temp [i];
				cvIndex = dices.Length;

				destroyAllDice();

				dices = new Dice[1];
				// 解鎖移動
				Rigidbody rigidbodyTemp = normalD6.GetComponent<Rigidbody> ();
				rigidbodyTemp.constraints = RigidbodyConstraints.None;
				dices [0] = GameObject.Instantiate (normalD6, new Vector3 (0f, -1f,-4.5f), Quaternion.identity) as Dice;
				dices [0].onShowNumber.AddListener (RegisterNumber);

				isReadyToRoll = true;
				isRolled = false;
				break;

			case 3:
				// 保留：選擇一顆骰子保留，其他重擲。（這應該最麻煩的）
				// 作法：要先在所有骰子prefab裝上TouchScript的TapGesture，然後enble先設為false。
				//		這個技能被選用的時候，在對this.Dice[]的所有骰子enble設為true。
				//		玩家可以對骰子點選，這邊要處理只能選一個骰子，還要highlight選定的骰子。
				//		選取骰子後，畫面中間要顯示ReRoll的按鈕讓玩家點選（或出現提示叫玩家flick來擲骰子）。
				//		這時候注意，Destroy選擇的骰子，並紀錄且顯示它的數值在畫面中間。(finalCheckValue = 保留的骰子值)
				//		isReadyToRoll = true, 呼叫rollDice, 在check()處理最終值（把保留的骰子值加回去）
				flickArea.enabled = false;
				for(int i=0; i<dices.Length; i++)
				{
					DiceTapGestureManager dtgm = dices [i].GetComponent<DiceTapGestureManager> ();
					dtgm.tapable = true;
				}
				break;

			case 4:
				// 要從哪邊看？：全部6變成9
				// 作法：同【盜夢空間】
				isReadyToRoll = false;
				isRolled = true;
				for (int i = 0; i < dices.Length; i++) {
					if (dices [i].value == 6) {
						dices [i].value = 9;

						Quaternion rotationTemp = Quaternion.identity;;
						rotationTemp.eulerAngles = new Vector3(90f,0f,0f);

						ValueTextManager gvt;
						gvt = GameObject.Instantiate (gamebleValueTextManager, Vector3.zero, rotationTemp) as ValueTextManager;
						gvt.transform.SetParent(dices [i].gameObject.transform, true);
						gvt.transform.localPosition = Vector3.zero;
						gvt.showGambleSkillValue (9);
					}

					if (checkValue [i] == 6 )
						checkValue [i] = 9;
				}
				check ();
				break;

			case 5:
                // 時間機器：全部重擲
                // 作法：isReadyToRoll = true, finalCheckValue = 0, 畫面中顯示重擲按鈕，點選後重擲。
                //		或是出現提示叫玩家flick來擲骰子。
                // 目前還沒有賭技，但為了測試狀態機轉換是否正常，所以先設為重擲
                finalCheckValue = 0;
                isReadyToRoll = true;
                isRolled = false;
                break;

			default:
				Debug.Log ("No Gamble Skill");
                // 目前還沒有賭技，但為了測試狀態機轉換是否正常，所以先設為重擲
				isReadyToRoll = false;
				isRolled = true;
				check ();
                break;
			}

			
		}
	}

	public void monsterRoll(){
		this.rollState = 3; // 因為是怪物擲骰子，所以roll state直接設為3

		Dice[] d = monster.getBattleDice();
		dices = new Dice[d.Length];

		for (int i = 0; i < dices.Length; i++) {
			
			// 解鎖x移動
			Rigidbody rigidbodyTemp = d [i].GetComponent<Rigidbody> ();
			rigidbodyTemp.constraints = RigidbodyConstraints.None;
			/*
			// 骰子產生時不要旋轉
			Spinner spinnerTemp = d [i].GetComponent<Spinner> ();
			spinnerTemp.triggerOnStart = false;

			// 稍微調大骰子
			Transform transformTemp = d [i].GetComponent<Transform> ();
			transformTemp.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
			*/
			dices [i] = GameObject.Instantiate (d [i], new Vector3 (Random.Range (-2.5f, 2.5f), -1f, Random.Range (-5.5f, -2f)), Quaternion.identity) as Dice;
			dices[i].onShowNumber.AddListener(RegisterNumber);
		}
		checkValue = new int[dices.Length];
		finalCheckValue = 0;
		isReadyToRoll = true;
		isRolled = false;
	}

	public void destroyAllDice(){
		for (int i = 0; i < dices.Length; i++) {
			if(dices[i])
				Destroy (dices [i].gameObject);
		}
	}

	// 取得最終累計的數值。
	public void check(){
		if (rollState == 2) {
            // 已經是第二次擲骰子了，所以必須檢查技能有無發動，並計算最終結果丟給戰鬥對象做check。
            //
            // 寫法應該會是monster.check(finalCheckValue)
            // 如果判定通過，monster的class應該要設定一個hurt flag
            // 等到玩家按下繼續按鈕，到達player attack的階段時，
            // 在呼叫monster.playHurtAnimation()來播放傷害動畫。
            // playHurtAnimation()應該會有if判斷式來檢查hurt flag，
            // flag立起來就trigger，不然就沒事。
            // (PS. 特殊的技能應該也會在這邊立起flag，寫法應該同上一次彥求寫的isHeal，

            Debug.Log("Roll2");

            // 重置 Skill Effect
            usingBattleSkillEffect = null;

            finalCheckValue = 0;
            // 拿到重擲的final check value
            for (int i = 0; i < checkValue.Length; i++)
            {
                finalCheckValue += checkValue[i];
            }

            Debug.Log("finalcheck: " + finalCheckValue);
            int damage = finalCheckValue;


            // 判定技能是否發動
            if (usingBattleSkill)
            {
                currentCharacter.Mp -= usingBattleSkill.needMP;

                // 技能分成迴避技能、治療技能、攻擊技能，但這個做法不好，
                // 應該要把skillWeight改成回傳一個skillResultCallback物件，這樣才可以因應不能狀況發動不同效果。
                // 像是可以邊攻擊邊迴避或邊治癒，這之後再改


                usingBattleSkillEffect = usingBattleSkill.calSkillEffect(checkValue);
                

                // 此技能有傷害
                if (usingBattleSkillEffect.isDamage)
                {
                    damage = usingBattleSkillEffect.damage;
                    monster.check(damage);
                }

                // 此技能有治療
                if (usingBattleSkillEffect.isHeal)
                {
                    currentCharacter.recoverHP(usingBattleSkillEffect.heal);

                }

                // 此技能有回避
                if (usingBattleSkillEffect.isDodge)
                {
                    currentCharacter.noHurtTurn += usingBattleSkillEffect.dodge;
                }

                // 此技能有減傷頓，之後再作
                if (usingBattleSkillEffect.isShield)
                {

                }

                // 此技能有回復MP
                if (usingBattleSkillEffect.isHealMP)
                {
                    currentCharacter.recoverMP(usingBattleSkillEffect.healMP);
                }

                // 此技能減少對面的MP
                if (usingBattleSkillEffect.isMPDamage)
                {
                    monster.damageMp(usingBattleSkillEffect.MPDamage);
                }

                // 此技能封印骰子
                if(usingBattleSkillEffect.isDisable)
                {

                }

                // 此技能封印自己骰子
                if(usingBattleSkillEffect.isSelfDisable)
                {

                }

            }
            // 普通攻擊
            else
            {
                monster.check(damage);
            }

        }	else if(rollState == 3){
            // 這是怪物擲骰子，攻擊玩家的傷害處理。
            // 首先怪物應該有AI選擇攻擊對象，monster.AI(finalCheckValue);
            // AI()會找場上的角色，並根據預先設定好的攻擊選擇，對選定的角色做判定。
            // 然後和前面一樣，對character的class設定hurt flag或其他特殊效果flag
            // 玩家按下繼續按鈕，到enemy attack的階段，在播放玩家傷害動畫。
            // 傷害動畫的寫法也同上面所述。
            Debug.Log("Roll3");

            for (int i = 0; i < checkValue.Length; i++)
            {
                finalCheckValue += checkValue[i];
            }

            monster.AI(checkValue);
        }
        else if(rollState == 1)
        {
            Debug.Log("Roll1");
            // 這邊會計算擲骰的結果值，同時處理battle skill的效果
            for (int i = 0; i < checkValue.Length; i++)
            {
                finalCheckValue += checkValue[i];
            }
        }
			
		if (usingGambleSkillIndex != -1 || rollState == 1 || rollState == 3)
		{
			if (usingGambleSkillIndex == 2 && rollState == 2) {
				string checkValueText = "~" + (finalCheckValue - checkValue [checkValue.Length - 1]) + "+" + checkValue [checkValue.Length - 1] + "~";
				checkValueTextManager.showCheckValue2 (checkValueText);
			}
			else
				checkValueTextManager.showCheckValue (finalCheckValue);
		}

		if (usingGambleSkillIndex != -1 || rollState == 1 || rollState == 3)
			uiManager.showNextButton ();
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

	public void rollDices()
	{
	//	uiManager.hideBackButton ();
		if (isReadyToRoll) {
			foreach (Dice dice in dices) {

				if (dice) {
					Spinner spinner = dice.gameObject.GetComponent<Spinner> ();
					Thrower thrower = dice.gameObject.GetComponent<Thrower> ();

					spinner.Trigger ();
					thrower.Trigger ();
				}
			}

			isRolled = true;
			isReadyToRoll = false;
		}
	}

    public SkillEffect getBattleSkillEffect()
    {
        return usingBattleSkillEffect;
    }

	public void lockSelectDice(bool toLock, int lockedDiceValue){
		flickArea.enabled = toLock;
		checkValue [0] = lockedDiceValue;
		cvIndex = 1;
		isRolled = false;
		isReadyToRoll = true;
	}
}