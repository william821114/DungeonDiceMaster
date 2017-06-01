using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class FlickAreaGestureManager : MonoBehaviour {

	public FlickGesture flickGesture;
	public BattleCheckManager bcManager;

	// Use this for initialization
	void Start () {
		
		flickGesture.Flicked += (object sender, System.EventArgs e) => 
		{
			if(bcManager.isReadyToRoll)
			{
				bcManager.rollDices();
			}
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
