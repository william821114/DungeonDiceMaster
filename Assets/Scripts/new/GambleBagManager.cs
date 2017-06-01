using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleBagManager : MonoBehaviour {

	private SpriteRenderer image;

	// Use this for initialization
	void Start () {
		image = this.gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		image.color = new Color (1f, 1f, 1f, 1f);
	}

	void OnTriggerExit(Collider other) {
		image.color = new Color (1f, 1f, 1f, 0.5f);
	}
}
