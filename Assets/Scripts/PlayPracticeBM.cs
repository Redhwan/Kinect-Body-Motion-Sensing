using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayPracticeBM : ButtonManager {

	public GameObject pauseMenuBackground;
	//public Text pauseButtonText;
	public GameObject pauseButton;
    public GameObject[] buttonList;
  

    private PlayPracticeController ppc;
    private KinectManager km;
    // Use this for initialization
    void Start(){

        ppc = GameObject.FindObjectOfType<PlayPracticeController>();
        km = GameObject.FindObjectOfType<KinectManager>();
        

//Hide cursor and dont allow user to control it.
      //  km.ControlMouseCursor = false;
        ppc.cursor.SetActive(false);
    }


    //This method is only called when in pause, so always just returning back to normal
	public void onClickPause () {
			pauseMenuHandler(false);

	}
	

//Bring up pause menu and all buttons. Show cursor and allow user to control it.
	public virtual void pauseMenuHandler (bool b) {

		pauseMenuBackground.SetActive (b);
        pauseButton.SetActive(b);
        //pauseButtonText.text = pauseStr;
        foreach (GameObject g in buttonList) {
			g.SetActive (b);
		}
		ppc.isPaused = b;
        ppc.cursor.SetActive(b);
        km.ControlMouseCursor = b;

    }

}
