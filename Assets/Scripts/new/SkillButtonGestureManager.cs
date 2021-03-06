﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class SkillButtonGestureManager : MonoBehaviour {

	public TapGesture singleTap;
	public LongPressGesture longPress;

	public Sprite skillOn;
	public Sprite skillOff;

	public SkillButtonGestureManager[] otherButtons;
	public bool state = false;
	public bool isLocked = false;
	public int index;

	public UIManager uimanager;
	public UsingSkillIconManager usim;

	private SpriteRenderer icon;

	// Use this for initialization
	void Start () {

		icon = this.gameObject.GetComponent<SpriteRenderer> ();

		singleTap.Tapped += (object sender, System.EventArgs e) => 
		{
			if(!isLocked)
			{
				if(state){
					state = false;
					usim.setNoSkill();
				} else{
					state = true;
					usim.setUsingSkill(index, skillOn);
					foreach (SkillButtonGestureManager skillButton in otherButtons) {
						skillButton.state = false;
					}
				}
			}
		};

		longPress.LongPressed += (object sender, System.EventArgs e) => 
		{
			if(!isLocked)
				uimanager.showSkillDetailPanel(index);
		};
		
	}
	
	// Update is called once per frame
	void Update () {
		if(state){
			icon.sprite = skillOn;
		} else{
			icon.sprite = skillOff;
		}
	}
}
