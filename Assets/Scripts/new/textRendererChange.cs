using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textRendererChange : MonoBehaviour {

	private MeshRenderer mr;

	// Use this for initialization
	void Start () {
		mr = this.GetComponent<MeshRenderer> ();
		mr.sortingLayerName = "UI";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
