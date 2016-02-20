﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PongController : SimpleGestureListener {

    private PauseMenuHandler pmh;
    public GameObject board;
    public GameObject ball;
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
        
        pmh = FindObjectOfType<PauseMenuHandler>();
        rb = ball.GetComponent<Rigidbody>();
        gameStarted = false;
        startCount = false;
        Invoke("startGame", 3.0f);
        startCount = true;

    }

    // Update is called once per frame
    void Update () {

        checkForCounter();

        if (gameStarted) {
            rb.AddForce(new Vector3(ballVelocity, ballVelocity, 0));
            gameStarted = false;
        }
        
        if(noOfCubes == 0) {
            gameIsWon();
        }

        if (!gameOver) {
            if (!pmh.isPaused) {
                var mousePositioin = Input.mousePosition;
                mousePositioin.z = 9.5f;
                mousePositioin = Camera.main.ScreenToWorldPoint(mousePositioin);
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
            //pmh.pauseMenuHandler(true);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            rb.velocity = temp;
            //pmh.pauseMenuHandler(true);
        }

        if (Input.GetKeyUp(KeyCode.Escape))
            pmh.pause();


    }


    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {

        if (!pmh.isPaused) {

            if (gesture == KinectGestures.Gestures.Tpose) {
                pmh.pause();
            }

        }

        if (gameOver) {
            if(gesture == KinectGestures.Gestures.SwipeRight) {
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

    public void checkForCounter() {
        if (startCount) {

            timeLeft -= Time.deltaTime;
            counter.text = "" + (int)timeLeft;
            if (timeLeft < 1) {
                counter.text = "";
                startCount = false;
            }
        }
    }

}
