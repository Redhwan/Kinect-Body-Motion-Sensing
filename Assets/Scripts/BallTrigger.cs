using UnityEngine;
using System.Collections;

public class BallTrigger : MonoBehaviour {

    private PongController pongC;

    // Use this for initialization
    void Start () {
        pongC = FindObjectOfType<PongController>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.name == "BottomWall") {
            pongC.gameIsOver();
        }
     
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Cube") {
            Destroy(col.gameObject);
            pongC.noOfCubes--;
            if (pongC.noOfCubes == 0) {
                pongC.gameIsWon();
            }
        }
    }

}
