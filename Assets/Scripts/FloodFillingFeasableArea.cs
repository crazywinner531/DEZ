using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloodFillingFeasableArea : MonoBehaviour
{
    public Vector3 centroid;
    public bool kitchenCoord;
    public Vector2 kitchenCord;
    public bool dinningCoord;
    public Vector2 dinningCord;
    public bool bedroom1Coord;
    public Vector2 bedroom1Cord;
    public bool bedroom2Coord;
    public Vector2 bedroom2Cord;
    public bool complete;
    void Awake()
    {
        enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        kitchenCord = Vector2.zero;
        dinningCord = Vector2.zero;
        bedroom1Cord = Vector2.zero;
        bedroom2Cord = Vector2.zero;
        WallDraw wallDraw = GetComponent<WallDraw>();
        if (wallDraw != null)
        {
            Flodder(wallDraw.approx);
        }
        GameObject.Find("Ground").SetActive(false);
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0) && kitchenCoord == false && dinningCoord == false && bedroom1Coord == false && bedroom2Coord == false)
        {
            getWorldPoint();
        }

        if(kitchenCoord == true && Input.GetMouseButtonDown(0))
        {
            getWorldPointKitchen();
        }
        if(dinningCoord == true && Input.GetMouseButtonDown(0))
        {
            getWorldPointDinning();
        }
        if(bedroom1Coord == true && Input.GetMouseButtonDown(0))
        {
            getWorldPointBedroom1();
        }
        if(bedroom2Coord ==  true && Input.GetMouseButtonDown(0))
        {
            getWorldPointBedroom2();
        }
        if(complete == true)
        {
            MatrixCreator matrixCreator = GetComponent<MatrixCreator>();
            if (matrixCreator != null)
            {
                matrixCreator.enabled = true;
            }
            
            enabled = false;
        }
    }
    // Update is called once per frame


    void Flodder(Vector3 point)
    {
        GameObject initialPoint = GameObject.Find("GridCell_" + point.x + "_" + point.z);
        if(initialPoint != null)
        {
            if(initialPoint.transform.GetChild(0).GetComponent<Renderer>().material.color == new Color(0x57 / 255f, 0xDA / 255f, 0x61 / 255f))
            {
                
                initialPoint.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0x53 / 255f, 0x4C / 255f, 0x87 / 255f);
                Flodder(new Vector3(point.x + 1, point.y, point.z));
                Flodder(new Vector3(point.x, point.y, point.z + 1));
                Flodder(new Vector3(point.x - 1, point.y, point.z));
                Flodder(new Vector3(point.x, point.y, point.z - 1));
            }
            
            
        }
    }

    void getWorldPoint()
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Pivot")
        {
            if (hit.collider.gameObject.GetComponent<Renderer>().material.color == new Color(0x53 / 255f, 0x4C / 255f, 0x87 / 255f))
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(0xD6 / 255f, 0xE4 / 255f, 0x7A / 255f);
            }
        }
    }

    void getWorldPointKitchen()
    {
        if(kitchenCord != Vector2.zero)
        {
            GameObject.Find("GridCell_" + kitchenCord.x + "_" + kitchenCord.y).transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0x53 / 255f, 0x4C / 255f, 0x87 / 255f);
        }
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Pivot")
        {
            int firstNumber, secondNumber;

            string[] parts = hit.collider.gameObject.transform.parent.name.Split('_');
            int.TryParse(parts[1], out firstNumber);
            int.TryParse(parts[2], out secondNumber);

            kitchenCord = new Vector2(firstNumber, secondNumber);
            if (hit.collider.gameObject.GetComponent<Renderer>().material.color == new Color(0x53 / 255f, 0x4C / 255f, 0x87 / 255f))
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(0xDC / 255f, 0x97 / 255f, 0x24 / 255f);
            }
            
        }

        kitchenCoord = false;
    }

    void getWorldPointDinning()
    {
        if (dinningCord != Vector2.zero)
        {
            GameObject.Find("GridCell_" + dinningCord.x + "_" + dinningCord.y).transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0x53 / 255f, 0x4C / 255f, 0x87 / 255f);
        }
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Pivot")
        {
            int firstNumber, secondNumber;

            string[] parts = hit.collider.gameObject.transform.parent.name.Split('_');
            int.TryParse(parts[1], out firstNumber);
            int.TryParse(parts[2], out secondNumber);

            dinningCord = new Vector2(firstNumber, secondNumber);
            if (hit.collider.gameObject.GetComponent<Renderer>().material.color == new Color(0x53 / 255f, 0x4C / 255f, 0x87 / 255f))
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(0xBF/255f, 0x23 / 255f, 0xDC / 255f);
            }
            
        }

        dinningCoord = false;
    }

    void getWorldPointBedroom1()
    {
        if (bedroom1Cord != Vector2.zero)
        {
            GameObject.Find("GridCell_" + bedroom1Cord.x + "_" + bedroom1Cord.y).transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0x53 / 255f, 0x4C / 255f, 0x87 / 255f);
        }
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Pivot")
        {
            int firstNumber, secondNumber;

            string[] parts = hit.collider.gameObject.transform.parent.name.Split('_');
            int.TryParse(parts[1], out firstNumber);
            int.TryParse(parts[2], out secondNumber);

            bedroom1Cord = new Vector2(firstNumber, secondNumber);
            if (hit.collider.gameObject.GetComponent<Renderer>().material.color == new Color(0x53 / 255f, 0x4C / 255f, 0x87 / 255f))
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(0x1F / 255f, 0xEF / 255f, 0xE5 / 255f);
            }
            
        }

        bedroom1Coord = false;
    }

    void getWorldPointBedroom2()
    {
        if (bedroom2Cord != Vector2.zero)
        {
            GameObject.Find("GridCell_" + bedroom2Cord.x + "_" + bedroom2Cord.y).transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0x53 / 255f, 0x4C / 255f, 0x87 / 255f);
        }
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Pivot")
        {
            int firstNumber, secondNumber;

            string[] parts = hit.collider.gameObject.transform.parent.name.Split('_');
            int.TryParse(parts[1], out firstNumber);
            int.TryParse(parts[2], out secondNumber);

            bedroom2Cord = new Vector2(firstNumber, secondNumber);
            if (hit.collider.gameObject.GetComponent<Renderer>().material.color == new Color(0x53 / 255f, 0x4C / 255f, 0x87 / 255f))
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(0xC5 / 255f, 0xFC / 255f, 0x50 / 255f);
            }
            
        }

        bedroom2Coord = false;
    }
}

