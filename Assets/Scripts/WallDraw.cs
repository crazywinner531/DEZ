using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallDraw : MonoBehaviour
{

    public GameObject startPole;
    public GameObject endPole;
    public GameObject wallPole;

    GameObject Wall;

    bool ActiveFlag;
    bool flag;

    public Vector3 approx; 
    Vector3 StartPoint;

    List<Vector3> CornorPoints = new List<Vector3>(); 

    // Start is called before the first frame update
    private void Awake()
    {
        enabled = false;
    }

    void Start()
    {
        ActiveFlag = false;
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SetStart();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetEnd();
        }
        else
        {
            if (ActiveFlag)
            {
                Adjust();
            }
        }
    }

    void SetStart()
    {
        ActiveFlag = true;
        if (flag)
        {
            startPole.transform.position = endPole.transform.position;
        }
        else
        {
            startPole.transform.position = getWorldPoint();
            StartPoint = startPole.transform.position;
        }

        Wall = (GameObject)Instantiate(wallPole, startPole.transform.position, Quaternion.identity);
        Wall.name = "waall";
        BoxCollider boxCollider = Wall.GetComponent<BoxCollider>();
        Vector3 newSize = new Vector3(8, boxCollider.size.y, boxCollider.size.z);

        // Assign the new size to the BoxCollider
        boxCollider.size = newSize;
        CornorPoints.Add(startPole.transform.position);
        flag = true;
    }

    void SetEnd()
    {
        ActiveFlag = false;
        endPole.transform.position = getWorldPoint();

        if (Vector3.Distance(endPole.transform.position, StartPoint) < 1)
        {
            endPole.transform.position = StartPoint;
            AdjustWall();

            for (int i = 0; i < CornorPoints.Count; i++)
            {

            }

            Vector3 centroid = CalculateCentroid();


            approx = new Vector3(Mathf.RoundToInt(centroid.x), centroid.y, Mathf.RoundToInt(centroid.z));
            Debug.Log("centroid" + approx);

            FloodFillingFeasableArea floodFillingFeasableArea = GetComponent<FloodFillingFeasableArea>();
            if(floodFillingFeasableArea != null)
            {
                floodFillingFeasableArea.enabled = true;
            }

            enabled = false;
        }
    }

    void Adjust()
    {
        endPole.transform.position = getWorldPoint();
        AdjustWall();
    }

    Vector3 CalculateCentroid()
    {
        if (CornorPoints.Count == 0)
        {
            Debug.LogWarning("Cannot calculate centroid with an empty list.");
            return Vector3.zero;
        }

        Vector3 centroid = Vector3.zero;

        foreach (Vector3 point in CornorPoints)
        {
            centroid += point;
        }

        centroid /= CornorPoints.Count;

        centroid = new Vector3(Mathf.RoundToInt(centroid.x), Mathf.RoundToInt(centroid.y), Mathf.RoundToInt(centroid.z));

        return centroid;
    }

    void AdjustWall()
    {
        startPole.transform.LookAt(endPole.transform.position);
        startPole.transform.LookAt(endPole.transform.position);

        float distance = Vector3.Distance(startPole.transform.position, endPole.transform.position);
        Wall.transform.position = startPole.transform.position + ((distance / 2) * startPole.transform.forward);
        Wall.transform.rotation = startPole.transform.rotation;
        Wall.transform.localScale = new Vector3(Wall.transform.localScale.x, 1, distance);
    }

    Vector3 getWorldPoint()
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Pivot")
        {
            return new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
        else
        {
            return endPole.transform.position;
        }
    }

}
