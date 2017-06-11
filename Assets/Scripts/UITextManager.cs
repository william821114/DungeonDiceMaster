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
				title.text = "Round Slash";
				information.text = "If each dice you rolled greater than 2, \nattack power +4.";
				break;
			case 1:
				title.text = "Heavy Slash";
				information.text = "If dices total value greater than 7, \n double attack power & recover 2 HP.";
				break;
			case 2:
				title.text = "Dodge";
				information.text = "If dices total value less than 5, \n dodge next turn's damage.";
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
				title.text = "Antlers";
				information.text = "When enemy's HP greater than 50%, \nprobable attack power +5.";
				break;
			case 1:
				title.text = "Heal";
				information.text = "When enemy's HP less than 50%, \nprobable recover 50% Max HP.";
				break;
			case 2:
				title.text = "Trapped Beast";
				information.text = "When enemy's HP less than 50%, \nprobable double attack power.";
				break;
			default:
				Debug.Log ("UITextManager - showBattleSkillDetail error");
				break;
			}
		} else if (monster == "Rogue") {
			switch (skillType) {
			case 0:
				title.text = "High Defence";
				information.text = "This enemy uses 'Steal' skill every turn.";
				break;
			case 1:
				title.text = "Low Attack";
				information.text = "This enemy's attack will down 1/3.";
				break;
			case 2:
				title.text = "Steal";
				information.text = "Take a random dice from hero.";
				break;
			default:
				Debug.Log ("UITextManager - showBattleSkillDetail error");
				break;
			}
		} else if (monster == "FireMagician") {
			switch (skillType) {
			case 0:
				title.text = "Fire Ball";
				information.text = "If each dice this enemy rolled less than 3, \nhis attack power +10.";
				break;
			case 1:
				title.text = "Seal Magic";
				information.text = "If each dice this enemy rolled is odd number, \nseal one player dices 2 turns.";
				break;
			case 2:
				title.text = "Wide Fire";
				information.text = "Multiple of 3's turn, \nif each dice this enemy rolled is even number, \ngive player 40 damage.";
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
