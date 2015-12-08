using UnityEngine;
using System.Collections;

public class LoadMainLevel : MonoBehaviour 
{

	
	private bool levelLoaded = false;

//	private static LoadMainLevel instance;
//	
//	public static LoadMainLevel Instance
//	{
//		get { return instance; }
//	}
//
//
//	void Awake(){
//		if (instance == null)
//			instance = this;
//	// If one already exist, it's because it came from another level.
//	else if (instance != this) {
//			Destroy (gameObject);
//			return;
//		}
//	}

	void Update() 
	{
		KinectManager manager = KinectManager.Instance;
		
		if(!levelLoaded && manager && KinectManager.IsKinectInitialized())
		{
			levelLoaded = true;
			Application.LoadLevel(1);
		}
	}
	
}
