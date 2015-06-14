using UnityEngine;
using System.Collections;

public class LoadLevelScript : MonoBehaviour {

	public void LoadLevel(int level)
	{
		if(level == -1)
		{
			Application.Quit();
		}
		Application.LoadLevel(level);
	}
}
