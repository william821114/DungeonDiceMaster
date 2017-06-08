using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueTextManager : MonoBehaviour {

	public Animator _animator;
	public TextMesh valueText;

	// Use this for initialization
	void Start () {
		//_animator = this.GetComponent<Animator> ();
		//valueText = this.gameObject.GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showCheckValue(int checkValue){
		this.gameObject.SetActive (true);
		valueText.text = "~" + checkValue + "~";
		_animator.SetTrigger ("ShowCheckValueText");
	} 

	public void showCheckValue2(string checkValueText){
		this.gameObject.SetActive (true);
		valueText.text = checkValueText;
		_animator.SetTrigger ("ShowCheckValueText");
	} 

	public void showGambleSkillValue(int diceValue){
		this.gameObject.SetActive (true);
		valueText.text = "" + diceValue;
		_animator.SetTrigger ("ShowGambleSkillValue");
	}

	public void hide(){
		this.gameObject.SetActive (false);
	}

	public void clean()
	{
		Destroy (this.gameObject);
	}
}
