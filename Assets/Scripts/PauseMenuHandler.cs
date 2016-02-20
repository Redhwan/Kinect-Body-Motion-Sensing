using UnityEngine;
using System.Collections;

public class PauseMenuHandler : SimpleGestureListener {

    public GameObject pauseMenu;
    public bool isPaused;

	
	// Update is called once per frame
	void Update () {
        if (isPaused)
        debugSelect();
    }

    public void pause() {
        if (isPaused) {
            isPaused = false;
            pauseMenu.SetActive(false);
        } else {
            isPaused = true;
            pauseMenu.SetActive(true);
        }
    }
    
    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {

        switch (gesture) {
  
            case KinectGestures.Gestures.SwipeRight:
                pause();
                break;
            case KinectGestures.Gestures.SwipeLeft:
                Application.LoadLevel(1);
                break;

        }

        return base.GestureCompleted(userId, userIndex, gesture, joint, screenPos);
    }



    public void debugSelect() {
        if (Input.GetKeyUp(KeyCode.RightArrow))
            pause();
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            Application.LoadLevel(1);
    }
}
