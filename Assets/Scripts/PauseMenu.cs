using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public bool mainMenu = false;
	bool paused = false;
    private GameObject player;

    void Start() {
        player = GameObject.Find("Player");
        if (mainMenu) {
            paused = true;
        }
    }

	void Update() {
		if (mainMenu == false && Input.GetKeyDown(KeyCode.Escape)) {
			paused = togglePause();
        }
	}
	
	void OnGUI() {
		if (mainMenu == false && paused) {
            GUILayout.BeginHorizontal(); {
                GUILayout.FlexibleSpace();
                GUILayout.Label("PAUSED");
                if (GUI.Button(new Rect(Screen.width/2-50,Screen.height/3-25,100,50),"MAIN MENU")) {
                    togglePause();
                    SceneManager.LoadScene(0);
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal ();
		} else if (mainMenu && paused) {
            GUILayout.BeginHorizontal(); {
                GUILayout.FlexibleSpace();
                GUILayout.Label("MAIN MENU");
                if (GUI.Button(new Rect(Screen.width/2-50,Screen.height/3-25,100,50),"START GAME")) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                if (GUI.Button(new Rect(Screen.width/2-50,2*Screen.height/3-25,100,50),"QUIT GAME")) {
                    Application.Quit();
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal ();
		}
	}
	
	bool togglePause() {
		if(Time.timeScale == 0f) {
			Time.timeScale = 1f;
            player.GetComponent<AudioSource>().Play();
			return(false);
		}
		else {
			Time.timeScale = 0f;
            player.GetComponent<AudioSource>().Pause();
			return(true);
		}
	}
}
