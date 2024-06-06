using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTouch : MonoBehaviour
{
    private void Awake()
    {
        enabled = false;
    }

    public GameObject floor;

    // Start is called before the first frame update
    void Start()
    {
        
        if (floor != null)
        {
            floor.SetActive(true);
        }
        else
        {
            Debug.LogError("Ground GameObject not found in the scene.");
        }

        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        foreach (GameObject wall in walls)
        {
            Destroy(wall);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
