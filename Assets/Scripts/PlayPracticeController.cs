using UnityEngine;
using System.Collections;

public class PlayPracticeController : SimpleGestureListener {

    private PlayPracticeBM ppbm;

	// Use this for initialization
	void Start () {

        ppbm = GameObject.Find("PlayPracticeBM").GetComponent<PlayPracticeBM>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {

        if (gesture == KinectGestures.Gestures.Tpose)
            ppbm.pauseMenuHandler(true, "Continue");

        return base.GestureCompleted(userId, userIndex, gesture, joint, screenPos);

       
    }
}