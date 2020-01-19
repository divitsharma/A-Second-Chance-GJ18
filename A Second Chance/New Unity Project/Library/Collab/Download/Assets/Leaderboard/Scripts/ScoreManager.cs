using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;
// using UnityEditor;

public class ScoreManager : MonoBehaviour {

	public Dictionary<string, int> scores;

	public Text nameText;
	public Text scoreText;

	public InputField nameIF;
	public Text timerText;
	private string newName = "";
	private int newScore = 0;

	private string filePath = "Assets/Leaderboard/high_scores.txt";
	private string line = "";

	// Use this for initialization
	void Start () {
		
	}

	void Init() {
		scores = new Dictionary<string, int>();
		StreamReader sr = new StreamReader(filePath);
		while((line = sr.ReadLine()) != null) {
			string[] words = line.Split();
			int temp = 0;
			if ((words[0] != "" && words[0] != "\n") && int.TryParse(words[1], out temp)) scores[words[0]] = temp;
		}
		scores[newName] = newScore;
		sr.Close();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateScores() {
		newName = nameIF.text;
		newScore = timerText.GetComponent<TimerManager>().getScore();

		nameText.text = "Name\n";
		scoreText.text = "Score\n";

		Init();
		StreamWriter sw = new StreamWriter(filePath, false);
		var items = from pair in scores orderby pair.Value ascending select pair;

		foreach (KeyValuePair<string, int> pair in items) {
			nameText.text += pair.Key + "\n";
			scoreText.text += Mathf.Floor(pair.Value / 60000).ToString("00") + "." + Mathf.Floor(pair.Value / 1000).ToString("00") + "." + (pair.Value % 1000).ToString("000") + "\n";
			sw.WriteLine("\n" + pair.Key + " " + pair.Value);
		}

		sw.Close();
	}
}
