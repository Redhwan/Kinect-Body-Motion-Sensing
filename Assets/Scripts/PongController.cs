using UnityEngine;
using System.Collections;

public class PongController : SimpleGestureListener {

    private PlayPracticeBM ppbm;
    public GameObject board;
    public GameObject cursor;

    // Use this for initialization
    void Start() {


        ppbm = GameObject.FindObjectOfType<PlayPracticeBM>();


        //Hide cursor

        cursor = GameObject.Find("Cursor");
       // cursor.SetActive(false);
    }

    // Update is called once per frame
    void Update () {

        float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        board.transform.position = (new Vector3(x, board.transform.position.y, board.transform.position.z));

        Debug.Log("Mouse Position: " + Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
	
	}


    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {

        if (gesture == KinectGestures.Gestures.Tpose) {
            ppbm.pauseMenuHandler(true);
        }



        return base.GestureCompleted(userId, userIndex, gesture, joint, screenPos);

    }
}
