using UnityEngine;
using System.Collections;

public class AboutController : SimpleGestureListener {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        debugSelect();
    }

    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {

        switch (gesture) {
            case KinectGestures.Gestures.SwipeRight:
                Application.LoadLevel(1);
                break;
        }

        return base.GestureCompleted(userId, userIndex, gesture, joint, screenPos);
    }

    public void debugSelect() {
        if (Input.GetKeyUp(KeyCode.RightArrow))
            Application.LoadLevel(1);    
    }
}
