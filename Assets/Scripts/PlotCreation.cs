using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlotCreation : MonoBehaviour
{
    public Material originalMaterial;

    public bool Toggle = true;
    public int plotX, plotZ;
    public GameObject cell;

    public bool Correct = false;
    // Start is called before the first frame update
    void Start()
    {
        plotX = 10;
        plotZ = 10;
    }

    // Update is called once per frame
    void Update()
    {
        while (Toggle)
        {
            Toggle = false;
            if(plotX < plotZ)
            {
                int temp = plotX;
                plotX = plotZ;
                plotZ = temp;
            }
            // Setting the Size of the " Ground " 

            GameObject ground = GameObject.Find("Ground");
            if (ground != null)
            {
                ground.transform.localScale = new Vector3(plotX, 1, plotZ);
            }

            //Setting the Size of the " Grid "

            GameObject grid = GameObject.Find("Grid");
            if (grid != null)
            {
                grid.transform.localScale = new Vector3(plotX, 1, plotZ);
                GameObject parentObject = GameObject.Find("Grid");
                if(parentObject != null)
                {
                    // Deletion of already existing cells
                    foreach (Transform child in parentObject.transform)
                    {
                        // Check if the child's name is not "pivot"
                        if (child.gameObject.name != "Pivot")
                        {
                            // Destroy the child GameObject
                            Destroy(child.gameObject);
                        }
                    }

                    // Creation of new cells

                    for (int i = 0; i < plotX; i++)
                    {
                        for (int j = 0; j < plotZ; j++)
                        {
                            GameObject gridcell = GameObject.Instantiate(cell,parentObject.transform);
                            gridcell.transform.GetChild(0).GetComponent<Renderer>().material = new Material(originalMaterial);
                            gridcell.transform.position = new Vector3(i, parentObject.transform.position.y, j);
                            gridcell.name = "GridCell_"+i+"_"+j;
                            gridcell.transform.localScale = new Vector3((float)1 / plotX, 1f, (float)1 / plotZ);
                            CellRule cellrule = gridcell.transform.GetChild(0).GetComponent<CellRule>();
                            cellrule.enabled = true;
                            
                        }
                    }
                }
            }

        }

        // Check if the required Plot size is entered by the user

        if (Correct)
        {
            Correct = false;
            WallDraw wallDraw = GetComponent<WallDraw>();
            wallDraw.enabled = true;
            Camera orthoCamera = Camera.main;
            float aspectRatio = plotX / plotZ;
            float orthoSize = Mathf.Max(plotX / 2f, plotZ / 2f); // Take the maximum of half length and half width
            orthoCamera.orthographicSize = orthoSize;

            // Adjust camera's aspect ratio
            orthoCamera.aspect = aspectRatio;

            // Position the camera at the center of the rectangle
            Vector3 cameraPosition = new Vector3(plotX / 2f, 2f, plotZ / 2f);
            orthoCamera.transform.position = cameraPosition;
            enabled = false;
        }
    }
}
