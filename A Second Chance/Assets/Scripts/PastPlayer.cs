using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastPlayer : MonoBehaviour {

    GameManager gameManager;

    [SerializeField]
    Transform spawnPoint;


    // Objects that can be spawned into the past from the future
    // 0 is ball
    [SerializeField]
    GameObject[] objects;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            Debug.Log("A pressed");
            // tell manager to spawn an object in the future
            gameManager.SpawnInFuture(1, spawnPoint.position.x);
        }
    }

    public void UseItem(string name)
    {
        if (name == "seed1" || name == "Pink")
        {
            gameManager.SpawnInFuture(1, spawnPoint.position.x);
        }
    }


    // recieving a request to instantiate an object from the future
    public void Spawn(int objIndex)
    {
        Instantiate(objects[objIndex], spawnPoint.position, transform.rotation);
    }

    // must be called before communicating to the future
    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }
}
