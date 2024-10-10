using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private Transform player;
	public Text scoreText;

	void Start() {
		player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (player) {
			scoreText.text = player.position.z.ToString("0"); 
		}
	}
}
