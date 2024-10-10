using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public bool gameHasBeenWon = false;
	bool gameHasEnded = false;

	public float restartDelay = 1f;

	public GameObject completeLevelUI;

	void Start() {
		completeLevelUI = GameObject.Find("Canvas").transform.Find("LevelComplete").gameObject;
		// completeLevelUI = GameObject.Find("LevelComplete");
	}

	public void CompleteLevel () {
		completeLevelUI.SetActive(true);
	}

	public void EndGame ()
	{
		if (gameHasEnded == false && gameHasBeenWon == false)
		{
			gameHasEnded = true;
			Debug.Log("GAME OVER");
			Invoke("Restart", restartDelay);
		}
	}

	void Restart ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
