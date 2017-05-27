using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceMaster;
using TouchScript.Gestures;
using TouchScript.Hit;

public class Loot : MonoBehaviour {

	public int lootType; // 0=HP; 1=MP; 2=GambleSkill; 3=Dice
	public int gambleSkillType; // 0~5
	public Sprite hpPotion;
	public Sprite mpPotion;
	public Sprite[] gambleSkill;
	public Dice[] dices;
	public TransformGesture transformGesture;
	public LootManager lootmanager;

	private SpriteRenderer spriteRenderer;
	private Vector3 initialPosition;
	private bool isIllegal = true;
	private BoxCollider boxCollider;

	void Awake () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		boxCollider = this.GetComponent<BoxCollider>();

	}

	// Use this for initialization
	void Start() {
		initialPosition = this.transform.position;

		transformGesture.TransformStarted += (object sender, System.EventArgs e) => 
		{
			boxCollider.size = new Vector3(0.2f, 0.2f, 0.2f); 
		};

		transformGesture.Transformed += (object sender, System.EventArgs e) => 
		{
			this.transform.position += transformGesture.DeltaPosition; 
		};

		transformGesture.TransformCompleted += (object sender, System.EventArgs e) => 
		{
			if(isIllegal)
			{
				this.transform.position = initialPosition;
				boxCollider.size = new Vector3(1.84f, 2.0f, 0.2f);
			}
			else
			{
				switch(lootType){
				case 0:
					break;
				case 1:
					break;
				case 2:
					break;
				case 3:
					break;
				default:
					Debug.Log("Loot - TransformCompleted Error");
						break;
				}
			}
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showLoot(){
		switch (lootType) {
		case 0:
			spriteRenderer.sprite = hpPotion;
			break;
		case 1:
			spriteRenderer.sprite = mpPotion;
			break;
		case 2:
			gambleSkillType = Random.Range (0, 5);
			spriteRenderer.sprite = gambleSkill[gambleSkillType];
			break;
		case 3:
			Dice d = dices [Random.Range (0, dices.Length - 1)];

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
			d.transform.parent = this.gameObject.transform;
			d.transform.localPosition = Vector3.zero;

			break;
		default:
			Debug.Log ("showLoot - Error");
			break;
		}
	}

	public void backToInitailPosition(){
		isIllegal = true;
		this.transform.position = initialPosition;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Character" && lootType == 0 || lootType == 1) {
			isIllegal = false;
			Debug.Log (other.gameObject.name);
		} else if (other.gameObject.tag == "Dice" && lootType == 3)
			isIllegal = false;
	}

	void OnTriggerExit(Collider other){
		isIllegal = true;
	}
}
