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
	public Button acceptButton;

	public int hpRecoverValue;
	public  int mpRecoverValue;

	private SpriteRenderer[] characterPieces;
	private List<int> randomNumbers = new List<int>();
	private DataManager dataManager;
	private Dice[] dices;


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
			loots [i].lootType = PickNumber ();
			loots [i].showLoot ();
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showCharacterDice(int characterIndex)
	{
		Dice[] d = dataManager.team [characterIndex].getBattleDice();
		dices = new Dice[d.Length];

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

			dices [i] = GameObject.Instantiate (d [i], new Vector3 (0f,0f,0f), Quaternion.identity) as Dice;
			dices [i].transform.parent = dicePanel[i].transform;
			dices [i].transform.localPosition = Vector3.zero;
		}
	}

	public void destroyAllDice(){
		for (int i = 0; i < dices.Length; i++) {
			if(dices[i])
				Destroy (dices [i].gameObject);
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

	public void showConfirmPanel(int lootType, int characterIndex, int gambleSkillIndex, int diceIndex, Dice dice){
		for (int i = 0; i < loots.Length; i++) {
			loots [i].unlockLootGesture (false);
		}

		acceptButton.onClick.RemoveAllListeners ();

		switch (lootType) {
		case 0:
			acceptButton.onClick.AddListener(() => this.hpRecover(characterIndex) );
			confirmPanel.showText ("Use Hp Potion on " + dataManager.team[characterIndex].unitName + " ?");
			break;
		case 1:
			acceptButton.onClick.AddListener(() => this.mpRecover(characterIndex) );
			confirmPanel.showText ("Use Mp Potion on " + dataManager.team[characterIndex].unitName + " ?");
			break;
		case 2:
			acceptButton.onClick.AddListener(() => this.addGambleSkillTimes(gambleSkillIndex) );
			confirmPanel.showText ("Get This Gamble Skill ?");
			break;
		case 3:
			acceptButton.onClick.AddListener(() => this.changeDice(characterIndex, diceIndex, dice) );
			confirmPanel.showText ("Switch the Dice ? \n (Old dice will be destroyed !)");
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
		SceneManager.LoadScene("Start", LoadSceneMode.Single);
	}

	public void mpRecover(int characterIndex){
		Character c = dataManager.team [characterIndex];

		c.Mp += mpRecoverValue;

		if (c.Mp >= c.MaxMp)
			c.Mp = c.MaxMp;

		confirmPanel.hide();
		SceneManager.LoadScene("Start", LoadSceneMode.Single);
	}

	public void addGambleSkillTimes(int gambleSkillIndex){
		dataManager.gambleSkillTimes [gambleSkillIndex]++;

		confirmPanel.hide();
		SceneManager.LoadScene("Start", LoadSceneMode.Single);
	}

	public void changeDice(int characterIndex, int diceIndex, Dice dice){
		dataManager.team [characterIndex].battleDice [diceIndex] = dice;

		confirmPanel.hide();
		SceneManager.LoadScene("Start", LoadSceneMode.Single);
	}

	public void freeAllLoots(){
		confirmPanel.hide ();
		for (int i = 0; i < loots.Length; i++) {
			loots [i].unlockLootGesture (true);
		}
	}
}
