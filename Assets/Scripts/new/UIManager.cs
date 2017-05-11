using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public SpriteRenderer[] characterPieces;
	public SpriteRenderer[] characterDicePanels;
	public SpriteRenderer monsterDicePanel;
	public MeshRenderer[] orderValue;
	public MeshRenderer[] order;

	public Button nextButton;
	public Button backButton;

	private Character[] characters;
	private Monster monster;
	private Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setCharacters(Character[] cs){
		characters = cs;
	}

	public void setMonster(Monster m){
		monster = m;
	}

	public void showCharacterPieces(){
		for (int i = 0; i < characters.Length; i++) {
			characterPieces [i].sprite =  characters[i].characterPiece;
		}
	}

	public void showOrderDicePanel(){
		foreach (SpriteRenderer spriterenderer in characterDicePanels) {
			spriterenderer.enabled = true;
		}

		monsterDicePanel.enabled = true;
	}

	public void showOrderValue ()
	{
		for (int i = 0; i < orderValue.Length; i++) {

			if (i == 0) {
				orderValue [i].GetComponent<TextMesh>().text = ""+monster.orderValue;
				order [i].GetComponent<TextMesh> ().text = orderString (monster.order);
			} else {
				orderValue [i].GetComponent<TextMesh>().text = ""+characters[i-1].orderValue;
				order [i].GetComponent<TextMesh> ().text = orderString (characters[i-1].order);
			}
		}

		playOrderValueAnimation ();
	}

	private string orderString(int number){
		switch (number) {
		case 1:
			return "1st";
			break;

		case 2:
			return "2nd";
			break;

		case 3:
			return "3rd";
			break;

		case 4:
			return "4th";
			break;

		default:
			Debug.Log ("PrepareForRollOrder - Error");
			return "Error";
			break;
		}
	}

	private void playOrderValueAnimation(){
		_animator.SetTrigger ("ShowOrderValue");
	}

	public void showNextButton(){
		nextButton.gameObject.SetActive (true);
	}

	public void showBackButton(){
		backButton.gameObject.SetActive (true);
	}
}
