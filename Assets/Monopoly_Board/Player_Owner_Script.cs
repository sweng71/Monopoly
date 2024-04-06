using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_Marker_Script : MonoBehaviour
{
    // Materials with different colors for each Player number
    public Material materialP0;
    public Material materialP1;
    public Material materialP2;
    public Material materialP3;


    // Change the color of the child object to match the player color
    public void SetPlayer(int player)
    {
        switch(player)
        {
            case 0:
                transform.Find("Mesh").GetComponent<MeshRenderer>().material = materialP0;
                break;

            case 1:
                transform.Find("Mesh").GetComponent<MeshRenderer>().material = materialP1;
                break;

            case 2:
                transform.Find("Mesh").GetComponent<MeshRenderer>().material = materialP2;
                break;

            case 3:
                transform.Find("Mesh").GetComponent<MeshRenderer>().material = materialP3;
                break;


        }
    }
}
