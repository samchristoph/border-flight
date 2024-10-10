using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// This is a reference to the Rigidbody component called "rb"
	public Rigidbody rb;

	public float forwardForce = 2200f;	// Variable that determines the forward force
	public float sidewaysForce = 40f;  // Variable that determines the sideways force
	public float torqueForce = 150f;

	void Start() {
		rb.maxAngularVelocity = 25f;
	}
	
	void FixedUpdate ()
	{
		// Add a forward force
		rb.AddForce(0, 0, forwardForce * Time.deltaTime);
		rb.AddTorque(new Vector3(1f, 0f, 0f) * torqueForce * Time.deltaTime, ForceMode.VelocityChange);

		if (Input.GetKey("d"))	// If the player is pressing the "d" key
		{
			// Add a force to the right
			rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
			rb.AddTorque(new Vector3(0f, 0f, -1f) * torqueForce * Time.deltaTime, ForceMode.VelocityChange);
		}

		if (Input.GetKey("a"))  // If the player is pressing the "a" key
		{
			// Add a force to the left
			rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
			rb.AddTorque(new Vector3(0f, 0f, 1f) * torqueForce * Time.deltaTime, ForceMode.VelocityChange);
		}

		if (rb.position.y < -1f)
		{
			FindObjectOfType<GameManager>().EndGame();
		}
	}
}
