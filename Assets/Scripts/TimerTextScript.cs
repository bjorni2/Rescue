using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerTextScript : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Text text = GetComponent<Text>();
		text.text = Time.timeSinceLevelLoad.ToString();
	}
}
