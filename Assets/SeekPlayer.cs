using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPlayer : MonoBehaviour {
    private GameObject player;
    public float distForceMultiplier = 5f;
    public float minForce = 50f;
    private int layerMask;

    void Start() {
        int layerMask = (1 << 7); //| (1 << layerB);
        layerMask = ~layerMask;
        player = GameObject.Find("Player");
    }

    void FixedUpdate() {
        if (player) {
            Vector3 dist = (player.transform.position - transform.position);
            Vector3 unit = dist.normalized;
            gameObject.GetComponent<Rigidbody>().AddForce(Mathf.Max(dist.magnitude * distForceMultiplier, minForce) * unit);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, unit, out hit, 20f)) { //, layerMask)) {
                Debug.Log("Did Hit");
                if (hit.collider != null && hit.collider.gameObject.name == "Ground") {
                    gameObject.GetComponent<Rigidbody>().AddForce(Mathf.Max(gameObject.GetComponent<Rigidbody>().mass * distForceMultiplier, minForce) * new Vector3(-dist.x, Mathf.Abs(dist.x), Mathf.Abs(dist.x))); //, ForceMode.VelocityChange);
                }
            }
        }
    }
}
