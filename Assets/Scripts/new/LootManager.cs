using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceMaster;

public class LootManager : MonoBehaviour {
	public SpriteRenderer[] characterPieces;
	public Loot[] loots;
	public GameObject[] dicePanel;

	private List<int> randomNumbers = new List<int>();
	private DataManager dataManager;
	private Dice[] dices;

	// Use this for initialization
	void Start () {
		dataManager = (DataManager)FindObjectOfType (typeof(DataManager));
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
		
}
