using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textRendererChange2 : MonoBehaviour {

	private MeshRenderer mr;

	// Use this for initialization
	void Start () {
		mr = this.GetComponent<MeshRenderer> ();
		mr.sortingLayerName = "UI";
		mr.sortingOrder = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
