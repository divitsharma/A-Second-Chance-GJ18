using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMapGenerator : MonoBehaviour {

    public int tileWidth;
    public int tileScale;
    public int worldSize;
    public int buildingFrequency;
    public int buildingWidth;
    public int buildingHeight;
    public int numSeed;
    public int safeFreq;

    public Vector3 startPos;
    public Vector3 spawnPoint;

    public string seed;
    public bool setRandomSeed; 

    public GameObject[] terrain;
    public GameObject buildingUnit1;
    public GameObject safe1;
    public GameObject seed1;
    public GameObject portal;

    void Start () {
        GenerateMap();
	}
	

	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            GenerateMap();
        }
    }

    void GenerateMap() {
        placeTerrain();
        placeBuildings();
        placeItems();
    }

    void placeTerrain() {
        if (setRandomSeed) {
            seed = Time.time.ToString();
        }

        Random.InitState(seed.GetHashCode());

        for (int i = 0; i < worldSize; ++i) {
            int rand = Random.Range(0, 2);
            Vector3 newVec = new Vector3(startPos.x + (i * tileWidth), startPos.y, startPos.z);
            GameObject temp = Instantiate(terrain[rand], newVec, Quaternion.identity);
            //temp.transform.localScale += newVec * tileScale;
            temp.AddComponent<PolygonCollider2D>();
        }
    }

    void placeBuildings() {
        int prevBuilding = 0;

        if (setRandomSeed) {
            seed = Time.time.ToString();
        }

        Random.InitState(seed.GetHashCode());

        for (int i = 0; i < buildingFrequency; ++i) {
            int rand = Random.Range(prevBuilding + buildingWidth + 10, prevBuilding + buildingWidth + 40);
            Debug.Log(rand);
            prevBuilding = rand;
            if (rand < ((worldSize * tileWidth) - buildingWidth)) {
                buildBuilding(rand, rand);
            }
        }
    }

    void buildBuilding(int start, int newSeed) {

        Random.InitState(newSeed.GetHashCode());

        int height = Random.Range(2, 5);
        Debug.Log(height);

        for (int i = 0; i < height; ++i) {
            Vector3 newVec = new Vector3(start + (buildingWidth / 2), startPos.y + (i * buildingHeight), startPos.z + 10);
            Instantiate(buildingUnit1, newVec, Quaternion.identity);
        }

        if (Random.Range(0, 100)  < safeFreq) {
            Instantiate(safe1, new Vector3(start + (buildingWidth / 2), startPos.y + (buildingHeight * height) - buildingHeight - 5
            , startPos.z + 5), Quaternion.identity);
        }

        
    }

    void placeItems() {
        for (int i = 0; i < numSeed; ++i) {
            Instantiate(seed1, new Vector3(Random.Range(startPos.x, tileWidth * worldSize), 
                startPos.y + 3, startPos.z), Quaternion.identity);
        }
    }

}



