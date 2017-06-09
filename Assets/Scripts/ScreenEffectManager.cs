using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenEffectManager : MonoBehaviour {

	public int nextSceneToLoad; // 0=Start 1=ChooseHero 2=Battle 3=Loot 4=GameOver

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

	public void fadeOutToGameOver(){
		_animator.SetTrigger ("FadeOutToGameOver");
	}

	public void fadeOutToLoot(){
		_animator.SetTrigger ("FadeOutToLoot");
	}
		
	public void loadScene(){
		switch (nextSceneToLoad) {
		case 0:
			destroyAllDontDestroyOnLoadObjects ();
			SceneManager.LoadScene("Start", LoadSceneMode.Single);
			break;
		case 1:
			SceneManager.LoadScene("BeforeBattle", LoadSceneMode.Single);
			break;
		case 2:
			SceneManager.LoadScene("Battle", LoadSceneMode.Single);
			break;
		case 3:
			SceneManager.LoadScene("Loot", LoadSceneMode.Single);
			break;
		case 4:
			SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
			break;
		default:
			break;
		}
	}

	public void loadGameOverScene(){
		SceneManager.LoadScene ("GameOver", LoadSceneMode.Single);
	}

	public void loadLootScene(){
		SceneManager.LoadScene("Loot", LoadSceneMode.Single);
	}

	private void destroyAllDontDestroyOnLoadObjects(){
		Character[] team = FindObjectsOfType (typeof(Character)) as Character[];
		for (int i = 0; i < team.Length; i++) {
			if(team[i])
				Destroy (team [i].gameObject);
		}

		DataManager dataManager = (DataManager)FindObjectOfType (typeof(DataManager));
		if(dataManager)
			Destroy (dataManager.gameObject);
	}
}
