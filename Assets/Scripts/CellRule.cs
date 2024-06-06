using UnityEngine;

public class CellRule : MonoBehaviour
{
    public Color onEnterColor = new Color(0xBA / 255f, 0x2C / 255f, 0x23 / 255f); // Enter color
    public Color onExitColor = new Color(0x57 / 255f, 0xDA / 255f, 0x61 / 255f);  // Exit color

    void Start()
    {
       
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        // Assign the enter material
        if (other.gameObject.name == "waall")
        {
            gameObject.GetComponent<MeshRenderer>().material.color = onEnterColor;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Assign the exit material
        if (other.gameObject.name == "waall")
        {
            gameObject.GetComponent<MeshRenderer>().material.color = onExitColor;
        }
    }

   
}
