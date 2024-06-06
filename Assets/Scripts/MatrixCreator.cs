using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixCreator : MonoBehaviour
{
    int MaxX,MaxZ;
    public int[,] matrix;

    int count = 0;

    int publicSpace, privatespace;

    bool flagL,flagk,flagd,flagb1,flagb2,flagba1,flagba2;

    List<Vector2> publicNeighbours = new List<Vector2>();

    List<Vector2> blobcells = new List<Vector2>();

    Vector2 StartPoint;
    private void Awake()
    {
        enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        flagb1 = false; flagb2 = false;
        flagba1 = false; flagba2 = false;
        flagL = false; flagk = false; flagd = false;
        PlotCreation plotCreation = GetComponent<PlotCreation>();
        if(plotCreation != null)
        {
            MaxX = plotCreation.plotX;
            MaxZ = plotCreation.plotZ;
        }
        matrix = new int[MaxX, MaxZ];

        for(int i = 0; i < MaxX; i++)
        {
            for(int j = 0; j < MaxZ; j++)
            {
                GameObject suspect = GameObject.Find("GridCell_" + i + "_" + j);
                if(suspect != null)
                {
                    if(suspect.transform.GetChild(0).GetComponent<Renderer>().material.color == new Color(0x57 / 255f, 0xDA / 255f, 0x61 / 255f))
                    {
                        matrix[i, j] = -1;
                    } 
                    else if(suspect.transform.GetChild(0).GetComponent<Renderer>().material.color == new Color(0xBA / 255f, 0x2C / 255f, 0x23 / 255f))
                    {
                        matrix[i, j] = -1;
                    }
                    else if(suspect.transform.GetChild(0).GetComponent<Renderer>().material.color == new Color(0x53 / 255f, 0x4C / 255f, 0x87 / 255f))
                    {
                        matrix[i, j] = 1;
                        blobcells.Add(new Vector2(i, j));
                        count++;
                    }
                    else if (suspect.transform.GetChild(0).GetComponent<Renderer>().material.color == new Color(0xDC / 255f, 0x97 / 255f, 0x24 / 255f))
                    {
                        matrix[i, j] = 3;
                        flagk = true;
                        count++;
                    }
                    else if (suspect.transform.GetChild(0).GetComponent<Renderer>().material.color == new Color(0xBF / 255f, 0x23 / 255f, 0xDC / 255f))
                    {
                        matrix[i, j] = 4;
                        flagd = true;
                        count++;
                    }
                    else if (suspect.transform.GetChild(0).GetComponent<Renderer>().material.color == new Color(0x1F / 255f, 0xEF / 255f, 0xE5 / 255f))
                    {
                        matrix[i, j] = 5;
                        flagb1 = true;
                        count++;
                    }
                    else if (suspect.transform.GetChild(0).GetComponent<Renderer>().material.color == new Color(0xC5 / 255f, 0xFC / 255f, 0x50 / 255f))
                    {
                        matrix[i, j] = 6;
                        flagb2 = true;
                        count++;
                    }
                    else if(suspect.transform.GetChild(0).GetComponent<Renderer>().material.color == new Color(0xD6 / 255f, 0xE4 / 255f, 0x7A / 255f))
                    {
                        StartPoint = new Vector2(i, j);
                        matrix[i, j] = 2;
                        flagL = true;
                        count++;
                    }
                }
                
                
            }
        }

        if (flagL == false)
        {
            int randomIndex = Random.Range(0, blobcells.Count);
            matrix[(int)blobcells[randomIndex].x, (int)blobcells[randomIndex].y] = 2;
            blobcells.RemoveAt(randomIndex);
        }

        if (flagk == false)
        {
            int randomIndex = Random.Range(0, blobcells.Count);
            matrix[(int)blobcells[randomIndex].x, (int)blobcells[randomIndex].y] = 3;
            blobcells.RemoveAt(randomIndex);
        }

        if (flagd == false)
        {
            int randomIndex = Random.Range(0, blobcells.Count);
            matrix[(int)blobcells[randomIndex].x, (int)blobcells[randomIndex].y] = 4;
            blobcells.RemoveAt(randomIndex);
        }

        if (flagb1 == false)
        {
            int randomIndex = Random.Range(0, blobcells.Count);
            matrix[(int)blobcells[randomIndex].x, (int)blobcells[randomIndex].y] = 5;
            blobcells.RemoveAt(randomIndex);
        }

        if (flagb2 == false)
        {
            int randomIndex = Random.Range(0, blobcells.Count);
            matrix[(int)blobcells[randomIndex].x, (int)blobcells[randomIndex].y] = 6;
            blobcells.RemoveAt(randomIndex);
        }

        if (flagba1 == false)
        {
            int randomIndex = Random.Range(0, blobcells.Count);
            matrix[(int)blobcells[randomIndex].x, (int)blobcells[randomIndex].y] = 7;
            blobcells.RemoveAt(randomIndex);
        }

        if (flagba2 == false)
        {
            int randomIndex = Random.Range(0, blobcells.Count);
            matrix[(int)blobcells[randomIndex].x, (int)blobcells[randomIndex].y] = 8;
            blobcells.RemoveAt(randomIndex);
        }

        Debug.Log("Matrix:");
        string rowValues = "";
        // Iterate over each row
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            

            // Iterate over each column in the current row
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                // Append the current value to the rowValues string
                rowValues += matrix[i, j] + "\t";
            }
            rowValues += "\n";
            // Print the values of the current row
            
        }
        Debug.Log(rowValues);





        //publicSpace = 0;
        //privatespace = 0;

        //publicNeighbours.Add(StartPoint);

        //while(publicSpace <= count / 2 && publicNeighbours.Count > 0)
        //{
        //    int randomIndex = Random.Range(0, publicNeighbours.Count);
        //    Debug.Log((int)publicNeighbours[randomIndex].x+"\t" + (int)publicNeighbours[randomIndex].y);

        //    int k = matrix[(int)publicNeighbours[randomIndex].x, (int)publicNeighbours[randomIndex].y];

        //    if (k == 0)
        //    {
        //        publicSpace++;
        //    }
        //    else if ((k == 1 || k == 8 || k == 4))
        //    {
        //        matrix[(int)publicNeighbours[randomIndex].x, (int)publicNeighbours[randomIndex].y] = 2;
        //        publicSpace++;
        //    }

        //    int l, r, u, d;

        //    d = matrix[(int)publicNeighbours[randomIndex].x, (int)publicNeighbours[randomIndex].y - 1];

        //    if(d == 1 ||d == 8 || d == 4) 
        //    {
        //        publicNeighbours.Add(new Vector2((int)publicNeighbours[randomIndex].x, (int)publicNeighbours[randomIndex].y - 1));

        //    }

        //    u = matrix[(int)publicNeighbours[randomIndex].x, (int)publicNeighbours[randomIndex].y + 1];

        //    if (u == 1 || u == 8 || u == 4) 
        //    {
        //        publicNeighbours.Add(new Vector2((int)publicNeighbours[randomIndex].x, (int)publicNeighbours[randomIndex].y + 1));

        //    }

        //    r = matrix[(int)publicNeighbours[randomIndex].x + 1, (int)publicNeighbours[randomIndex].y];

        //    if (r == 1 ||r == 8 || r == 4)
        //    {
        //        publicNeighbours.Add(new Vector2((int)publicNeighbours[randomIndex].x + 1, (int)publicNeighbours[randomIndex].y));

        //    }

        //    l = matrix[(int)publicNeighbours[randomIndex].x - 1, (int)publicNeighbours[randomIndex].y];

        //    if (l == 1 ||l == 8 ||l == 4)
        //    {
        //        publicNeighbours.Add(new Vector2((int)publicNeighbours[randomIndex].x - 1, (int)publicNeighbours[randomIndex].y));
        //    }





        //    publicNeighbours.RemoveAt(randomIndex);
        //}

        //privatespace = count - publicSpace;

        Blob blob = GetComponent<Blob>();
        if (blob != null)
        {
            blob.matrix = new int[MaxX, MaxZ];
            blob.matrix = matrix;
            blob.enabled = true;
            
        }
        UniquePairSelector uniquePairSelector = GetComponent<UniquePairSelector>();
        if (uniquePairSelector != null)
        {
            uniquePairSelector.matrix = new int[MaxX, MaxZ];
            uniquePairSelector.matrix = matrix;
            uniquePairSelector.enabled = true;
        }


        Debug.Log("Matrix:");
        rowValues = "";
        // Iterate over each row
        for (int i = 0; i < matrix.GetLength(0); i++)
        {


            // Iterate over each column in the current row
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                // Append the current value to the rowValues string
                rowValues += matrix[i, j] + "\t";
            }
            rowValues += "\n";
            // Print the values of the current row

        }
        Debug.Log(rowValues);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
