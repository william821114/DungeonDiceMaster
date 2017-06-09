using UnityEngine;
using UnityEngine.SceneManagement;

public class tempChangeScene : MonoBehaviour
{
	public void changeBattleScene()
	{
		// Only specifying the sceneName or sceneBuildIndex will load the scene with the Single mode
		SceneManager.LoadScene("Battle", LoadSceneMode.Single);
	}

	public void changeLootScene()
	{
		SceneManager.LoadScene("Loot", LoadSceneMode.Single);
	}

	public void changeStartScene()
	{
		SceneManager.LoadScene("Start", LoadSceneMode.Single);
	}

	public void changeBeforeBattleScene()
	{
		SceneManager.LoadScene("BeforeBattle", LoadSceneMode.Single);
	}

	public void changeGameOverScene()
	{
		SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
	}
}