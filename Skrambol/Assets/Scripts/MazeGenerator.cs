using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{

    public GameObject blockPrefab;

    public GameObject blockParent;

    public GameObject player;

    public int size;

    private bool[,] maze;

    // Start is called before the first frame update
    void Start()
    {
        maze = GenerateMaze();

        for (int z = 0; z < size; z++) {
            for (int x = 0; x < size; x++) {
                if (maze[z, x]) {
                    CreateChildPrefab(blockPrefab, blockParent, x, 1, z);
                    CreateChildPrefab(blockPrefab, blockParent, x, 2, z);
                    // CreateChildPrefab(blockPrefab, blockParent, x, 3, z);
                }

                CreateChildPrefab(blockPrefab, blockParent, x, 0, z);
            }
        }
    }

    bool[,] GenerateMaze() {
        bool[,] map = new bool[size, size];
        for (int z = 0; z < size; z++) {
            for (int x = 0; x < size; x++) {
                map[z, x] = true;
            }
        }

        int tilesConsumed = 0;
        int tilesToRemove = (size * size) / 3;
        int mazeX = Random.Range(1, size - 2), mazeY = Random.Range(1, size - 2);

        while (tilesConsumed < tilesToRemove) {
            int xDir = 0, yDir = 0;

            if (Random.value < 0.5) {
                xDir = Random.value < 0.5 ? 1 : -1;
            } else {
                yDir = Random.value < 0.5 ? 1 : -1;
            }

            int spacesMove = (int)(Random.Range(1, size - 1));

            for (int i = 0; i < spacesMove; i++) {
                mazeX = Mathf.Clamp(mazeX + xDir, 1, size - 2);
                mazeY = Mathf.Clamp(mazeY + yDir, 1, size - 2);

                if (map[mazeY, mazeX]) {
                    map[mazeY, mazeX] = false;
                    tilesConsumed++;
                }
            }
        }
        Instantiate(player, new Vector3(1, 3, 1), Quaternion.identity);
        return map;
    }

    void CreateChildPrefab(GameObject prefab, GameObject parent, int x, int y, int z) {
        var childPrefab = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
        childPrefab.transform.parent = parent.transform;
    }
}
