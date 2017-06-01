using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class HeroChooseButtonManager : MonoBehaviour {
	public TapGesture singleTap;

	public HeroChooseButtonManager[] otherButtons;
	public bool state = false;
	public bool isDead = false;

	public int characterIndex;

	public ChooseHeroManager chooseHeroManager;

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
					foreach (HeroChooseButtonManager characterButton in otherButtons) {
						characterButton.state = false;
					}

					chooseHeroManager.setHero(characterIndex);
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
