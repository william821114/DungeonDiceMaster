using UnityEngine;
using UnityEngine.SceneManagement;

public class tempChangeScene : MonoBehaviour
{
	public void changeBattleScene()
	{
		// Only specifying the sceneName or sceneBuildIndex will load the scene with the Single mode
		SceneManager.LoadScene("Battle", LoadSceneMode.Single);
	}

	public void changeLoostScene()
	{
		SceneManager.LoadScene("Loot", LoadSceneMode.Single);
	}
}