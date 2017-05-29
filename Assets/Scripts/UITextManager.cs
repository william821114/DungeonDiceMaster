using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextManager : MonoBehaviour {

	public Animator _animator;
	public Text information;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showText(string text){
		this.gameObject.SetActive (true);
		information.text = text;
		_animator.SetTrigger ("ShowPanel");
	}
	
	public void hide(){
		this.gameObject.SetActive (false);
	}
}
