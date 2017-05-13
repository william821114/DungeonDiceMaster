using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public StateManager stateManager;
	public State.BattleState currentState;

	public BattleCheckManager bcManager;

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
	private bool isBackClicked = false; // 用來判斷是否back button，以此決定面板平移動畫要播哪個方向的

	// Use this for initialization
	void Start () {
		_animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//-----------------------------------Set Function----------------------------------
	public void setCharacters(Character[] cs){
		characters = cs;
	}

	public void setMonster(Monster m){
		monster = m;
	}

	public void setCurrentCharacter(Character c){
		currentCharacter = c;
	}


//-----------------------------------Button Function Set----------------------------------
	private void setNextButton(State.BattleState state){

		State.BattleState nextState = state;

		switch (state) {
		case State.BattleState.RollOrder:
			nextState = State.BattleState.SelectBattleSkill;
			break;

		case State.BattleState.SelectBattleSkill:
			nextState = State.BattleState.PlayerRollBattleDice;
			break;
		
		case State.BattleState.PlayerRollBattleDice:
			nextState = State.BattleState.SelectGambleSkill;
			break;

		case State.BattleState.EnemyRollBattleDice:
			nextState = State.BattleState.EnemyAttack;
			break;
		
		case State.BattleState.SelectGambleSkill:
			Debug.Log ("setNextButton - PlayerAttack");
			break;

		case State.BattleState.EnemyAttack:
			Debug.Log ("setNextButton - SelectBattleSkill");
			break;

		case State.BattleState.PlayerAttack:
			Debug.Log ("setNextButton - SelectBattleSkill");
			break;

		default:
			Debug.Log ("setNextButton - ErrorState");
			break;
		}

		Button tmp = nextButton.GetComponent<Button> ();
		tmp.onClick.RemoveAllListeners ();
		tmp.GetComponent<Button>().onClick.AddListener(() => clickBack(false));
		tmp.GetComponent<Button>().onClick.AddListener(() => stateManager.setState(nextState));
	}


	private void setBackButton(State.BattleState state){

		State.BattleState previousState = state;

		switch (state) {
		case State.BattleState.PlayerRollBattleDice:
			previousState = State.BattleState.SelectBattleSkill;
			break;

		default:
			Debug.Log ("setBackButton - ErrorState");
			break;
		}

		Button tmp = backButton.GetComponent<Button> ();
		tmp.onClick.RemoveAllListeners ();
		tmp.onClick.AddListener(() => clickBack(true));
		tmp.onClick.AddListener(() => stateManager.setState(previousState));
	}


//-----------------------------------Button Show & Hide----------------------------------
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


//-----------------------------------Button Function----------------------------------
	private void clickBack(bool clicked){
		this.isBackClicked = clicked;
	}


//-----------------------------------Show & Hide Panel By State----------------------------------
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


	public void showBattleSkillPanel(){
		showNextButton ();
		hideBackButton ();

		if (isBackClicked)
			playBackToBattleSkillAnimation ();
		else {
			battleSkillOn = currentCharacter.battleSkillOn;
			battleSkillOff = currentCharacter.battleSkillOff;

			for (int i = 0; i < battleSkill.Length; i++) {

				battleSkill [i].sprite =  battleSkillOff[i];
				SkillButtonGestureManager sbgm = battleSkill [i].gameObject.GetComponent<SkillButtonGestureManager>() ;
				sbgm.skillOff = battleSkillOff [i];
				sbgm.skillOn = battleSkillOn [i];

				// 如果角色MP不夠，則封鎖技能按鈕，並呈現半透明
				if (currentCharacter.Mp < currentCharacter.skill [i].needMP) {
					sbgm.isLocked = true;
					battleSkill [i].color = new Color(1f,1f,1f,0.5f);
				}
			}

			characterHalf.sprite = currentCharacter.characterHalf;
			characterHP.text = "" + currentCharacter.Hp;
			characterMP.text = "" + currentCharacter.Mp;
			characterDEF.text = "" + currentCharacter.Def;

			playSwipeToBattleSkillAnimation ();
		}
	}

	public void showDiceRollingPanel(){
		hideNextButton ();
		showBackButton ();

		//if (isBackClicked)
		//	playBackToDiceRollingAnimation ();
		//else
			playSwipeToDiceRollingAnimation ();
	}

	public void showEnemyRollingPanel(){
		hideNextButton ();
		hideBackButton ();

		//if (isBackClicked)
		//	playBackToDiceRollingAnimation ();
		//else
		playSwipeToEnemyRollingAnimation ();
	}


//-----------------------------------Anitmation Trigger----------------------------------
	private void playOrderValueAnimation(){
		_animator.SetTrigger ("ShowOrderValue");
	}

	public void playCheckValueAnimation(){
		_animator.SetTrigger("ShowCheckValue");
	}

	private void playSwipeToBattleSkillAnimation(){
		_animator.SetTrigger ("SwipeToBattleSkill");
	}

	private void playBackToBattleSkillAnimation(){
		_animator.SetTrigger ("BackToBattleSkill");
	}


	private void playSwipeToDiceRollingAnimation(){
		_animator.SetTrigger ("SwipeToDiceRolling");
	}

//	private void playBackToDiceRollingAnimation(){
//		_animator.SetTrigger ("BackToDiceRolling");
//	}

	private void playSwipeToEnemyRollingAnimation(){
		_animator.SetTrigger ("SwipeToEnemyRolling");
	}


//-----------------------------------Other Local Function----------------------------------
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

	// SwipeToEnemyRollingAnimation 的最後一個frame呼叫
	private void enemyRollDice(){
		bcManager.rollDices();
	}
}
