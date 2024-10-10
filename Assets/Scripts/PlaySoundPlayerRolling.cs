using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundPlayerRolling : MonoBehaviour {
    private AudioSource audio;
    private bool rolling = false;

    private float speed;

    void Start() {
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        audio.pitch = Mathf.Max(Mathf.Min(speed / 8f, 5f), 0.5f);
    }

    void OnCollisionStay(Collision collision) {
        speed = collision.relativeVelocity.magnitude;
        if (audio.isPlaying == false && collision.relativeVelocity.magnitude >= 0.1f) {
            audio.Play();
        }
        // else if (audio.isPlaying == true) {
        //     audio.Pause();
        // }
    }

    void OnCollisionExit(Collision collision) {
        if (audio.isPlaying == true) { // && collision.gameObject.tag == "Ground") {
            audio.Pause();
        }
    }

}
