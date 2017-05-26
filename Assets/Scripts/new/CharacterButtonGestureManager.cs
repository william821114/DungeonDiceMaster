using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class CharacterButtonGestureManager : MonoBehaviour {

	public TapGesture singleTap;

	public CharacterButtonGestureManager[] otherButtons;
	public bool state = false;
	public bool isDead = false;

	public LootManager lootManager;
	public int index;

	private SpriteRenderer image;

	// Use this for initialization
	void Start () {
		image = this.gameObject.GetComponent<SpriteRenderer> ();

		singleTap.Tapped += (object sender, System.EventArgs e) => 
		{
			if(!isDead)
			{
				if(!state){
					state = true;
					foreach (CharacterButtonGestureManager characterButton in otherButtons) {
						characterButton.state = false;
					}

					lootManager.destroyAllDice();
					lootManager.showCharacterDice(index);
				}
			}
		};
	}
	
	// Update is called once per frame
	void Update () {
		if(state){
			image.color = new Color (1f, 1f, 1f, 1f);
		} else{
			image.color = new Color (1f, 1f, 1f, 0.5f);
		}
	}

}
