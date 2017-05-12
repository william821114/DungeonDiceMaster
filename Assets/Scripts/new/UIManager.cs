using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public StateManager stateManager;
	public State.BattleState currentState;

	public SpriteRenderer[] characterPieces;
	public SpriteRenderer[] characterDicePanels;
	public SpriteRenderer monsterDicePanel;
	public TextMesh[] orderValue;
	public TextMesh[] order;

	public TextMesh characterHP;
	public TextMesh characterMP;
	public TextMesh characterDEF;
	public SpriteRenderer characterHalf;
	public SpriteRenderer[] battleSkill;

	public Button nextButton;
	public Button backButton;

	private Character[] characters;
	private Monster monster;
	private Character currentCharacter;
	private Sprite[] battleSkillOn;
	private Sprite[] battleSkillOff;
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

	public void setCurrentCharacter(Character c){
		currentCharacter = c;
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

	public void hideOrderDicePanel(){
		foreach (SpriteRenderer spriterenderer in characterDicePanels) {
			spriterenderer.enabled = false;
		}

		monsterDicePanel.enabled = false;
	}

	public void showOrderValue ()
	{
		for (int i = 0; i < orderValue.Length; i++) {

			if (i == 0) {
				orderValue [i].text = ""+monster.orderValue;
				order [i].text = orderString (monster.order);
			} else {
				orderValue [i].text = ""+characters[i-1].orderValue;
				order [i].text = orderString (characters[i-1].order);
			}
		}

		playOrderValueAnimation ();
	}

	private void setNextButton(State.BattleState state){

		State.BattleState nextState = state;

		switch (state) {
		case State.BattleState.RollOrder:
			nextState = State.BattleState.SelectBattleSkill;
			break;

		case State.BattleState.SelectBattleSkill:
			nextState = State.BattleState.RollBattleDice;
			break;
		
		case State.BattleState.RollBattleDice:
			Debug.Log ("setNextButton - RollBattleDice");
			break;

		default:
			Debug.Log ("setNextButton - ErrorState");
			break;
		}

		nextButton.GetComponent<Button>().onClick.AddListener(() => stateManager.setState(nextState));
	}

	private void setBackButton(State.BattleState state){

		State.BattleState previousState = state;

		switch (state) {
		case State.BattleState.RollBattleDice:
			previousState = State.BattleState.SelectBattleSkill;
			break;

		default:
			Debug.Log ("setBackButton - ErrorState");
			break;
		}

		backButton.GetComponent<Button>().onClick.AddListener(() => stateManager.setState(previousState));
	}

	public void showNextButton(){
		setNextButton (currentState);
		nextButton.gameObject.SetActive (true);
	}

	public void hideNextButton(){
		nextButton.gameObject.SetActive (false);
	}

	public void showBackButton(){
		setBackButton (currentState);
		backButton.gameObject.SetActive (true);
	}

	public void hideBackButton(){
		backButton.gameObject.SetActive (false);
	}

	public void showBattleSkillPanel(){
		showNextButton ();
		hideBackButton ();

		battleSkillOn = currentCharacter.battleSkillOn;
		battleSkillOff = currentCharacter.battleSkillOff;

		for (int i = 0; i < battleSkill.Length; i++) {
			battleSkill [i].sprite =  battleSkillOff[i];
			SkillButtonGestureManager sbgm = battleSkill [i].gameObject.GetComponent<SkillButtonGestureManager>() ;
			sbgm.skillOff = battleSkillOff [i];
			sbgm.skillON = battleSkillOn [i];
		}

		characterHalf.sprite = currentCharacter.characterHalf;
		characterHP.text = "" + currentCharacter.Hp;
		characterMP.text = "" + currentCharacter.Mp;
		characterDEF.text = "" + currentCharacter.Def;

		playSwipeToBattleSkillAnimation ();
	}

	public void showDiceRollingPanel(){
		hideNextButton ();
		showBackButton ();

		playSwipeToDiceRollingAnimation ();
	}

	private string orderString(int number){
		switch (number) {
		case 1:
			return "1st";

		case 2:
			return "2nd";

		case 3:
			return "3rd";

		case 4:
			return "4th";

		default:
			Debug.Log ("PrepareForRollOrder - Error");
			return "Error";
		}
	}

	private void playOrderValueAnimation(){
		_animator.SetTrigger ("ShowOrderValue");
	}

	private void playSwipeToBattleSkillAnimation(){
		_animator.SetTrigger ("SwipeToBattleSkill");
	}

	private void playSwipeToDiceRollingAnimation(){
		_animator.SetTrigger ("SwipeToDiceRolling");
	}
}
