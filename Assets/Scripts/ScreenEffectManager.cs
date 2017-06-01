using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenEffectManager : MonoBehaviour {

	private Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = this.GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void fullScreenFadeOut()
	{
		_animator.SetTrigger ("FullScreenFadeOut");
	}

	public void loadStartScene(){
		SceneManager.LoadScene("Start", LoadSceneMode.Single);
	}
}
