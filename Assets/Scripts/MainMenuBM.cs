using UnityEngine;
using System.Collections;

public class MainMenuBM : MonoBehaviour {

    void Start() { 
        // When coming back from Play/PRactice screen, clear the avatar list as it doesnt look for destoyed avatar
        // and reset controller so it recognises the user

    KinectManager manager = KinectManager.Instance;
		
		if(manager)
		{

			manager.Player1Avatars[0] = null;

        }

        manager.ResetAvatarControllers();
    }


	public void onClickPlay (){	//Play Screen
		Application.LoadLevel (2);
	}
	
	public void onClickPractice (){	//Practice Screen
		Application.LoadLevel (3);
	}
	
	public void onClickSettings (){	//Settings Screen
		Application.LoadLevel (4);
	}
	
	public void onClickHelp (){	//Help Screen
		Application.LoadLevel (5);
	}

}
