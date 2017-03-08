using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class MapGenerator : MonoBehaviour {
    [Range(0, 100)] public int randomFillPercent;
    public bool useRandomSeed;
    public string seed;

    private Map map;
    private int[,] grid;

    private void Start() {
        if (useRandomSeed) {
            seed = RandomString(10);
        }
    }

    private string RandomString(int length) {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public int[,] Generate(Map map) {
        this.map = map;
        grid = new int[map.width, map.height];
        RandomFillMap();
        for (int i = 0; i < 5; i++) {
            SmoothMap();
        }
        return grid;
    }

    private void RandomFillMap() {
        Random pseudoRandom = new Random(seed.GetHashCode())
            ;
        for (int x = 0; x < map.width; x++) {
            for (int y = 0; y < map.height; y++) {
                if (x == 0 || x == map.width - 1 || y == 0 || y == map.height - 1) {
                    grid[x, y] = 1;
                }
                else {
                    grid[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    private void SmoothMap() {
        for (int x = 0; x < map.width; x++) {
            for (int y = 0; y < map.height; y++) {
                int walls = GetSurroundingWallCount(x, y);
                if (walls > 4) {
                    grid[x, y] = 1;
                }
                else if (walls < 4) {
                    grid[x, y] = 0;
                }
            }
        }
    }

    public int GetSurroundingWallCount(int gridX, int gridY) {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
                if (map.IsInMapRange(neighbourX, neighbourY)) {
                    if (neighbourX != gridX || neighbourY != gridY) {
                        wallCount += grid[neighbourX, neighbourY];
                    }
                }
                else {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    private void OnDrawGizmos() {
        /*if (map != null) {
             for (int x = 0; x < width; x++) {
                 for (int y = 0; y < height; y++) {
                     Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
                     Vector3 pos = new Vector3(-width / 2 + x + .5f, -height / 2 + y + .5f, 0);
                     Gizmos.DrawCube(pos, Vector3.one);
                 }
             }
         }*/
    }
}