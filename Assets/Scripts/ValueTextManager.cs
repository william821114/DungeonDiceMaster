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

	public void hide(){
		this.gameObject.SetActive (false);
	}

	public void clean()
	{
		Destroy (this.gameObject);
	}
}
