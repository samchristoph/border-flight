using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	public PlayerMovement movement;
	public float invDur = 0.5f; // duration a player has invulnerability
	public GameObject playerBroken;
	public float breakSpeedThreshold = 20f;
	public float shieldSeparation = 0.1f;

	private GameObject forcefields;
	private float timer;
	private AudioSource audio;
    private bool rolling = false;

    private float relativeSpeed;
    private Rigidbody rb;
	private GameManager gameManager;


	void Start() {
		gameManager = FindObjectOfType<GameManager>();
        audio = GetComponent<AudioSource>();
		forcefields = GameObject.Find("Forcefield Group");
		Vector3 playerScale = transform.localScale;
		Vector3 scaleDiff = new Vector3(shieldSeparation, shieldSeparation, shieldSeparation);
		float i = 1f;
		foreach (Transform t in forcefields.transform) {
			t.transform.localScale = new Vector3(playerScale.x + scaleDiff.x * i, playerScale.y + scaleDiff.y * i, playerScale.z + scaleDiff.z * i);
			i += 1f;
		}
		timer = 0f;
	}

	void FixedUpdate() {
        audio.pitch = Mathf.Max(Mathf.Min(relativeSpeed / 8f, 5f), 0.5f);
		if (timer > 0f) {
			timer -= Time.deltaTime;
		}
		// Debug.Log(timer);
	}

	void OnCollisionEnter (Collision collision) {
        Vector3 relativeVelocity = collision.relativeVelocity;
        // Vector3 velocity = GetComponent<Rigidbody>().velocity;
        Vector3 collisionNormal = collision.contacts[0].normal;
        float speedInCollisionDirection = Vector3.Dot(relativeVelocity, collisionNormal);
		Debug.Log(Mathf.Pow(speedInCollisionDirection, 2f));
        if (Mathf.Abs(Mathf.Pow(speedInCollisionDirection, 2f)) > breakSpeedThreshold) {
			GetRubble();
			// gameObject.GetComponent<AudioSource>().Pause();
			gameManager.EndGame();
			// collision.gameObject.GetComponent<AudioSource>().Play();
			Destroy(gameObject);
		}
		HandleCollision(collision);
	}

	void OnCollisionStay (Collision collision) {
        relativeSpeed = collision.relativeVelocity.magnitude;
        if (audio.isPlaying == false && collision.relativeVelocity.magnitude >= 0.1f) {
            audio.Play();
        }
		HandleCollision(collision);
	}

	void HandleCollision(Collision collision) {
		if (collision.collider.tag == "Obstacle" && timer <= 0f) {
			timer = invDur;
			if (forcefields.transform.childCount == 0) {
				GetRubble();
				movement.enabled = false;
				gameObject.GetComponent<AudioSource>().Pause();
				gameManager.EndGame();
				collision.gameObject.GetComponent<AudioSource>().Play();
				Destroy(gameObject);
			} else {
				Destroy(forcefields.transform.GetChild(forcefields.transform.childCount - 1).gameObject);
			}
		}
	}

    void OnCollisionExit(Collision collision) {
        if (audio.isPlaying == true) { // && collision.gameObject.tag == "Ground") {
            audio.Pause();
        }
    }

	void GetRubble() {
		GameObject rubble = Instantiate(playerBroken, gameObject.transform.position, gameObject.transform.rotation);
		foreach (Transform child in rubble.transform) {
			child.gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
		}
	}

}
