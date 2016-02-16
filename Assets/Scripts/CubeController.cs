using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class CubeController : MonoBehaviour {

    //public GameObject cube;
    //private float verDegree;
    //private float horDegree;
    //public float speed;
    public bool up, down, left, right, gameOver, roundComplete;
    public int curDir, score;
    public ArrayList dirs;
    public Text directionText, scoreText;

    public enum direction{
        up,
        down,
        right,
        left
    }

    // Use this for initialization
    void Start () {

        
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

        checkRound();
        doMove();
        checkForMove();



        if (Input.GetKeyUp(KeyCode.Space)) {
            foreach (var val in dirs) {
                Debug.Log(val.ToString());
            }
            Debug.Log("-------------------------------");
        }

        if (Input.GetKeyUp(KeyCode.A)) {
            addDirection();
        }





    }   

    public void checkRound() {
        if (curDir == dirs.Count) {
            completeRound();
        }
        
    }

    public void completeRound() {
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
            scoreText.text = "WRONG!";
        } else {
            scoreText.text = "" + score;
        }


    }

    public void showDirections() {
        directionText.enabled = true;
        Invoke("hideDirections", 3);


    }

    public void hideDirections() {
        directionText.enabled = false;
    }



    public void doMove() {
        if (Input.GetKeyUp(KeyCode.UpArrow))
            up = true;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            down = true;
        if (Input.GetKeyUp(KeyCode.RightArrow))
            right = true;
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            left = true;

    }



    //public void moveCube() {
    //    if (Input.GetKeyUp(KeyCode.RightArrow))
    //        verDegree += 90f;
    //    if (Input.GetKeyUp(KeyCode.LeftArrow))
    //        verDegree -= 90f;
    //    if (Input.GetKeyUp(KeyCode.UpArrow))
    //        horDegree += 90f;
    //    if (Input.GetKeyUp(KeyCode.DownArrow))
    //        horDegree -= 90f;


    //    cube.transform.localRotation = Quaternion.Lerp(cube.transform.localRotation, Quaternion.Euler(horDegree, verDegree, 0 ), speed*Time.deltaTime);
    //}


}
