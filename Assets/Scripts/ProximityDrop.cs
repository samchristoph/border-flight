using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDrop : MonoBehaviour {

    public float proxDist = 20f;
    public float deleteDist = 20f;

    private float offset;
    private GameObject player;

    void Start() {
        player = GameObject.Find("Player");
        offset = Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Update() {
        if (player && Mathf.Abs(player.transform.position.z - gameObject.transform.position.z) <= proxDist) {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
        }
        if (player && player.transform.position.z - gameObject.transform.position.z >= deleteDist + offset) {
            Destroy(gameObject);
        }
    }
}
