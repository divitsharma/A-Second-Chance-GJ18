using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    [SerializeField]
    private List<Item> permInvArray;
    [SerializeField]
    private List<Item> throwableInvArray;
    private int DNACount = 0;
    [SerializeField]
    private GameObject invScreen;
    [SerializeField]
    private List<Vector2> invPositions;// = GameObject.Find("InventoryScreen").GetComponent<InventoryScreen>().invPositions;

    // Use this for initialization
    void Start () {
        invPositions = GameObject.Find("InventoryScreen").GetComponent<InventoryScreen>().invPositions;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("e")) {

            //Debug.Log("e key was pressed");
            Vector2 center = GameObject.FindGameObjectWithTag("Player").transform.position;
            //string position = center.ToString();
            //Debug.Log(position);

            Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(center, 2);

            int arrayLength = nearbyColliders.Length;

            for(int i=0; i < arrayLength; ++i) {

                Item colliderItem = nearbyColliders[i].GetComponent<Item>();
                string colliderName = nearbyColliders[i].gameObject.name;
                string colliderTag = nearbyColliders[i].gameObject.tag;
                if (colliderTag == "Item") {

                    //Debug.Log(colliderName);
                    if (colliderItem.getType() == "Throwable") {

                        int index = throwableInvArray.Count;

                        if (0 <= index && index < 9) {

                            if (colliderItem.getName() == "DNA") {
                                DNACount++;
                                if (DNACount == 9) {
                                    GameObject.Find("Timer").GetComponent<TimerManager>().stopTime();
                                }
                            }

                            throwableInvArray.Add(colliderItem);

                            //Debug.Log(index);

                            colliderItem.GetComponent<Item>().inventorySprite.gameObject.transform.localPosition = invPositions[index];
                            colliderItem.GetComponent<Item>().inventorySprite.GetComponent<SpriteRenderer>().enabled = true;

                            invScreen.GetComponent<InventoryScreen>().slotsFilled[index] = true;

                            nearbyColliders[i].gameObject.SetActive(false);

                        }
                    } 
                    else if (colliderItem.getType() == "Permanent") {
                        permInvArray.Add(colliderItem);
                        nearbyColliders[i].gameObject.SetActive(false);
                    } 

                }

            }

        }

        if(Input.GetKeyDown("q")) {

            int invSize = throwableInvArray.Count;

            if(invSize > 0) {

                Item lastElement = throwableInvArray[invSize - 1];

                if (lastElement.getName() == "DNA") DNACount--;

                Vector2 playerPosition = gameObject.transform.position;

                lastElement.transform.position = playerPosition; //changes position of item in inventory to where player is

                lastElement.gameObject.SetActive(true);

                invScreen.GetComponent<InventoryScreen>().slotsFilled[invSize-1] = false; //removes from inventory gui

                lastElement.inventorySprite.GetComponent<SpriteRenderer>().enabled = false; //disables sprite renderer

                throwableInvArray.RemoveAt(invSize - 1); //removes it from inventory
            }

        }

    }
}
