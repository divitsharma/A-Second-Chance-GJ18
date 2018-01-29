using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    bool done = false;

    GameObject pmo = null;
    GameObject fmo = null;

    GameManager pm = null;
    GameManager fm = null;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (pmo != null && fmo != null) return;


        Debug.Log("Looking for managers...");
        pmo = GameObject.FindGameObjectWithTag("PastManager");
        fmo = GameObject.FindGameObjectWithTag("FutureManager");

        if (pmo == null || fmo == null) return;

        pm = pmo.GetComponent<GameManager>();
        fm = fmo.GetComponent<GameManager>();

        pm.futureManager = fm;
        pm.pastManager = pm;
        fm.pastManager = pm;
        fm.futureManager = fm;

        done = true;
    }
}
