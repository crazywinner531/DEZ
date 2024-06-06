using UnityEngine;
using System;
using System.Collections.Generic;

public class UniquePairSelector : MonoBehaviour
{
    public int[,] matrix; // Matrix received from Blob script
    private void Awake()
    {
        enabled = false;
    }
    void Start()
    {
        if (matrix != null)
        {
            // Debug log the initial matrix
            Debug.Log("Initial Matrix:");
            LogMatrix(matrix);

            // Select unique pairs of adjacent cells with different values
            List<Tuple<int, int>> pairs = GetPairsWithValues(matrix, 1, 2);

            // Debug log selected pairs
            Debug.Log("Selected Pairs:");
            foreach (var pair in pairs)
            {
                Debug.Log("(" + pair.Item1 + ", " + pair.Item2 + ")");
            }
        }
        else
        {
            Debug.LogError("Matrix is null. Please ensure Blob script sets the matrix.");
        }
    }

    // Function to get pairs of adjacent cells with specified values
    List<Tuple<int, int>> GetPairsWithValues(int[,] matrix, int value1, int value2)
    {
        List<Tuple<int, int>> pairs = new List<Tuple<int, int>>();

        // Iterate over the matrix to find pairs of adjacent cells with specified values
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (IsCellValid(matrix, i, j) && matrix[i, j] == value1)
                {
                    // Check neighboring cells (right and bottom)
                    if (j + 1 < matrix.GetLength(1) && matrix[i, j + 1] == value2)
                    {
                        pairs.Add(new Tuple<int, int>(i, j));
                    }
                    if (i + 1 < matrix.GetLength(0) && matrix[i + 1, j] == value2)
                    {
                        pairs.Add(new Tuple<int, int>(i, j));
                    }
                }
                else if (IsCellValid(matrix, i, j) && matrix[i, j] == value2)
                {
                    // Check neighboring cells (right and bottom)
                    if (j + 1 < matrix.GetLength(1) && matrix[i, j + 1] == value1)
                    {
                        pairs.Add(new Tuple<int, int>(i, j));
                    }
                    if (i + 1 < matrix.GetLength(0) && matrix[i + 1, j] == value1)
                    {
                        pairs.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
        }

        return pairs;
    }

    // Function to check if a cell is valid (non-negative and not a border cell)
    bool IsCellValid(int[,] matrix, int x, int y)
    {
        return (x >= 0 && y >= 0 && x < matrix.GetLength(0) && y < matrix.GetLength(1));
    }

    // Function to debug log the matrix
    void LogMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            string row = "";
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                row += matrix[i, j] + "\t";
            }
            Debug.Log(row);
        }
    }
}
