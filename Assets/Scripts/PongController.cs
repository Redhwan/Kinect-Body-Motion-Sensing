using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PongController : SimpleGestureListener {

    private PlayButtonManager pbm;
    public GameObject board;
    public GameObject ball;
    public GameObject cursor;
    public float ballVelocity;
    private Rigidbody rb;
    private bool test;
    public bool gameOver, gameStarted, startCount, restart, backToMain;
    private float timeLeft = 4;
    public Text counter;
    public int noOfCubes = 11;
    private Vector3 temp;



    // Use this for initialization
    void Start() {

        

        pbm = GameObject.FindObjectOfType<PlayButtonManager>();
        rb = ball.GetComponent<Rigidbody>();

        //Hide cursor
        cursor = GameObject.Find("Cursor");

        gameStarted = false;
        startCount = false;

        Invoke("startGame", 3.0f);
        startCount = true;

    }

    // Update is called once per frame
    void Update () {


        if (gameStarted) {
            rb.AddForce(new Vector3(ballVelocity, ballVelocity, 0));
            gameStarted = false;
        }

        
        if (startCount) {
            
            timeLeft -= Time.deltaTime;
            counter.text = "" + (int)timeLeft;
            if (timeLeft < 1) {
                counter.text = "";
                startCount = false;
            }
        }

        if(noOfCubes == 0) {
            gameIsWon();
        }

        if (!gameOver) {

            

            if (!pbm.isPaused) {

                var mousePositioin = Input.mousePosition;
                mousePositioin.z = 9.5f;
                mousePositioin = Camera.main.ScreenToWorldPoint(mousePositioin);

                //   Debug.Log(mousePositioin.x);

                board.transform.position = (new Vector3(mousePositioin.x, board.transform.position.y, board.transform.position.z));

            }
        } else {
            if (Input.GetKeyDown(KeyCode.R)) {
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

        


        if (Input.GetKeyDown(KeyCode.P)) {
            temp = rb.velocity;
            rb.velocity = new Vector3(0f, 0f, 0f);
            //pbm.pauseMenuHandler(true);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            rb.velocity = temp;
            //pbm.pauseMenuHandler(true);
        }

    }


    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {

        if (!pbm.isPaused) {

            if (gesture == KinectGestures.Gestures.Tpose) {
                pbm.pauseMenuHandler(true);
            }

        }


        if (gameOver) {
            Debug.Log("Its getting Here");
            if(gesture == KinectGestures.Gestures.SwipeRight) {
                Debug.Log("HI");
                restart = true;
            }
            if (gesture == KinectGestures.Gestures.SwipeLeft) {
                Application.LoadLevel(1);
            }
        }


        return base.GestureCompleted(userId, userIndex, gesture, joint, screenPos);

    }


    public void startGame() {
        gameStarted = true;
    }

    public void gameIsOver() {
        gameOver = true;
        counter.text = "Game Over\n Swipe Left to go to Main Menu, Swipe Right to retry";
    }

    public void gameIsWon() {
        rb.velocity = new Vector3(0f, 0f, 0f);
        gameOver = true;
        counter.text = "Well Done!";
    }

}
