using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturePlayer : MonoBehaviour {

    GameManager gameManager;
    [SerializeField] Transform spawnPoint;

    // Objects that can be spawned into the future from the past
    // 0 is ball
    [SerializeField]
    GameObject[] objects;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.C))
        {
            Debug.Log("S pressed");
            // tell manager to spawn an object in the future
            gameManager.SpawnInPast(0);
        }
    }

    public void UseItem(string name)
    {

    }



    // recieving a request to instantiate an object from the past
    public void Spawn(int objIndex, float x)
    {
        Instantiate(objects[objIndex], new Vector3(x, spawnPoint.position.y, spawnPoint.position.z), transform.rotation);
    }

    // must be called before communicating to the future
    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }
}
