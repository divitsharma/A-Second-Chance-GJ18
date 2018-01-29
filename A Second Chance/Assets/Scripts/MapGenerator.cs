using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public int width;
    public int height;
    public int terrainMaxHeight;
    public int maxBuildingHeight;
    public int maxBuildingWidth;

    public string seed;
    public bool useRandomSeed = false;

    private bool overhangsRemoved = false;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;

    /* Type of element to number 
     * Rock - 1
     * Air - 0
     * Gold - 2;
     */

    void Start() {
        GenerateMap();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GenerateMap();
        }
    }

    void GenerateMap() {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; ++i) {
            SmoothTerrain();
        }

        RemoveOverhangs();
        //AddTallBuildings();

        MeshGenerator meshGen = GetComponent<MeshGenerator>();
        meshGen.GenerateMesh(map, 1);
    }

    void RandomFillMap() {
        if (useRandomSeed) {
            seed = Time.time.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; ++x) {
            for (int y = 0; y < height; ++y) {
                if (y >= terrainMaxHeight) {
                    map[x, y] = 0;
                    continue;
                }
                if (y == 0 || y == 1) {
                    map[x, y] = 1;
                } else {
                    if (pseudoRandom.Next(0, 100) < (randomFillPercent / 3)) {
                        map[x, y] = 1;
                    } else if (pseudoRandom.Next(0, 100) < randomFillPercent) {
                        map[x, y] = 1;
                    } else {
                        map[x, y] = 0;
                    }
                }
            }
        }
    }

    void SmoothTerrain() {

        System.Random pseudoRandom = new System.Random(Time.time.ToString().GetHashCode());

        for (int x = 0; x < width; ++x) {
            for (int y = 0; y < height; ++y) {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4) {
                    map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 1;
                } else if (neighbourWallTiles < 4) {
                    map[x, y] = 0;
                }
            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY) {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height) {
                    if (neighbourX != gridX || neighbourY != gridY) {
                        wallCount += map[neighbourX, neighbourY];
                    }
                } else {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    void RemoveOverhangs() {
        if (!overhangsRemoved) {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    if (y > terrainMaxHeight) {
                        map[x, y] = 0;
                    }
                }
            }
            overhangsRemoved = true;
        }
    }

    //void AddTallBuildings() {
    //    for (int x = 0; x < width; x++) {
    //        if (Random.Range(0, 100) < 20) {
    //            int bWidth = Random.Range(4, maxBuildingWidth);
    //            BuildBuilding(x, bWidth);
    //            x += bWidth;
    //        }
    //    }
    //}

    //void BuildBuilding(int startPos, int bWidth) {
    //    int bHeight = Random.Range(4, maxBuildingHeight);
    //    for (int x = startPos; x < bWidth; ++x ) {
    //        for (int y = terrainMaxHeight - 2; y < bHeight; ++y) {
    //            map[x, y] = 3;
    //        }
    //    }
    //}

    void OnDrawGizmos() {

        //Matrix4x4 rotationMatrix = Matrix4x4.Rotate(transform.rotation);
        //Gizmos.matrix = rotationMatrix;

        //if (map != null) {
        //    for (int x = 0; x < width; x++) {
        //        for (int y = 0; y < height; y++) {
        //            if (map[x, y] == 1) {
        //                Gizmos.color = Color.black;
        //            } else if (map[x, y] == 2) {
        //                Gizmos.color = Color.black;
        //            } else if (map[x, y] == 3) {
        //                Gizmos.color = Color.red;
        //            } else {
        //                Gizmos.color = Color.white;
        //            }
        //            Vector3 pos = new Vector3(-width / 2 + x + .5f, 0, -height / 2 + y + .5f);
        //            Gizmos.DrawCube(pos, Vector3.one);
        //        }
        //    }
        //}
    }



}