using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayPracticeController : SimpleGestureListener {

    private PlayPracticeBM ppbm;
    private KinectManager km;
    public Text tstTxt;

    public GameObject textBox;
    public Text dialogueText;
    public TextAsset textFile;
    public string[] TextLines;
    private int curLine;
    private int endLine;
    private bool swiped;

    // Use this for initialization
    void Start () {


        //   ppbm = GameObject.Find("PlayPracticeBM").GetComponent<PlayPracticeBM>();
        //   km = GameObject.Find("KinectObject").GetComponent<KinectManager>();
        ppbm = GameObject.FindObjectOfType<PlayPracticeBM>();
        km = GameObject.FindObjectOfType<KinectManager>();
        swiped = false;

        if (textFile != null)
            TextLines = (textFile.text.Split('\n'));

        curLine = 1;
        endLine = TextLines.Length - 1;



        //       km.goForIt = false; 
        //       tstTxt.text = "Somethings Happening"; 
    }

    // Update is called once per frame.             
    void Update () {

      
        dialogueText.text = TextLines[curLine];

     
   

        if (swiped) { 
            if (curLine < endLine) {
                curLine += 1;
                if (TextLines[curLine].Equals(TextLines[0])) {
                    textBox.SetActive(false);
                } else {
                    textBox.SetActive(true);
                }
            } else {
                textBox.SetActive(false);
            }

            swiped = false;
        }

       

        /*        if (Input.GetKeyDown(KeyCode.Space)) {
                    if (km.goForIt) {
                        km.goForIt = false;
                        tstTxt.text = "Somethings Happening";
                    } else {
                        km.goForIt = true;
                        tstTxt.text = "Waiting for Gesture";
                    }
                }           */


    }


    public override bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos) {

        if (gesture == KinectGestures.Gestures.Tpose) 
            ppbm.pauseMenuHandler(true, "Continue");

        if (gesture == KinectGestures.Gestures.SwipeRight)
            swiped = true;



        return base.GestureCompleted(userId, userIndex, gesture, joint, screenPos);

    }



}