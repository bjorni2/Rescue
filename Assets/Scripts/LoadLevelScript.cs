using UnityEngine;
using System.Collections;

public class LoadLevelScript : MonoBehaviour {

	public void LoadLevel(int level)
	{
		Application.LoadLevel(level);
	}
}
