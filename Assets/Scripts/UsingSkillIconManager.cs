using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class UsingSkillIconManager : MonoBehaviour {
	public TapGesture singleTap;
	public UITextManager textManager;
	public bool tapable = false;
	public int index;

	private SpriteRenderer icon;
	private string heroName ="Knight";


	// Use this for initialization
	void Start () {
		icon = this.gameObject.GetComponent<SpriteRenderer> ();

		singleTap.Tapped += (object sender, System.EventArgs e) => 
		{
			if(tapable && index != -1)
				textManager.showBattleSkillDetail (heroName, index, icon.sprite);		
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setUsingSkill(int index, Sprite image){
		this.index = index;
		icon.sprite = image;
		tapable = true;
	}

	public void setNoSkill(){
		this.index = -1;
		tapable = false;
		icon.sprite = null;
	}
}
