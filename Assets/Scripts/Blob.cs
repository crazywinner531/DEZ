using UnityEngine;
using System.Collections.Generic;

public class Blob : MonoBehaviour
{
    public int[,] matrix ;
    public int[,] temp;
    private void Awake()
    {
        enabled = false;
    }
    void Start()
    {
        MatrixCreator matrixCreator = FindObjectOfType<MatrixCreator>();
        if (matrixCreator != null)
        {
            matrix = matrixCreator.matrix;
            temp = matrix;
            // Debug log the initial matrix
            Debug.Log("Initial Matrix:");

            for (int i = 0; i < matrixCreator.matrix.GetLength(0); i++)
            {
                string row = "";
                for (int j = 0; j < matrixCreator.matrix.GetLength(1); j++)
                {
                    row += matrix[i, j] + "\t";
                }
                Debug.Log(row);
            }

            // Spread blobs over 1s
            

            // Debug log the final matrix
            Debug.Log("Final Matrix:");
            for (int i = 0; i < matrixCreator.matrix.GetLength(0); i++)
            {
                string row = "";
                for (int j = 0; j < matrixCreator.matrix.GetLength(1); j++)
                {
                    row += matrix[i, j] + "\t";
                }
                Debug.Log(row);
            }
        }
        popWalls popwalls = GetComponent<popWalls>();
        if(popwalls != null)
        {
            popwalls.matrix = matrix;
            popwalls.enabled = true;
        }
        FinalTouch finalTouch = GetComponent<FinalTouch>();
        if(finalTouch != null)
        {
            finalTouch.enabled = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)== true)
        {
            Debug.Log("gokul");
            matrix = temp;
            bool hasChanged = true;
            while (hasChanged)
            {
                hasChanged = SpreadBlobs();
            }

            popWalls popwalls = GetComponent<popWalls>();
            
                popwalls.enabled = true;
            
            if(popwalls != null )
            {
                popwalls.matrix = matrix;
                popwalls.ClearWalls();
                popwalls.InstantiateWalls();
            }
            popwalls.enabled = false;
        }
        
    }

    // Function to spread blobs over 1s with percentages while maintaining blob structure
    bool SpreadBlobs()
    {
        bool hasChanged = false;

        // Create a copy of the matrix to track changes
        int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                newMatrix[i, j] = matrix[i, j];
            }
        }

        // Iterate over the matrix
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                // If the cell has a number, spread it to neighboring 1 cells
                if (matrix[i, j] >= 2)
                {
                    hasChanged |= SpreadNumber(newMatrix, i, j, matrix[i, j]);
                }
            }
        }

        // Update the matrix with the changes
        if (hasChanged)
        {
            matrix = newMatrix;
        }

        return hasChanged;
    }

    // Function to spread a number to neighboring 1 cells with percentages while maintaining blob structure
    bool SpreadNumber(int[,] newMatrix, int x, int y, int number)
    {
        bool hasChanged = false;
        int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }; // Right, Down, Left, Up

        // Calculate the total percentage sum
        int totalPercentage = 0;
        int[] percentages = { 25, 12, 13, 5, 20, 20, 5 }; // Percentages for numbers 2 to 8
        for (int i = 2; i <= 8; i++)
        {
            totalPercentage += percentages[i - 2];
        }

        // Calculate the number of cells to spread based on percentage
        int cellsToSpread = Mathf.RoundToInt((totalPercentage / 100f) * CountAdjacentOnes(x, y));

        // Spread the number to neighboring 1 cells based on percentage
        List<int[]> neighbors = GetAdjacentOnes(x, y);
        Shuffle(neighbors); // Shuffle the list to spread randomly
        int spreadCount = Mathf.Min(cellsToSpread, neighbors.Count); // Spread to at most the number of neighboring 1 cells
        for (int i = 0; i < spreadCount; i++)
        {
            int[] neighbor = neighbors[i];
            int nx = neighbor[0];
            int ny = neighbor[1];
            newMatrix[nx, ny] = number;
            hasChanged = true;
        }

        return hasChanged;
    }

    // Function to count the number of neighboring 1 cells
    int CountAdjacentOnes(int x, int y)
    {
        int count = 0;
        int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }; // Right, Down, Left, Up
        
        for (int d = 0; d < 4; d++)
        {
            int newX = x + directions[d, 0];
            int newY = y + directions[d, 1];
            if (newX >= 0 && newX < matrix.GetLength(0) && newY >= 0 && newY < matrix.GetLength(1) && matrix[newX, newY] == 1)
            {
                count++;
            }
        }
        return count;
    }

    // Function to get neighboring 1 cells
    List<int[]> GetAdjacentOnes(int x, int y)
    {
        List<int[]> neighbors = new List<int[]>();
        int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }; // Right, Down, Left, Up
        for (int d = 0; d < 4; d++)
        {
            int newX = x + directions[d, 0];
            int newY = y + directions[d, 1];
            if (newX >= 0 && newX < matrix.GetLength(0) && newY >= 0 && newY < matrix.GetLength(1) && matrix[newX, newY] == 1)
            {
                neighbors.Add(new int[] { newX, newY });
            }
        }
        return neighbors;
    }

    // Function to shuffle a list
    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    // Function to debug log the matrix

}
