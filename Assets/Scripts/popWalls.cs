using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class popWalls : MonoBehaviour
{
    public GameObject horizontalWallPrefab; // Horizontal wall prefab
    public GameObject verticalWallPrefab;   // Vertical wall prefab
    public GameObject roof;
    public float wallOffset = 0.5f;         // Offset for positioning walls

    public int[,] matrix; // Your matrix with different values


    private void Awake()
    {
        enabled = false;
    }
    void Start()
    {

    }

    private void Update()
    {
        
    }

    public void ClearWalls()
    {
        // Find all game objects with the "Wall" tag and destroy them
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {
            Destroy(wall);
        }
    }

    public void InstantiateWalls()
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        // Iterate through the matrix
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (matrix[i,j] != -1)
                {
                    Instantiate(roof, new Vector3(i,2.75f,j), Quaternion.identity);
                }
                
                // Check if current cell is not on the last row
                if (i < rows - 1)
                {
                    // Check if value in current cell is different from value in cell below it
                    if (matrix[i, j] != matrix[i + 1, j])
                    {
                        // Calculate position for horizontal wall
                        Vector3 position = new Vector3(i + wallOffset, 1.4f, j);


                        // Instantiate horizontal wall prefab at calculated position
                        Instantiate(horizontalWallPrefab, position, Quaternion.identity);
                    }
                }

                // Check if current cell is not on the last column
                if (j < cols - 1)
                {
                    // Check if value in current cell is different from value in cell to the right
                    if (matrix[i, j] != matrix[i, j + 1])
                    {
                        // Calculate position for vertical wall
                        Vector3 position = new Vector3(i, 1.4f, j + wallOffset);

                        // Instantiate vertical wall prefab at calculated position
                        Instantiate(verticalWallPrefab, position, Quaternion.identity);
                    }
                }
            }
        }
    }
}
