using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPlayer : MonoBehaviour {
    private GameObject player;
    public float distForceMultiplier = 1f;
    public float minForce = 50f;
    private int layerMask;
    private float mass;
    private Rigidbody rb;
    private float offset;
    private float liftForceMultiplier = 10f;
    public LayerMask seekerLayerMask;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        // int layerMask = (1 << 7) | (1 << 3);
        // layerMask = ~layerMask;
        player = GameObject.Find("Player");
        mass = rb.mass;
        offset = Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void FixedUpdate() {
        if (player) {
            Vector3 dist = (player.transform.position - transform.position);
            Vector3 unit = dist.normalized;
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, offset, unit, out hit, Mathf.Infinity, seekerLayerMask) && hit.collider != null) { //, layerMask)) {
                // Debug.Log(unit);
                Collider col = hit.collider;
                if ((col.gameObject.name == "Ground"  && col.transform.position.y >= transform.position.y) || (player.transform.position.y >= transform.position.y)) {
                    // Debug.Log("OFF ROAD" + (Mathf.Max(mass * distForceMultiplier * liftForceMultiplier, minForce) * new Vector3(1f, 1f, 1f)).ToString());
                    // rb.AddForce(Mathf.Max(mass * distForceMultiplier, minForce) * new Vector3(-unit.x, Mathf.Abs(dist.magnitude), Mathf.Abs(dist.magnitude))); //, ForceMode.VelocityChange);
                    rb.AddForce(Mathf.Max(mass * distForceMultiplier * liftForceMultiplier, minForce) * new Vector3(-Mathf.Sign(unit.x), 1f, 1f));
                } else {
                    // Debug.Log("SEEKING PLAYER");
                    rb.AddForce(Mathf.Max(mass * dist.magnitude * distForceMultiplier, minForce) * unit);
                    // maybe do this: add this force always, but when you need to reverse it, subtract it and add the liftforcemultiplier force as well
                }
            }
        }
    }
}
