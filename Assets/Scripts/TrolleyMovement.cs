using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyMovement : MonoBehaviour {
    public Vector3 deltaDist = new Vector3(1f, 1f, 1f);

    private Vector3 initPos;

    void Start() {
        // initPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        initPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        deltaDist = new Vector3(0f, 0f, deltaDist.z);
    }

    void Update() {
        // Debug.Log(deltaDist);
        // Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 pos = new Vector3(initPos.x, initPos.y, transform.position.z);
        Color color = gameObject.GetComponent<MeshRenderer>().materials[0].color;
        // color.a = 1f * Mathf.Abs((Vector3.Distance(initPos, pos) - Vector3.Distance(initPos, deltaDist)) / (Vector3.Distance(initPos, deltaDist)));
        color.a = 1f * Mathf.Abs((initPos.z + deltaDist.z) - pos.z) / Mathf.Abs(deltaDist.z);
        gameObject.GetComponent<MeshRenderer>().materials[0].color = color;
        foreach (Transform child in gameObject.transform) {
            child.gameObject.GetComponent<MeshRenderer>().materials[0].color = color;
        }

        if (pos.z >= initPos.z + deltaDist.z) {
            Debug.Log(pos.z);
            Destroy(gameObject);
        }

        // if (Mathf.Abs(pos.x - initPos.x) > deltaDist.x) {
        //     Debug.Log(pos.x);
        //     Destroy(gameObject);
        // }
        // if (Mathf.Abs(pos.y - initPos.y) > deltaDist.y) {
        //     Debug.Log(pos.y);
        //     Destroy(gameObject);
        // }
        // if (Mathf.Abs(pos.z - initPos.z) > deltaDist.z) {
        //     Debug.Log(pos.z);
        //     Destroy(gameObject);
        // }
    }
}
