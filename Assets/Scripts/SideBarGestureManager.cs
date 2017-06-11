using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class SideBarGestureManager : MonoBehaviour {

	public TapGesture singleTap;
	private Animator _animator;
	private bool isShowed =false;

	// Use this for initialization
	void Start () {
		_animator = this.GetComponent<Animator> ();

		singleTap.Tapped += (object sender, System.EventArgs e) => 
		{
			if(isShowed)
			{
				_animator.SetTrigger("HideBar");
				isShowed = false;
			}
			else
			{
				_animator.SetTrigger("ShowBar");
				isShowed = true;
			}
				
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
