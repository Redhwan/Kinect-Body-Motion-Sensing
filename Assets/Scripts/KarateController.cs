using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KarateController : SimpleGestureListener {

    private PauseMenuHandler pmh;

    public GameObject textBox;
    public GameObject afterMoveChoice;
    public Text dialogueText;
    public TextAsset textFile;
    public string[] TextLines;
    public bool isShowingGesture;
    public int curLine, endLine;
    public bool swipedRight, swipedLeft;
    public bool checkGestureUnderstood, userPerformingGesture, gesturePerformed;
    public int gestureNumber;

    // Use this for initialization
    void Start () {
        pmh = FindObjectOfType<PauseMenuHandler>();

        //Setting all initial variables
        // isShowingGesture = swipedRight = swipedLeft = checkGestureUnderstood = userPerformingGesture = gesturePerformed = false; 
        
        //Reads each new line into and array index.
        if (textFile != null) {
            TextLines = (textFile.text.Split('\n'));
        }

        gestureNumber = 0;
        curLine = 0;
        endLine = TextLines.Length - 1;

        //Showing the textLine with index currLine
        dialogueText.text = TextLines[curLine];
    }

    // Update is called once per frame.             
    void Update () {
     
        mainGame();
        debugSelect();

    }


    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {

        // Basically cant do any of the below gestures while the game is paused.
        if (!pmh.isPaused) {
           //If the Teacher is showing a guesture, you cant pause the game, also Bow and swipe right wont be done so not
           // to mess up the bools
            if (!isShowingGesture) {
                if (gesture == KinectGestures.Gestures.Tpose) {
                    pmh.pause(true);
                }
                //This is for the user gesture, can only work when its time for the user to perfom
                if (userPerformingGesture) {
                    if (gestureNumber == 0) {
                        if (gesture == KinectGestures.Gestures.Bow)
                            gesturePerformed = true;
                    } else if (gestureNumber == 1) {
                        if (gesture == KinectGestures.Gestures.AgeUke)
                            gesturePerformed = true;
                    } else if (gestureNumber == 2) {
                        if (gesture == KinectGestures.Gestures.MaeGeri)
                            gesturePerformed = true;
                    }
                } else {
                    //Basically anyother time, you can swipe right
                    if (gesture == KinectGestures.Gestures.SwipeRight)
                        swipedRight = true;
                }
            }
            //Swipe left can only be done when an option if the user understand the guesture is available
            if (checkGestureUnderstood) {
                if (gesture == KinectGestures.Gestures.SwipeLeft)
                    swipedLeft = true;
            }
        }
        return base.GestureCompleted(userId, userIndex, gesture, joint, screenPos);
    }


    //Firstly make sure no that the user isnt perfroming a guesture and the checkguestureunderstood hasnt happened yet.
    //go into else: if swiped right, move textLine up one, if its "ShowGesture" then its gonna do that method.
    //if "DoGesture" its gonna do that method. Reset swiped right variable.
    public void mainGame() {
        if (checkGestureUnderstood) {
            checkIfGestureUnderstood();
        } else if (userPerformingGesture) {
            if (gesturePerformed) {
                gestureIsDone();
            }
        } else {
            if (swipedRight) {
                if (curLine < endLine) {
                    curLine += 1;
                    if (TextLines[curLine].Equals("ShowGesture\r")) {
                        curLine += 1;
                        textBox.SetActive(false);
                        StartCoroutine(showGesture());
                    } else if (TextLines[curLine].Equals("DoGesture\r")) {
                        curLine += 1;
                        textBox.SetActive(false);
                        userPerformingGesture = true;
                    } else {
                        if (!textBox.activeInHierarchy) {
                            textBox.SetActive(true);
                        }
                    }
                    dialogueText.text = TextLines[curLine];
                } else {
                    textBox.SetActive(false);
                }
                swipedRight = false;
            }
        }
    }


    //This method is a place holder for the animation the teacher will show.
    //After the guesture, add 1 to currline to show the next lines and checkGestureUnderstood to true to give the option.
    //Also set box back to true, showing geture is now.
    IEnumerator showGesture() {
        isShowingGesture = true;
        Debug.Log("Gesture in progress...");
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Gesture Complete");
        textBox.SetActive(true);
        isShowingGesture = false;
        checkGestureUnderstood = true;
    }

  //If the checkGestureUnderstood is true this checks for left/right sweep, if right, then finish and go out, this will continue as normal
  //if its left, then go back a couple of lines, and re do the swipe right bit to show the gesture. the reset the left, and checkgestureunderstton variables.
    void checkIfGestureUnderstood() {
        if (swipedRight) {
            checkGestureUnderstood = false;
        } else if (swipedLeft) {
            curLine -= 2;
            swipedRight = true;
            swipedLeft = false;
            checkGestureUnderstood = false;
        }
    }
    
    //once the gesture is done, adjust variables and move onto the next line in text.
    void gestureIsDone() {
        userPerformingGesture = false;
        gesturePerformed = false;
        //curLine += 1;
        textBox.SetActive(true);
        gestureNumber++;
    }
    
    public void debugSelect() {

        if (Input.GetKeyDown(KeyCode.Escape))
            pmh.pause(true);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            swipedRight = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            swipedLeft = true;
    }
}