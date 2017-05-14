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

	public TextMesh currentCharacterHP;
	public TextMesh currentCharacterMP;
	public TextMesh currentCharacterDEF;
	public SpriteRenderer currentCharacterHalf;
	public SpriteRenderer[] battleSkill;

	public TextMesh[] characterHP;
	public TextMesh[] characterMP;
	public TextMesh[] characterDEF;

	public TextMesh monsterHP;
	public TextMesh monsterDEF;

	public Button nextButton;
	public Button backButton;

	public TextMesh[] gambleSkillTimesText;
	public SpriteRenderer[] gambleSkill;

	public TextMesh[] characterHurtValue;
	public TextMesh monsterHurtValue;

	private Character[] characters;
	private Monster monster;
	private Character currentCharacter;
	private Sprite[] battleSkillOn;
	private Sprite[] battleSkillOff;
	private Animator _animator;
	private bool isBackClicked = false; // 用來判斷是否back button，以此決定面板平移動畫要播哪個方向的
	private GambleSkillManager gsMananger = null;

	// Use this for initialization
	void Start () {
		_animator = this.GetComponent<Animator> ();
		gsMananger = (GambleSkillManager)FindObjectOfType (typeof(GambleSkillManager));
	}
	
	// Update is called once per frame
	void Update () {

		// 自動更新角色和怪物的狀態數值
		for (int i = 0; i < characters.Length; i++) {
			if (characters [i]) {
				characterHP [i].text = "" + characters [i].Hp;
				characterMP [i].text = "" + characters [i].Mp;
				characterDEF [i].text = "" + characters [i].Def; 
			}
		}
		if (monster) {
			monsterHP.text = "" + monster.Hp;
			monsterDEF.text = "" + monster.Def;
		}
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

		Button tmp = nextButton.GetComponent<Button> ();
		tmp.onClick.RemoveAllListeners ();
		tmp.GetComponent<Button>().onClick.AddListener(() => clickBack(false));

		switch (state) {
		case State.BattleState.RollOrder:
			nextState = State.BattleState.SelectBattleSkill;
			break;

		case State.BattleState.SelectBattleSkill:
			nextState = State.BattleState.PlayerRollBattleDice;
			tmp.GetComponent<Button>().onClick.AddListener(() => setBattleSkill());
			break;
		
		case State.BattleState.PlayerRollBattleDice:
			nextState = State.BattleState.SelectGambleSkill;
			break;

		case State.BattleState.EnemyRollBattleDice:
			nextState = State.BattleState.EnemyAttack;
			break;
		
		case State.BattleState.SelectGambleSkill:
			nextState = State.BattleState.PlayerRollBattleDice2;
			tmp.GetComponent<Button> ().onClick.AddListener (() => setGambleSkill ());
			break;

		case State.BattleState.PlayerRollBattleDice2:
			nextState = State.BattleState.PlayerAttack;
			break;

		case State.BattleState.EnemyAttack:
			nextState = State.BattleState.SelectBattleSkill;
			break;

		case State.BattleState.PlayerAttack:
			nextState = State.BattleState.SelectBattleSkill;
			break;

		default:
			Debug.Log ("setNextButton - ErrorState");
			break;
		}

		tmp.GetComponent<Button>().onClick.AddListener(() => stateManager.setState(nextState));
	}


	private void setBackButton(State.BattleState state){

		State.BattleState previousState = state;

		Button tmp = backButton.GetComponent<Button> ();
		tmp.onClick.RemoveAllListeners ();
		tmp.onClick.AddListener(() => clickBack(true));

		switch (state) {
		case State.BattleState.PlayerRollBattleDice:
			previousState = State.BattleState.SelectBattleSkill;
			break;

		case State.BattleState.PlayerRollBattleDice2:
			previousState = State.BattleState.SelectGambleSkill;
			break;

		default:
			Debug.Log ("setBackButton - ErrorState");
			break;
		}
			
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

	// 告知battle check manager 目前角色選用的技能 
	// (這個function設置在下一步按鈕上，當選完技能後，點擊下一步按鈕進入下個階段的同時，告知battle check manager)
	private void setBattleSkill(){
		Skill tmp = null;

		for (int i = 0; i < battleSkill.Length; i++) {
			SkillButtonGestureManager sbgm = battleSkill [i].gameObject.GetComponent<SkillButtonGestureManager>() ;

			if (sbgm.state) {
				tmp = currentCharacter.skill [i];
				currentCharacter.Mp -= currentCharacter.skill [i].needMP;
				break;
			}
		}

		bcManager.setUsingBattleSkill(tmp);
	}

	// 告知battle check manager 目前使用的賭技
	// (這個function設置在下一步按鈕上，當選完技能後，點擊下一步按鈕進入下個階段的同時，告知battle check manager)
	private void setGambleSkill(){
		int tmp = -1;

		for (int i = 0; i < gambleSkill.Length; i++) {
			SkillButtonGestureManager sbgm = gambleSkill [i].gameObject.GetComponent<SkillButtonGestureManager>() ;

			if (sbgm.state) {
				tmp = i;
				gsMananger.skillTimes [i]--;
				break;
			}
		}

		bcManager.setUsingGambleSkill(tmp);
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
				sbgm.state = false;

				// 如果角色MP不夠，則封鎖技能按鈕，並呈現半透明
				if (currentCharacter.Mp < currentCharacter.skill [i].needMP) {
					sbgm.isLocked = true;
					battleSkill [i].color = new Color(1f,1f,1f,0.5f);
				}
			}

			currentCharacterHalf.sprite = currentCharacter.characterHalf;
			currentCharacterHP.text = "" + currentCharacter.Hp;
			currentCharacterMP.text = "" + currentCharacter.Mp;
			currentCharacterDEF.text = "" + currentCharacter.Def;

			playSwipeToBattleSkillAnimation ();
		}
	}

	public void showDiceRollingPanel(){
		hideNextButton ();
		showBackButton ();

		playSwipeToDiceRollingAnimation ();
	}

	public void showEnemyRollingPanel(){
		hideNextButton ();
		hideBackButton ();

		playSwipeToEnemyRollingAnimation ();
	}

	public void showGambleSkillPanel(){
		showNextButton ();
		hideBackButton ();

		if (isBackClicked)
			playBackToGambleSkillAnimation ();
		else {
			int[] gambleSkillTimes = gsMananger.skillTimes;

			for (int i = 0; i < gambleSkillTimes.Length; i++) {
				SkillButtonGestureManager sbgm = gambleSkill [i].gameObject.GetComponent<SkillButtonGestureManager>() ;
				sbgm.state = false;
				// 如果技能次數=0，則封鎖技能按鈕，並呈現半透明
				if (gambleSkillTimes[i] == 0) {
					sbgm.isLocked = true;
					gambleSkill [i].color = new Color(1f,1f,1f,0.5f);
				}
				gambleSkillTimesText [i].text = "" + gambleSkillTimes[i];
			}
	
			playSwipeToGambleSkillAnimation ();
		}
	}

	public void showDiceRolling2Panel(){
		hideNextButton ();
		showBackButton ();

		playSwipeToDiceRolling2Animation ();
	}

	public void showEnemyAttack(){
		showNextButton (); // 應該先hide，戰鬥動畫演示完再show，但這邊還沒做戰鬥動畫，所以先show，來測試能不能進入下個回合
		hideBackButton ();

		playSwipeToAttackScreenAnimation ();
	}

	public void showPlayerAttack(){
		showNextButton (); // 應該先hide，戰鬥動畫演示完再show，但這邊還沒做戰鬥動畫，所以先show，來測試能不能進入下個回合
		hideBackButton ();

		playSwipeToAttackScreenAnimation ();
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


	private void playSwipeToEnemyRollingAnimation(){
		_animator.SetTrigger ("SwipeToEnemyRolling");
	}

	private void playSwipeToGambleSkillAnimation(){
		_animator.SetTrigger ("SwipeToGambleSkill");
	}

	private void playBackToGambleSkillAnimation(){
		_animator.SetTrigger ("BackToGambleSkill");
	}

	private void playSwipeToDiceRolling2Animation(){
		_animator.SetTrigger ("SwipeToDiceRolling2");
	}

	private void playSwipeToAttackScreenAnimation(){
		_animator.SetTrigger ("SwipeToAttackScreen");
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
