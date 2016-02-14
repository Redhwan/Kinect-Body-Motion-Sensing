using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayPracticeController : SimpleGestureListener {

    private PlayButtonManager pbm;
    private KinectManager km;
    public Text tstTxt;

    public GameObject textBox;
    public GameObject afterMoveChoice;
    public Text dialogueText;
    public TextAsset textFile;
    public string[] TextLines;
    //public GameObject cursor;

    //public bool isPaused;
    public bool isShowingGesture;

    public int curLine, endLine;
    public bool swipedRight, swipedLeft;
    public bool checkGestureUnderstood, userPerformingGesture, gesturePerformed;
    public int gestureNumber;


    

    // Use this for initialization
    void Start () {

        pbm = GameObject.FindObjectOfType<PlayButtonManager>();
        km = GameObject.FindObjectOfType<KinectManager>();
     //   cursor = GameObject.Find("Cursor");

       
 //Setting all initial variables
        isShowingGesture = false;
        pbm.isPaused = false;
        swipedRight = false;
        swipedLeft = false;
        checkGestureUnderstood = false;
        userPerformingGesture = false;
        gesturePerformed = false;
        gestureNumber = 0;


        //Reads each new line into and array index.
        if (textFile != null) {
            TextLines = (textFile.text.Split('\n'));
        }

        curLine = 0;
        endLine = TextLines.Length - 1;

    }

    // Update is called once per frame.             
    void Update () {

        if (Input.GetKeyDown(KeyCode.P)) {
            pbm.pauseMenuHandler(true);

        }



        //Showing the textLine with index currLine
        dialogueText.text = TextLines[curLine];


 //Firstly make sure no that the user isnt perfroming a guesture and the checkguestureunderstood hasnt happened yet.
 //go into else: if swiped right, move textLine up one, if its "ShowGesture" then its gonna do that method.
 //if "DoGesture" its gonna do that method. Reset swiped right variable.

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

                        textBox.SetActive(false);
                        StartCoroutine(showGesture());

                    } else if (TextLines[curLine].Equals("DoGesture\r")) {

                        textBox.SetActive(false);
                        doGesture();

                    } else {
                        textBox.SetActive(true);
                    }
                } else {
                    textBox.SetActive(false);
                }

                swipedRight = false;
            }
        }

    }


    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {

// Basically cant do any of the below gestures while the game is paused.
        if (!pbm.isPaused) {

           //If the Teacher is showing a guesture, you cant pause the game, also Bow and swipe right wont be done so not
           // to mess up the bools
            if (!isShowingGesture) {

                if (gesture == KinectGestures.Gestures.Tpose) {
                    pbm.pauseMenuHandler(true);
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

                    //Basically anyother time, you can swipe right`

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


    //This method is a place holder for the animation the teacher will show.
    //After the guesture, add 1 to currline to show the next lines and checkGestureUnderstood to true to give the option.
    //Also set box back to true, showing geture is now.
    IEnumerator showGesture() {
        isShowingGesture = true;
        Debug.Log("Gesture in progress...");
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Gesture Complete");
        // afterMoveChoice.SetActive(true);
        curLine += 1;
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

    //This just allows to get into the user performing the gesture part
    void doGesture() {

        userPerformingGesture = true;

    }


    //once the gesture is done, adjust variables and move onto the next line in text.
    void gestureIsDone() {
        userPerformingGesture = false;
        gesturePerformed = false;
        curLine += 1;
        textBox.SetActive(true);
        gestureNumber++;

    }



}