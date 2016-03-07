using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class CubeController : SimpleGestureListener {

    private PauseMenuHandler pmh;
    public GameObject cube;
    private float verDegree;
    private float horDegree;
    public float speed;
    public bool up, down, left, right;
    public int curDir, score;
    public ArrayList dirs;
    public Text directionText, scoreText, userText;
    public bool roundComplete, inRound, restart, backToMain;


    // Use this for initialization
    void Start () {
        pmh = FindObjectOfType<PauseMenuHandler>();
        speed = 10f;
        curDir = score = 0;

        dirs = new ArrayList();

        for (int i = 0; i < 3; i++) {
            addDirection();
        }
        showDirections();
    }
	
	// Update is called once per frame
	void Update () {

        if (!pmh.isPaused) {
            if (!pmh.gameOver) {
                checkRound();
                doMove();
                checkForMove();
                //moveCube();
            } else {





                if (Input.GetKeyUp(KeyCode.R)) {
                    restart = true;
                }
                if (Input.GetKeyUp(KeyCode.Space)) {
                    backToMain = true;
                }

                if (restart) {
                    Application.LoadLevel(Application.loadedLevel);
                }

                if (backToMain) {
                    Application.LoadLevel(1);
                }


            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
            pmh.pause(true);


        }


    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {
        print("hi");
        if (inRound) {
            switch (gesture) {
                case KinectGestures.Gestures.SwipeUp:

                    up = true;
                    break;
                case KinectGestures.Gestures.SwipeRight:
                    right = true;
                    break;
                case KinectGestures.Gestures.SwipeLeft:
                    left = true;
                    break;
                case KinectGestures.Gestures.SwipeDown:
                    down = true;
                    break;
            }
        }

        return base.GestureCompleted(userId, userIndex, gesture, joint, screenPos);
    }

    public void checkRound() {
        if (curDir == dirs.Count) {
            completeRound();
        }
        
    }

    public void completeRound() {
        inRound = false;
        addDirection();
        curDir = 0;
        roundComplete = false;
        Debug.Log("NextRound");
        showDirections();
    }


    private void addDirection() {
        Directions.direction temp = Directions.getRandomDirection();
        dirs.Add(temp);
        directionText.text += temp + ", ";

    }

    public void checkForMove() {
        Directions.direction temp = (Directions.direction)dirs[curDir];
        if (up) {
            horDegree += 90f;
            userText.text += "up, ";
            if (temp == Directions.direction.up) {
                score++;
                curDir++;
                up = false;
            } else {
                pmh.gameOver = true;
            }
        }

        if (down) {
            horDegree -= 90f;
            userText.text += "down, ";
            if (temp == Directions.direction.down) {
                score++;
                curDir++;
                down = false;
            } else {
                pmh.gameOver = true;
            }
        }

        if (right) {
            verDegree += 90f;
            userText.text += "right, ";
            if (temp == Directions.direction.right) {
                score++;
                curDir++;
                right = false;
            } else {
                pmh.gameOver = true;
            }
        }

        if (left) {
            verDegree -= 90f;
            userText.text += "left, ";
            if (temp == Directions.direction.left) {
                score++;
                curDir++;
                left = false;
            } else {
                pmh.gameOver = true;
            }
        }

        if (pmh.gameOver) {
            directionText.enabled = true;
            scoreText.text = "WRONG!";
        } else {
            scoreText.text = "" + score;
        }

        cube.transform.localRotation = Quaternion.Lerp(cube.transform.localRotation, Quaternion.Euler(horDegree, verDegree, 0), speed * Time.deltaTime);

    }

    public void doMove() {
        if (inRound) {
            if (Input.GetKeyUp(KeyCode.UpArrow)) {
                up = true;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow)) {
                down = true;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)) {
                right = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)) {
                left = true;
            }
        }
    }

    public void showDirections() {
        directionText.enabled = true;
        Invoke("hideDirections", 3);
        Invoke("hideUserDirections", 1);

    }

    public void hideDirections() {
        directionText.enabled = false;
        userText.text = "";
        inRound = true;
    }

    public void hideUserDirections() {
        userText.text = "";
    }


    
    public void debugMoveCube() {
        if (Input.GetKeyUp(KeyCode.RightArrow))
            verDegree += 90f;
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            verDegree -= 90f;
        if (Input.GetKeyUp(KeyCode.UpArrow))
            horDegree += 90f;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            horDegree -= 90f;
        print("Rotation: " + cube.transform.localRotation.eulerAngles);
        cube.transform.rotation = Quaternion.Lerp(cube.transform.rotation, Quaternion.Euler(horDegree, verDegree, 0),  Time.deltaTime);
    }
}
