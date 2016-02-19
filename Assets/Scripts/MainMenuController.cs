using UnityEngine;
using System.Collections;

public class MainMenuController : SimpleGestureListener {

    // Use this for initialization
    void Start () {

        //TRY - static variable, with previous scene.. then add it in an if to see if coming from the karate scene.
        checkForAvatar();

    }

    // Update is called once per frame
    void Update () {

        debugSelect();

    }

    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {

        switch (gesture) {
            case KinectGestures.Gestures.SwipeUp:
                Application.LoadLevel(2);
                break;
            case KinectGestures.Gestures.SwipeRight:
                Application.LoadLevel(3);
                break;
            case KinectGestures.Gestures.SwipeLeft:
                Application.LoadLevel(4);
                break;
            case KinectGestures.Gestures.SwipeDown:
                Application.LoadLevel(5);
                break;
        }

        return base.GestureCompleted(userId, userIndex, gesture, joint, screenPos);
    }

    //Delete avatars and restart the controllers so the manager class does not look for the avater from the previous page (only if coming from the karate screen).
    public void checkForAvatar() {

        KinectManager manager = KinectManager.Instance;
        if (manager) {
            for (int i = 0; i < manager.Player1Avatars.Count; i++) {
                manager.Player1Avatars[i] = null;
            }
        }
        manager.ResetAvatarControllers();
    }

    public void debugSelect() {

        if (Input.GetKeyUp(KeyCode.UpArrow))
            Application.LoadLevel(2);
        if (Input.GetKeyUp(KeyCode.RightArrow))
            Application.LoadLevel(3);
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            Application.LoadLevel(4);
        if (Input.GetKeyUp(KeyCode.DownArrow))
            Application.LoadLevel(5);

    }
}
