using System;
using System.Linq;
using UnityEngine;

public class Board_Script : MonoBehaviour
{
    public GameObject ownerMarkerPrefab;
    public GameObject playerTestPrefab;

    // Keep track of owned markers
    private GameObject[] ownedProperties = new GameObject[40];

    // Flag that can be enabled to run in Test mode
    public bool testBoard;

    // Test player object for Test mode
    private GameObject playerTestObject;

    // Timer attributes for testing the board
    private float timer = 0;

    void Start()
    {
        // If we are in Test mode, we spawn a test player object to place on the board
        if (testBoard)
        {
            playerTestObject = Instantiate(playerTestPrefab);
        }
    }

    void Update()
    {
        // If we are in Test mode, we move the player marker to a random tile and try to change ownership
        if (testBoard)
        {
            // Every 2 seconds we do this
            const float UPDATE_INTERVAL = 2.0f;

            timer += Time.deltaTime;
            if (timer >= UPDATE_INTERVAL)
            {
                timer = 0;

                // Pick a random Player and Tile on the board
                System.Random rnd = new System.Random();
                int tileIndex = rnd.Next(0, 40);
                int playerNumber = rnd.Next(0, 4);

                // Move the player test object to the tile
                var tileTransform = GetPosition(tileIndex);
                playerTestObject.transform.position = tileTransform.position;
                playerTestObject.transform.rotation = tileTransform.rotation;
                playerTestObject.GetComponentInChildren<Owner_Marker_Script>().SetPlayer(playerNumber);

                // Try to change ownership of the property if possible
                assignPropertyToPlayer(playerNumber, tileIndex);
            }
        }
    }

    // Get the position of specified Property/Tile
    public Transform GetPosition(int index)
    {
        string name = "Pos_" + index;
        var child = transform.Find(name);

        if (child != null)
        {
            return child;
        }
        return null;
    }

    // Get the position inside of the Jail cell
    public Transform GetJailPosition()
    {
        return GetPosition(99);
    }


    // Get the Free Parking tile position
    public Transform GetFreeParkingPosition()
    {
        return GetPosition(20);
    }

    public void assignPropertyToPlayer(int playerNumber, int tileIndex)
    {
        // Make sure property is ownable
        int[] restrictedList = { 0, 2, 4, 7, 10, 17, 20, 22, 30, 33, 36 };

        if (restrictedList.Contains(tileIndex))
        {
            Debug.Log("Property is now ownable!");
            return;
        }

        // Free the property if owned already
        freeProperty(tileIndex);


        var tileTransform = GetPosition(tileIndex);
        var marker = Instantiate(ownerMarkerPrefab);

        if (marker != null)
        {
            // Move the marker beside the property that was just owned
            marker.transform.position = tileTransform.position;
            marker.transform.rotation = tileTransform.rotation;
            marker.GetComponentInChildren<Owner_Marker_Script>().SetPlayer(playerNumber);

            // Store the marker in the property list so we know that it's owned
            ownedProperties[tileIndex] = marker;
        }
        else
        {
            Debug.Log("Owner Marker failed to spawn!");
        }
    }

    // Checks if property is owned
    public bool isPropertyOwned(int tileIndex)
    {
        return ownedProperties[tileIndex] != null;
    }

    // Free the property
    private void freeProperty(int tileIndex)
    {
        if (isPropertyOwned(tileIndex))
        {
            Destroy(ownedProperties[tileIndex].gameObject);
            ownedProperties[tileIndex] = null;
        }
    }
}
