using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

	public GameObject leaderboardScreen;

	public Text timerText;
	private float total = 0;
	private float msec = 0;
	private int sec = 0;
	private int min = 0;

	private bool runTime = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (runTime) {
			float toAdd = Time.deltaTime;
			msec += Mathf.Floor((toAdd * 1000) % 1000);
			total += Mathf.Floor((toAdd * 1000) % 1000);
			
			if (msec >= 1000) {
				sec++;
				msec = 0;
			}
			if (sec >= 60) {
				min++;
				sec = 0;
			}
			timerText.text = "Time: " + min.ToString("00") + "." + sec.ToString("00") + "." + msec.ToString("000");
		}
	}

	public void stopTime() {
		runTime = false;
		leaderboardScreen.SetActive(true);
	}

	public void restart() {
		total = 0;
		msec = 0;
		sec = 0;
		min = 0;
		runTime = true;
		leaderboardScreen.SetActive(false);
	}

	public int getScore() {
		return (int)total;
	}

	public float getMSec() {
		return msec;
	}

	public int getSec() {
		return sec;
	}

	public int getMin() {
		return min;
	}
	
}
