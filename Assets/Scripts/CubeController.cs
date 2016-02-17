using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class CubeController : MonoBehaviour {

    public GameObject cube;
    private float verDegree;
    private float horDegree;
    public float speed;
    public bool up, down, left, right;
    public int curDir, score;
    public ArrayList dirs;
    public Text directionText, scoreText, userText;
    public bool gameOver, roundComplete, inRound, restart;

    public enum direction{
        up,
        down,
        right,
        left
    }

    // Use this for initialization
    void Start () {

        speed = 10f;
        curDir = score = 0;
        up = down = left = right = gameOver = false;

        dirs = new ArrayList();

        for (int i = 0; i < 3; i++) {
            addDirection();
        }
        Debug.Log("Direction is: " + dirs[curDir]);

        showDirections();
    }
	
	// Update is called once per frame
	void Update () {

        if (!gameOver) {
           // checkRound();
           // doMove();
           // checkForMove();
             moveCube();
        } else {
            if (Input.GetKeyUp(KeyCode.R)) {
                restart = true;
            }

            if (restart) {
                Application.LoadLevel(Application.loadedLevel);
            }

        } 

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
        direction temp = (direction)UnityEngine.Random.Range(0, 3);
        dirs.Add(temp);
        directionText.text += temp + ", ";

    }

    public void checkForMove() {
        direction temp = (direction)dirs[curDir];
        if (up) {
            if(temp == direction.up) {
                score++;
                curDir++;
                up = false;
            } else {
                gameOver = true;
            }
        }

        if (down) {
            if (temp == direction.down) {
                score++;
                curDir++;
                down = false;
            } else {
                gameOver = true;
            }
        }

        if (right) {
            if (temp == direction.right) {
                score++;
                curDir++;
                right = false;
            } else {
                gameOver = true;
            }
        }

        if (left) {
            if (temp == direction.left) {
                score++;
                curDir++;
                left = false;
            } else {
                gameOver = true;
            }
        }

        if (gameOver) {
            directionText.enabled = true;
            scoreText.text = "WRONG!";
        } else {
            scoreText.text = "" + score;
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



    public void doMove() {
        if (inRound) {
            if (Input.GetKeyUp(KeyCode.UpArrow)) {
                horDegree += 90f;
                up = true;
                userText.text += "up, ";
            }
            if (Input.GetKeyUp(KeyCode.DownArrow)) {
                horDegree -= 90f;
                down = true;
                userText.text += "down, ";
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)) {
                verDegree += 90f;
                right = true;
                userText.text += "right, ";
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)) {
                verDegree -= 90f;
                left = true;
                userText.text += "left, ";
            }


           
        }
        cube.transform.localRotation = Quaternion.Lerp(cube.transform.localRotation, Quaternion.Euler(horDegree, verDegree, 0), speed * Time.deltaTime);

    }
    


    public void moveCube() {
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
