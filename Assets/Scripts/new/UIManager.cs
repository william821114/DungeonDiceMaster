using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public StateManager stateManager;
    public State.BattleState currentState;

    public BattleCheckManager bcManager;

    public TextMesh currentCharacterHP;
    public TextMesh currentCharacterMP;
    public TextMesh currentCharacterDEF;
    public SpriteRenderer currentCharacterHalf;
    public SpriteRenderer[] battleSkill;

    public GameObject mark;

    public TextMesh monsterHP;
    public TextMesh monsterDEF;

    public Button nextButton;

    public TextMesh[] gambleSkillTimesText;
    public SpriteRenderer[] gambleSkill;

    public Animator canvasAnimator;
    public Text battleSkillText;
    public Image battleSkillImage;

    private Monster monster;
    private Character currentCharacter;
    private Sprite[] battleSkillOn;
    private Sprite[] battleSkillOff;
    private Animator _animator;
    private DataManager dataManager;
    private Skill usingBattleSkill;
    private int usingGambleSkillIndex;

    // Use this for initialization

    void Awake() {
        _animator = this.GetComponent<Animator>();
    }
    void Start() {
        // 一開始先更新monsterUI一次
        updateMonsterUI();
        updateCharacterUI();
    }

    // Update is called once per frame
    void Update() {

    }

    //-----------------------------------Set Function----------------------------------
    public void setMonster(Monster m) {
        monster = m;
    }

    public void setCurrentCharacter(Character c) {
        currentCharacter = c;
    }

    public void setDataManager(DataManager dm) {
        dataManager = dm;
    }

    //-----------------------------------update Function----------------------------------
    public void updateMonsterUI()
    {
        monsterHP.text = "" + monster.Hp;
        monsterDEF.text = "" + monster.Def;
    }

    public void updateCharacterUI()
    {
        currentCharacterHP.text = "" + currentCharacter.Hp;
        currentCharacterMP.text = "" + currentCharacter.Mp;
        currentCharacterDEF.text = "" + currentCharacter.Def;
    }

    //-----------------------------------Button Function Set----------------------------------
    private void setNextButton(State.BattleState state) {

        State.BattleState nextState = state;

        Button tmp = nextButton.GetComponent<Button>();
        tmp.onClick.RemoveAllListeners();

        switch (state) {
            case State.BattleState.SelectBattleSkill:
                nextState = State.BattleState.PlayerRollBattleDice;
                tmp.GetComponent<Button>().onClick.AddListener(() => setBattleSkill());
                break;

            case State.BattleState.PlayerRollBattleDice:
                nextState = State.BattleState.SelectGambleSkill;
                //tmp.GetComponent<Button>().onClick.AddListener(() => processBattleSkillMP());
                break;

            case State.BattleState.EnemyRollBattleDice:
                nextState = State.BattleState.EnemyAttack;
                break;

            case State.BattleState.SelectGambleSkill:
                nextState = State.BattleState.PlayerRollBattleDice2;
                tmp.GetComponent<Button>().onClick.AddListener(() => setGambleSkill());
                break;

            case State.BattleState.PlayerRollBattleDice2:
                nextState = State.BattleState.PlayerAttack;
                tmp.GetComponent<Button>().onClick.AddListener(() => processGambleSkillTimes());
                break;

            case State.BattleState.EnemyAttack:
                nextState = State.BattleState.SelectBattleSkill;
                break;

            case State.BattleState.PlayerAttack:
                nextState = State.BattleState.SelectBattleSkill;
                break;

            default:
                Debug.Log("setNextButton - ErrorState");
                break;
        }

        tmp.GetComponent<Button>().onClick.AddListener(() => stateManager.setState(nextState));
    }


    //-----------------------------------Button Show & Hide----------------------------------
    public void showNextButton() {
        setNextButton(currentState);
        nextButton.gameObject.SetActive(true);
    }

    public void hideNextButton() {
        nextButton.gameObject.SetActive(false);
    }


    //-----------------------------------Button Function----------------------------------
    // 告知battle check manager 目前角色選用的技能 
    // (這個function設置在下一步按鈕上，當選完技能後，點擊下一步按鈕進入下個階段的同時，告知battle check manager)
    private void setBattleSkill() {
        usingBattleSkill = null;

        for (int i = 0; i < battleSkill.Length; i++) {
            SkillButtonGestureManager sbgm = battleSkill[i].gameObject.GetComponent<SkillButtonGestureManager>();

            if (sbgm.state) {
                usingBattleSkill = currentCharacter.skill[i];
                break;
            }
        }

        bcManager.setUsingBattleSkill(usingBattleSkill);
    }

    /*private void processBattleSkillMP(){
		if(usingBattleSkill)
			//currentCharacter.Mp -= usingBattleSkill.needMP;

		Debug.Log ("XXXX");
	}*/

    // 告知battle check manager 目前使用的賭技
    // (這個function設置在下一步按鈕上，當選完技能後，點擊下一步按鈕進入下個階段的同時，告知battle check manager)
    private void setGambleSkill() {
        usingGambleSkillIndex = -1;

        for (int i = 0; i < gambleSkill.Length; i++) {
            SkillButtonGestureManager sbgm = gambleSkill[i].gameObject.GetComponent<SkillButtonGestureManager>();

            if (sbgm.state) {
                usingGambleSkillIndex = i;
                break;
            }
        }
        bcManager.setUsingGambleSkill(usingGambleSkillIndex);
        //stateManager.setUsingGambleSkill(usingGambleSkillIndex);
    }

    private void processGambleSkillTimes() {
        if (usingGambleSkillIndex != -1)
            dataManager.gambleSkillTimes[usingGambleSkillIndex]--;
    }


    //-----------------------------------Show & Hide Panel By State----------------------------------
    public void showBattleSkillPanel() {
        showNextButton();

        battleSkillOn = currentCharacter.battleSkillOn;
        battleSkillOff = currentCharacter.battleSkillOff;

        for (int i = 0; i < battleSkill.Length; i++) {

            battleSkill[i].sprite = battleSkillOff[i];
            SkillButtonGestureManager sbgm = battleSkill[i].gameObject.GetComponent<SkillButtonGestureManager>();
            sbgm.skillOff = battleSkillOff[i];
            sbgm.skillOn = battleSkillOn[i];
            sbgm.state = false;

            // 如果角色MP不夠，則封鎖技能按鈕，並呈現半透明
            if (currentCharacter.Mp < currentCharacter.skill[i].needMP) {
                sbgm.isLocked = true;
                battleSkill[i].color = new Color(1f, 1f, 1f, 0.5f);
                Debug.Log("XXXX");
            }
        }

        currentCharacter.transform.position = mark.transform.position;
        currentCharacter.transform.rotation = mark.transform.rotation;
        currentCharacter.gameObject.transform.parent = mark.transform;
        //SpriteRenderer tempRenderer = currentCharacter.GetComponentInChildren<SpriteRenderer> ();
        //tempRenderer.enabled = true;
        //currentCharacterHalf.sprite = currentCharacter.characterHalf;
        currentCharacterHP.text = "" + currentCharacter.Hp;
        currentCharacterMP.text = "" + currentCharacter.Mp;
        currentCharacterDEF.text = "" + currentCharacter.Def;

        playShowBattleSkillAnimation();
    }

    public void showDiceRollingPanel() {
        hideNextButton();

        playSwipeToDiceRollingAnimation();
    }

    public void showEnemyRollingPanel() {
        hideNextButton();

        playSwipeToEnemyRollingAnimation();
    }

    public void showGambleSkillPanel() {
        showNextButton();

        int[] gambleSkillTimes = dataManager.gambleSkillTimes;

        for (int i = 0; i < gambleSkillTimes.Length; i++) {
            SkillButtonGestureManager sbgm = gambleSkill[i].gameObject.GetComponent<SkillButtonGestureManager>();
            sbgm.state = false;
            // 如果技能次數=0，則封鎖技能按鈕，並呈現半透明
            if (gambleSkillTimes[i] == 0) {
                sbgm.isLocked = true;
                gambleSkill[i].color = new Color(1f, 1f, 1f, 0.5f);
            }
            gambleSkillTimesText[i].text = "" + gambleSkillTimes[i];
        }

        playSwipeToGambleSkillAnimation();
    }

    public void showDiceRolling2Panel() {
        hideNextButton();
        //showBackButton ();

        if (usingGambleSkillIndex == -1) {
            stateManager.setState(State.BattleState.PlayerAttack);
        }
        else {
            playSwipeToDiceRolling2Animation();
        }
    }

    public void showEnemyAttack() {
        showNextButton(); // 應該先hide，戰鬥動畫演示完再show，但這邊還沒做戰鬥動畫，所以先show，來測試能不能進入下個回合

        playSwipeToAttackScreenAnimation();
    }

    public void showPlayerAttack() {
        SkillEffect se = bcManager.getBattleSkillEffect();

        if (se == null)
        {
            // 沒使用技能
            Debug.Log("普通攻擊動畫!");
        }
        else
        {
            if (se.isSkillActivated)
            {
                // 技能發動成功
                battleSkillText.text = usingBattleSkill.skillName_ch;
                playSkillActivatedAnimation();
                Debug.Log("技能動畫! 技能成功");
            }
            else
            {
                // 技能沒發動成功
                Debug.Log("普通攻擊動畫! 技能失敗");
            }
        }

        if (usingGambleSkillIndex == -1)
            playSwipeToAttackScreen2Animation();
        else
            playSwipeToAttackScreenAnimation();

    }

    public void showMonsterHurt()
    {
        //monster.check 裡已經把這回合受到的傷害數字更新到hurt value了
        playMonsterHurtAnimation();
    }

    public void showPlayerHurt()
    {
        playPlayerHurtAnimation();
    }


    //-----------------------------------Anitmation Trigger----------------------------------
    private void playShowBattleSkillAnimation() {
        _animator.SetTrigger("ShowBattleSkill");
    }

    private void playSwipeToDiceRollingAnimation() {
        _animator.SetTrigger("SwipeToDiceRolling");
    }

    private void playSwipeToEnemyRollingAnimation() {
        _animator.SetTrigger("SwipeToEnemyRolling");
    }

    private void playSwipeToGambleSkillAnimation() {
        _animator.SetTrigger("SwipeToGambleSkill");
    }

    private void playSwipeToDiceRolling2Animation() {
        _animator.SetTrigger("SwipeToDiceRolling2");
    }

    private void playSwipeToAttackScreenAnimation() {
        _animator.SetTrigger("SwipeToAttackScreen");
    }

    private void playSwipeToAttackScreen2Animation() {
        _animator.SetTrigger("SwipeToAttackScreen2");
    }

    private void playSkillActivatedAnimation()
    {
        canvasAnimator.SetTrigger("skillActivated");
    }

    private void playMonsterHurtAnimation()
    {
        Animator monsterAnimator = monster.gameObject.GetComponent<Animator>();
        monsterAnimator.SetTrigger("hurt");
    }

    private void playPlayerHurtAnimation()
    {
        Animator playerAnimator = currentCharacter.gameObject.GetComponent<Animator>();
        playerAnimator.SetTrigger("hurt");
    }


//-----------------------------------Other Local Function----------------------------------
	// SwipeToEnemyRollingAnimation 的最後一個frame呼叫
	private void enemyRollDice(){
		bcManager.rollDices();
	}
}
	
