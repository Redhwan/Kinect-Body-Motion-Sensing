using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayPracticeBM : ButtonManager {

	public GameObject pauseMenuBackground;
	public Text pauseButtonText;
	public GameObject[] buttonList;
	private bool isPaused;

	// Use this for initialization
	void Start(){

		isPaused = false;

	}



	public void onClickPause () {

		if (!isPaused) {
			pauseMenuHandler(true, "Continue");
		} else {
			pauseMenuHandler(false, "Pause");
		}

	}
	
	public virtual void pauseMenuHandler (bool b, string pauseStr) {

		pauseMenuBackground.SetActive (b);
		pauseButtonText.text = pauseStr;
		foreach (GameObject g in buttonList) {
			g.SetActive (b);
		}
		isPaused = b;

	}

}
