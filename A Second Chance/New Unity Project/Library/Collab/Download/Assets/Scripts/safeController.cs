using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safeController : MonoBehaviour {

	public GameObject insideSafe;

	public int passcode = 1234;

	private string usercodeStr = "";
	public int usercode = 0;

	public void addNum(string num) {
		usercodeStr += num;
		if (usercodeStr.Length == 4) {
			int temp = 0;
			if (int.TryParse(usercodeStr, out temp)) {
				usercode = temp;
				if (usercode == passcode) {
					insideSafe.SetActive(true);
				}
				else usercode = 0;
			}
		}
	} 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
