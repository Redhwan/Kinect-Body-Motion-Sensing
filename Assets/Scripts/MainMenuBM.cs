using UnityEngine;
using System.Collections;

public class MainMenuBM : MonoBehaviour {

    void Start() { 
     
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
