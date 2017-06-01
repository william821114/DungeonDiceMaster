using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceMaster;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LootManager : MonoBehaviour {
	public GameObject[] characterButton;
	public GameObject gamebleBag;
	public Loot[] loots;
	public GameObject[] dicePanel;
	public TextMesh[] hp;
	public TextMesh[] mp;
	public UITextManager confirmPanel;
	public UITextManager detailPanel;
	public Button acceptButton;

	public int hpRecoverValue;
	public int mpRecoverValue;

	public Sprite hpPotion;
	public Sprite mpPotion;
	public Sprite[] gambleSkill;
	public Dice[] lootDices;

	public ParticleSystem healEffect;

	private SpriteRenderer[] characterPieces;
	private List<int> randomNumbers = new List<int>();
	private DataManager dataManager;
	private Dice[] characterDices;
	private Loot loot;


	void Awake () {
		characterPieces = new SpriteRenderer[characterButton.Length];

		for (int i = 0; i < characterButton.Length; i++) {
			characterPieces [i] = characterButton [i].GetComponent<SpriteRenderer> ();
		}
	}

	// Use this for initialization
	void Start () {
		dataManager = (DataManager)FindObjectOfType (typeof(DataManager));

		for (int i = 0; i < dataManager.team.Length; i++) {
			hp [i].text = "" + dataManager.team [i].Hp;
			mp [i].text = "" + dataManager.team [i].Mp;
		}

		showCharacterDice (1);

		for (int i = 0; i < 4; i++)
			randomNumbers.Add(i);

		for (int i = 0; i < 3; i++) {
			showLoot(i);
			//loots [i].lootType = PickNumber ();
			//loots [i].showLoot ();
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void showLoot(int i){
		int lootType = PickNumber ();
		loots [i].lootType = lootType;
		SpriteRenderer sr = loots [i].spriteRenderer;

		switch (lootType) {
		case 0:
			sr.sprite = hpPotion;
			break;
		case 1:
			sr.sprite = mpPotion;
			break;
		case 2:
			int gambleSkillType = Random.Range (0, 5);
			loots [i].gambleSkillType = gambleSkillType;
			sr.sprite = gambleSkill [gambleSkillType];
			break;
		case 3:
			Dice d = lootDices [Random.Range (0, lootDices.Length - 1)];
			loots[i].dice = d;

			// 鎖定移動
			Rigidbody rigidbodyTemp = d.GetComponent<Rigidbody> ();
			rigidbodyTemp.constraints = RigidbodyConstraints.FreezeAll;

			// 骰子產生時不要旋轉
			Spinner spinnerTemp = d.GetComponent<Spinner> ();
			spinnerTemp.triggerOnStart = false;

			// 稍微調大骰子
			//Transform transformTemp = d.GetComponent<Transform> ();
			//transformTemp.localScale = new Vector3 (0.8f, 0.8f, 0.8f);

			d = GameObject.Instantiate (d, new Vector3 (0f, 0f, 0f), Quaternion.identity) as Dice;
			d.transform.parent = loots[i].gameObject.transform;
			d.transform.localPosition = Vector3.zero;

			break;
		default:
			Debug.Log ("showLoot - Error");
			break;
		}
	}

	public void showCharacterDice(int characterIndex)
	{
		Dice[] d = dataManager.team [characterIndex].getBattleDice();
		characterDices = new Dice[d.Length];

		for (int i = 0; i < d.Length; i++) {

			// 鎖定移動
			Rigidbody rigidbodyTemp = d [i].GetComponent<Rigidbody> ();
			rigidbodyTemp.constraints = RigidbodyConstraints.FreezeAll;

			// 骰子產生時不要旋轉
			Spinner spinnerTemp = d [i].GetComponent<Spinner> ();
			spinnerTemp.triggerOnStart = false;

			// 稍微調大骰子
			//Transform transformTemp = d [i].GetComponent<Transform> ();
			//transformTemp.localScale = new Vector3 (0.8f, 0.8f, 0.8f);

			characterDices [i] = GameObject.Instantiate (d [i], new Vector3 (0f,0f,0f), Quaternion.identity) as Dice;
			characterDices [i].transform.parent = dicePanel[i].transform;
			characterDices [i].transform.localPosition = Vector3.zero;
		}
	}

	public void destroyAllDice(){
		for (int i = 0; i < characterDices.Length; i++) {
			if(characterDices[i])
				Destroy (characterDices [i].gameObject);
		}
	}

	private int PickNumber(){
		if (randomNumbers.Count <= 0)
			return -1; // No numbers left

		int index = Random.Range(0, randomNumbers.Count);
		int value = randomNumbers [index];
		randomNumbers.RemoveAt (index);
		Debug.Log (value);
		return value;
	}

	public void showGambleBag(bool show)	{
		for (int i = 0; i < characterButton.Length; i++) {
			characterButton [i].SetActive (!show);
		}

		gamebleBag.SetActive (show);
	}

	public void showConfirmPanel(Loot l){
		this.loot = l;
		for (int i = 0; i < loots.Length; i++) {
			loots [i].unlockLootGesture (false);
		}

		acceptButton.onClick.RemoveAllListeners ();

		switch (loot.lootType) {
		case 0:
			acceptButton.onClick.AddListener(() => this.hpRecover(loot.characterIndex) );
			confirmPanel.showText ("Use Hp Potion on " + dataManager.team[loot.characterIndex].unitName + " ?");
			break;
		case 1:
			acceptButton.onClick.AddListener(() => this.mpRecover(loot.characterIndex) );
			confirmPanel.showText ("Use Mp Potion on " + dataManager.team[loot.characterIndex].unitName + " ?");
			break;
		case 2:
			acceptButton.onClick.AddListener(() => this.addGambleSkillTimes(loot.gambleSkillType) );
			confirmPanel.showText ("Get This Gamble Skill ?");
			break;
		case 3:
			acceptButton.onClick.AddListener(() => this.changeDice(loot.characterIndex, loot.diceIndex, loot.dice) );
			confirmPanel.showText ("Switch the Dice ? \n (Old dice will be destroyed !)");
			break;
		default:
			Debug.Log("LootManager - showConfirmPanel Error");
			break;
		}
	}

	public void showDetailPanel(Loot l){
		this.loot = l;
		for (int i = 0; i < loots.Length; i++) {
			loots [i].unlockLootGesture (false);
		}

		acceptButton.onClick.RemoveAllListeners ();

		switch (loot.lootType) {
		case 0:
			detailPanel.showLootDetail (loot.lootType, loot.spriteRenderer.sprite);
			break;
		case 1:
			detailPanel.showLootDetail (loot.lootType, loot.spriteRenderer.sprite);
			break;
		case 2:
			detailPanel.showGambleSkillDetail (loot.gambleSkillType, loot.spriteRenderer.sprite);
			break;
		case 3:
			//detailPanel.showDiceDetail (loot.dice);
			freeAllLoots();
			break;
		default:
			Debug.Log("LootManager - showConfirmPanel Error");
			break;
		}
	}

	public void hpRecover(int characterIndex){
		Character c = dataManager.team [characterIndex];

		c.Hp += hpRecoverValue;

		if (c.Hp >= c.MaxHp)
			c.Hp = c.MaxHp;
		
		confirmPanel.hide();
		loot.playLootFadeOutAnimation ();

		healEffect.transform.position = characterButton [characterIndex].transform.position - new Vector3 (0f, 2.5f, 0f);
		var main = healEffect.main;
		main.startColor = new ParticleSystem.MinMaxGradient(new Color32 (180,255,180,255));
		healEffect.gameObject.SetActive (true);
		healEffect.Play ();
	}

	public void mpRecover(int characterIndex){
		Character c = dataManager.team [characterIndex];

		c.Mp += mpRecoverValue;

		if (c.Mp >= c.MaxMp)
			c.Mp = c.MaxMp;

		confirmPanel.hide();
		loot.playLootFadeOutAnimation ();

		healEffect.transform.position = characterButton [characterIndex].transform.position - new Vector3 (0f, 2.5f, 0f);
		var main = healEffect.main;
		main.startColor = new ParticleSystem.MinMaxGradient(new Color32 (207,75,248,255));
		healEffect.gameObject.SetActive (true);
		healEffect.Play ();
	}

	public void addGambleSkillTimes(int gambleSkillIndex){
		dataManager.gambleSkillTimes [gambleSkillIndex]++;

		confirmPanel.hide();
		loot.playLootShrinkAnimation ();
	}

	public void changeDice(int characterIndex, int diceIndex, Dice dice){
		dataManager.team [characterIndex].battleDice [diceIndex] = dice;

		confirmPanel.hide();
		SceneManager.LoadScene("Start", LoadSceneMode.Single);
	}

	public void freeAllLoots(){
		confirmPanel.hide ();
		showGambleBag (false);

		for (int i = 0; i < loots.Length; i++) {
			loots [i].unlockLootGesture (true);
		}
	}
}
