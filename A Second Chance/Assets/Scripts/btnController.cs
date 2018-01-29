using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnController : MonoBehaviour {

	public string number;
	public GameObject safe;

	public void onClick() {
		safe.GetComponent<safeController>().addNum(number);
		Debug.Log(number);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
