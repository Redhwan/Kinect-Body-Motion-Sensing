using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuHandler : SimpleGestureListener {

    public GameObject pauseMenu;
    public bool isPaused;
    public bool gameOver;
    public Text pauseMenuText;
    
	// Update is called once per frame
	void Update () {
        debugSelect();
    }

    public void pause(bool p) {
        isPaused = p;
        pauseMenu.SetActive(p);
        pauseMenuText.text = "Continue";
    }

    public void gameIsOver() {
        gameOver = true;
        pauseMenu.SetActive(true);
        pauseMenuText.text = "Retry";
    }
    
    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {
        if (isPaused || gameOver) {
            switch (gesture) {
                case KinectGestures.Gestures.SwipeRight:
                    if (isPaused) {
                        pause(false);
                    } else {
                        Application.LoadLevel(Application.loadedLevel);
                    }
                    break;
                case KinectGestures.Gestures.SwipeLeft:
                    Application.LoadLevel(1);
                    break;
            }
        }
        return base.GestureCompleted(userId, userIndex, gesture, joint, screenPos);
    }
    
    public void debugSelect() {
        if (isPaused || gameOver) {
            if (Input.GetKeyUp(KeyCode.RightArrow)) {
                if (isPaused) {
                    pause(false);
                } else {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)) {
                Application.LoadLevel(1);
            }
        }
    }
}
