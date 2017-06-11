using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class MonsterSkillButtonManager : MonoBehaviour {
	public TapGesture singleTap;
	public int index;
	public SpriteRenderer spriteRenderer;
	public UITextManager textManager;
	public string monsterName;


	// Use this for initialization
	void Start () {
		singleTap.Tapped += (object sender, System.EventArgs e) => 
		{
			textManager.showMonsterSkillDetail (monsterName, index, spriteRenderer.sprite);
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
