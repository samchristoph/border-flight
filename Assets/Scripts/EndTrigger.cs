using UnityEngine;

public class EndTrigger : MonoBehaviour {

	public GameManager gameManager;

	void Start() {
		gameManager = FindObjectOfType<GameManager>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			gameManager.gameHasBeenWon = true;
			other.GetComponent<AudioSource>().Pause();
			gameObject.GetComponent<AudioSource>().Play();
			gameManager.CompleteLevel();
		}
	}

}
