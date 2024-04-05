using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Tester : MonoBehaviour
{
    public GameObject gameBoard;

    // Start is called before the first frame update
    void Start()
    {
        // Test that the player can get positions from the board
        var script = gameBoard.GetComponent<Board_Script>();
        if (script != null)
        {
            Transform position_1 = script.GetPosition(3);

            print($"Player recieved position {position_1.position} from the board");

            Transform position2 = script.GetPosition(5);
            print($"Player recieved position { position2.position} from the board");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
