using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceMaster;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine.SceneManagement;

public class Loot : MonoBehaviour {

	public int lootType; // 0=HP; 1=MP; 2=GambleSkill; 3=Dice
	public int gambleSkillType; // 0~5
	public TransformGesture transformGesture;
	public LootManager lootmanager;
	public SpriteRenderer spriteRenderer;
	public Animator _animator;
	public Dice dice;

	public ScreenEffectManager screenEffect;

	private Vector3 initialPosition;
	private bool isIllegal = true;
	private BoxCollider boxCollider;
	public int characterIndex;
	public int diceIndex;

	void Awake () {
		boxCollider = this.GetComponent<BoxCollider> ();
	}

	// Use this for initialization
	void Start() {
		initialPosition = this.transform.position;

		transformGesture.TransformStarted += (object sender, System.EventArgs e) => 
		{
			if(lootType == 2)
				lootmanager.showGambleBag(true);

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

				if(lootType == 2)
					lootmanager.showGambleBag(false);
			}
			else
			{
					lootmanager.showConfirmPanel(this);
			}
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Character" && lootType == 0 || lootType == 1) {
			isIllegal = false;

			switch (other.gameObject.name) {
			case "Hero_piece1":
				characterIndex = 0;
				break;
			case "Hero_piece2":
				characterIndex = 1;
				break;
			case "Hero_piece3":
				characterIndex = 2;
				break;
			default:
				Debug.Log ("Loot - OnTriggerEnter Error");
				break;
			}
		} else if (other.gameObject.tag == "GambleBag" && lootType == 2) {
			isIllegal = false;
		} else if (other.gameObject.tag == "Dice" && lootType == 3) {
			isIllegal = false;

			switch(other.gameObject.name){
			case "Dice1":
				diceIndex = 0;
				break;
			case "Dice2":
				diceIndex = 1;
				break;
			case "Dice3":
				diceIndex = 2;
				break;
			default:
				Debug.Log("Loot - OnTriggerEnter Error");
				break;
			}
		}
	}

	void OnTriggerExit(Collider other){
		isIllegal = true;
	}

	public void unlockLootGesture(bool unlock){
		if (unlock)
			backToInitailPosition();
		
		transformGesture.enabled = unlock;
	}

	private void backToInitailPosition(){
		isIllegal = true;
		this.transform.position = initialPosition;
		boxCollider.size = new Vector3(1.84f, 2.0f, 0.2f);
	}

	public void playLootFadeOutAnimation(){
		_animator.SetTrigger ("LootFadeOut");
	}

	public void playLootShrinkAnimation(){
		_animator.applyRootMotion = false;
		_animator.SetTrigger ("LootShrink");
	}

	public void fadeOutScreen(){
		screenEffect.fullScreenFadeOut ();
	}
}
