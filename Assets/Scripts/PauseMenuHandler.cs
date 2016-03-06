using UnityEngine;
using System.Collections;

public class PauseMenuHandler : SimpleGestureListener {

    public GameObject pauseMenu;
    public bool isPaused;


	// Update is called once per frame
	void Update () {

        debugSelect();
    }

    public void pause(bool p) {
            isPaused = p;
            pauseMenu.SetActive(p);
    }


    
    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {

        if (isPaused) {
            switch (gesture) {

                case KinectGestures.Gestures.SwipeRight:
                    pause(false);
                    break;
                case KinectGestures.Gestures.SwipeLeft:
                    Application.LoadLevel(1);
                    break;
            }
        }

        return base.GestureCompleted(userId, userIndex, gesture, joint, screenPos);
    }



    public void debugSelect() {
        if (isPaused) {
            if (Input.GetKeyUp(KeyCode.RightArrow))
                pause(false);
            if (Input.GetKeyUp(KeyCode.LeftArrow))
                Application.LoadLevel(1);
        }
    }
}
