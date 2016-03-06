using UnityEngine;
using System.Collections;

public class Test : SimpleGestureListener {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {

        switch (gesture) {
            case KinectGestures.Gestures.SwipeUp:
                print("swiped up");
                break;
            case KinectGestures.Gestures.SwipeRight:
                print("swiped right");
                break;
            case KinectGestures.Gestures.SwipeLeft:
                print("swiped left");
                break;
            case KinectGestures.Gestures.SwipeDown:
                print("swiped down");
                break;
        }


        return base.GestureCompleted(userId, userIndex, gesture, joint, screenPos);
    }


}
