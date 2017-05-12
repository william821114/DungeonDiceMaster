using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class SkillButtonGestureManager : MonoBehaviour {

	public TapGesture singleTap;
	public LongPressGesture longPress;

	public Sprite skillON;
	public Sprite skillOff;

	private bool state = false;
	private SpriteRenderer icon;

	// Use this for initialization
	void Start () {

		icon = this.gameObject.GetComponent<SpriteRenderer> ();

		singleTap.Tapped += (object sender, System.EventArgs e) => 
		{
			if(state){
				icon.sprite = skillOff;
				state = false;
			} else{
				icon.sprite = skillON;
				state = true;
			}
		};


		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
