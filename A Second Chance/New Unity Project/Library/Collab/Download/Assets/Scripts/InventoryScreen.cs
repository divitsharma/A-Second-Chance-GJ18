using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScreen : MonoBehaviour {
    [SerializeField]
    public List<bool> slotsFilled;
    [SerializeField]
    public List<Vector2> invPositions;

    // Use this for initialization
    void Start () {

        for(int i = 0; i < 9; ++i) {

            slotsFilled.Add(false);
            //Debug.Log("slot " + i + " initialized");

        }

        invPositions.Add(new Vector2(-1.221333f, 1.53f));
        //Debug.Log(invPositions.Count);
        invPositions.Add(new Vector2(0f, 1.53f));
        invPositions.Add(new Vector2(1.249f, 1.53f));
        invPositions.Add(new Vector2(-1.228f, 0.34f));
        invPositions.Add(new Vector2(0.01f, 0.34f));
        invPositions.Add(new Vector2(1.249f, 0.34f));
        invPositions.Add(new Vector2(-1.228f, -0.856f));
        invPositions.Add(new Vector2(0.0011f, -0.856f));
        invPositions.Add(new Vector2(1.25f, -0.856f));

        Debug.Log("invPositions initialized!");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
