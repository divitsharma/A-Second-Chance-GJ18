using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastPlayer : MonoBehaviour {

    GameManager gameManager;


    // Objects that can be spawned into the past from the future
    // 0 is ball
    [SerializeField]
    GameObject[] objects;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("A pressed");
            // tell manager to spawn an object in the future
            gameManager.SpawnInFuture(0);
        }
    }


    // recieving a request to instantiate an object from the future
    public void Spawn(int objIndex)
    {
        Instantiate(objects[objIndex], transform.position, transform.rotation);
    }

    // must be called before communicating to the future
    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }
}
