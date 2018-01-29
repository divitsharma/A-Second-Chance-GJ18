using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    GameObject inventoryScreen;
    [SerializeField]
    GameObject inventoryGraphic;

    // Use this for initialization
    void Start () {

        int myLayer = LayerMask.NameToLayer("PlayerLayer");

        int toAvoid = LayerMask.NameToLayer("ItemLayer");

        Physics2D.IgnoreLayerCollision(myLayer,toAvoid, true);
		
	}

    /*void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Item") {

            Debug.Log("Collided!");

            Collider2D objectCollider = GetComponent<Collider2D>();

            Collider2D toIgnoreCollider = collision.gameObject.GetComponent<Collider2D>();

            Physics2D.IgnoreCollision(toIgnoreCollider, objectCollider);
        }

    }*/


        // Update is called once per frame
        void Update () {

        if(Input.GetKeyDown("i")) {
            

            if (!inventoryScreen.activeSelf || !inventoryGraphic.gameObject.activeSelf) {

                inventoryScreen.SetActive(true);
                inventoryGraphic.SetActive(true);
                //Time.timeScale = 1;

            }
            
            else {

                inventoryScreen.SetActive(false);
                //Time.timeScale = 0;

            }

        }
		
	}
}
