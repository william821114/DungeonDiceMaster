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
	private int hpRecoverValue = 10;
	private int mpRecoverValue = 3;
	private Vector3 initialPosition;

	void Awake () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start() {
		initialPosition = this.transform.position;

		transformGesture.TransformStarted += (object sender, System.EventArgs e) => 
		{
			//TouchHit hit;
			//if(transformGesture.GetTargetHitResult(out hit))
			//{
			//	hit.RaycastHit.collider.gameObject.SetActive(false);
			//}

		};

		transformGesture.Transformed += (object sender, System.EventArgs e) => 
		{
			this.transform.position += transformGesture.DeltaPosition; 
		};

		transformGesture.TransformCompleted += (object sender, System.EventArgs e) => 
		{
			//Debug.Log(transformGesture.GetTargetHitResult());

			this.transform.position = initialPosition;
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
}
