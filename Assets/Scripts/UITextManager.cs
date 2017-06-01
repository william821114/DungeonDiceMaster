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

	public void showBattleSkillDetail(string detail, Sprite image){
	
	}
	
	public void hide(){
		this.gameObject.SetActive (false);
	}
}
