using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

    private KinectManager km;
    private GameObject cursor;

    // Use this for initialization
    void Start () {
        cursor = GameObject.Find("Cursor");
        

        //Trying to hide the cursor

        //  Screen.lockCursor = false;
        Screen.showCursor = false;
        
        // When coming back from Play/PRactice screen, clear the avatar list as it doesnt look for destoyed avatar
        // and reset controller so it recognises the user

        KinectManager manager = KinectManager.Instance;


        if (manager) {



            for (int i = 0; i < manager.Player1Avatars.Count; i++) {

                manager.Player1Avatars[i] = null;

                
            }
        }

        manager.ResetAvatarControllers();
    }
	
	// Update is called once per frame
	void Update () {
        if (cursor.activeInHierarchy == false) {
            cursor.SetActive(true);
        }	
	}
}
