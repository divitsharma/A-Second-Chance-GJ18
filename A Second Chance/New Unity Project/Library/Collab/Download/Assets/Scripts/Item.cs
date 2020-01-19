using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField]
    private string itemName;
    [SerializeField]
    private string itemType;
    [SerializeField]
    public GameObject inventorySprite;

    public string getName() {
        return itemName;
    }

    public string getType() {
        return itemType;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
