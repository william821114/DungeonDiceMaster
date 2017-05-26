using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using TouchScript.Hit;

public class DragManager : MonoBehaviour {
	public TransformGesture transformGesture;

	// Use this for initialization
	void Start () {
		transformGesture.TransformStarted += (object sender, System.EventArgs e) => 
		{
			//TouchHit hit;
			//if(transformGesture.GetTargetHitResult(out hit))
			//{
			//	hit.RaycastHit.collider.gameObject.SetActive(false);
			//}

		};

		transformGesture.Transformed += (object sender, System.EventArgs e) => 
		{
			//TouchHit hit;
			//if(transformGesture.GetTargetHitResult(out hit))
			//{
			//	hit.RaycastHit.collider.gameObject.SetActive(false);
			//}
			this.transform.position += transformGesture.DeltaPosition; 
		};

		transformGesture.TransformCompleted += (object sender, System.EventArgs e) => 
		{
			
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
