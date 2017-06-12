using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DiceMaster;

public class UITextManager : MonoBehaviour {

	public Animator _animator;
	public Text information;
	public Text title;
	public Image icon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showText(string text){
		this.gameObject.SetActive (true);
		information.text = text;
		_animator.SetTrigger ("ShowPanel");
	}

	public void showLootDetail(int lootType, Sprite image){
		this.gameObject.SetActive (true);
		this.icon.sprite = image;

		switch (lootType) {
		case 0:
			title.text = "Health Postion";
			information.text = "This item can recover character's HP\n 10 points.";
			break;
		case 1:
			title.text = "Magic Postion";
			information.text = "This item can recover character's MP\n 3 points.";
			break;
		default:
			Debug.Log ("UITextManager - showLootDetail error");
			break;
		}

		_animator.SetTrigger ("ShowPanel");
	}

	public void showGambleSkillDetail(int skillType, Sprite image){
		this.gameObject.SetActive (true);
		this.icon.sprite = image;

		switch (skillType) {
		case 0:
			title.text = "Steal";
			information.text = "Take a dice from enemy.\n\nNext Turn, \nThe enemy roll one less dice.\nYou roll one more dice.";
			break;
		case 1:
			title.text = "Thr33";
			information.text = "If the dice value is less than 3, transform it to 3.";
			break;
		case 2:
			title.text = "One More Dice";
			information.text = "Throw one more D6.";
			break;
		case 3:
			title.text = "Keep";
			information.text = "Choose a dice to keep.\nReroll other dices.";
			break;
		case 4:
			title.text = "6 - 9";
			information.text = "If the dice value is 6, transform it to 9.";
			break;
		case 5:
			title.text = "Reroll";
			information.text = "Reroll all dices.";
			break;
		default:
			Debug.Log ("UITextManager - showLootDetail error");
			break;
		}

		_animator.SetTrigger ("ShowPanel");
	}

	public void showDiceDetail(Dice dice){
		
	}

	public void showBattleSkillDetail(string character, int skillType, Sprite image){
		this.gameObject.SetActive (true);
		this.icon.sprite = image;

		if (character == "Knight") {
			switch (skillType) {
			case 0:
				title.text = "迴旋斬";
				information.text = "若所有骰子點數大於 2，\n攻擊點數 +4.";
				break;
			case 1:
				title.text = "重擊";
				information.text = "若所有骰子點數加總大於 7， \n攻擊點數加倍 & 回復 2 HP.";
				break;
			case 2:
				title.text = "閃避";
				information.text = "若骰子點數加總小於 5， \n迴避下回合傷害.";
				break;
			default:
				Debug.Log ("UITextManager - showBattleSkillDetail error");
				break;
			}
		} else if (character == "Theif") {
		} else if (character == "Priest") {
		}

		_animator.SetTrigger ("ShowPanel");
	}

	public void showMonsterSkillDetail(string monster, int skillType, Sprite image){
		this.gameObject.SetActive (true);
		this.icon.sprite = image;

		if (monster == "Deer") {
			switch (skillType) {
			case 0:
				title.text = "犄角攻擊";
				information.text = "當 HP 大於 50% 以上時, \n若骰子總和大於 6 ，攻擊加 5";
				break;
			case 1:
				title.text = "治癒";
				information.text = "怪物血量小於 50% 時，\n50% 機率 ，回復 1/2 MaxHp";
				break;
			case 2:
				title.text = "困獸之鬥";
				information.text = "怪物血量小於 50% 時，\n50% 機率 如果所有骰子都大於 2，\n攻擊最後加倍";
				break;
			default:
				Debug.Log ("UITextManager - showBattleSkillDetail error");
				break;
			}
		} else if (monster == "Rogue") {
			switch (skillType) {
			case 0:
				title.text = "肉盾";
				information.text = "每一回合都會使用 綁架 封印我方一顆骰子";
				break;
			case 1:
				title.text = "搔癢";
				information.text = "攻擊點數減少 1/3";
				break;
			case 2:
				title.text = "綁架";
				information.text = "隨機封印一顆骰子";
				break;
			default:
				Debug.Log ("UITextManager - showBattleSkillDetail error");
				break;
			}
		} else if (monster == "FireMagician") {
			switch (skillType) {
			case 0:
				title.text = "火球術";
				information.text = "若骰子全部小於 3，\n則最後傷害 + 10";
				break;
			case 1:
				title.text = "封印術";
				information.text = "若全部骰子為奇數，\n封印隨機一顆骰子 2 回合";
				break;
			case 2:
				title.text = "野火燎原";
				information.text = "每 3 倍數回合必用，\n若所有骰子為偶數，\n造成 40 傷害";
				break;
			default:
				Debug.Log ("UITextManager - showBattleSkillDetail error");
				break;
			}
		}

		_animator.SetTrigger ("ShowPanel");
	}
	
	public void hide(){
		this.gameObject.SetActive (false);
	}
}
