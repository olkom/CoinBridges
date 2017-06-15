using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    //THESE VALUES ARE SET TWICE AT THE MOMENT, FIX!! (ALSO IN GAMEMANAGER SCRIPT)

    public int columns = 4;
    public int rows = 4;
    // height offset so that the coins lay flat on the ground.
    public float yoffsetBoard = 0.2f;
    public Coin[,] BoardTopCoins = new Coin[4,4];

    //this matrix keeps track of the bridges that are placed
    public bool[,] BridgedPositions = new bool[9, 9]; 



    //function to add a Coin to the topcoin array 
    public void AddTopCoin(Vector3 position, Coin topCoin)
    {
        BoardTopCoins[(int)position.x, (int)position.z] = topCoin;
    }
    //function to get the TopColor of the topcoin.
    public Color GetTopCoinTopColor(float x, float z)
    {
        if (((int)x > rows-1 || (int)z > columns-1) || ((int)x < 0 || (int)z < 0)) // this statement make sure to return black when asking for a position outside the coins on the board.
        {
            return Color.black;
        }
        else
        {
            return BoardTopCoins[(int)x, (int)z].TopColor;
        }
    }
    //function to get the height of the top coin.
    public float GetTopCoinHeight(float x, float z)
    {
        if (((int)x > rows - 1 || (int)z > columns - 1) || ((int)x < 0 || (int)z < 0)) // this statement make sure to return 0 when asking for a position outside the coins on the board.
        {
            return 0;
        }
        else
        {
            return BoardTopCoins[(int)x, (int)z].transform.position.y;
        }
    }
    //function to add bridge to bridge-array
    public void AddBridge(float x, float z)
    {
        BridgedPositions[(int)x, (int)z] = true;
    }
    //function to as the board if there is a bridge in x,z position.
    public bool isBridged(float x, float z)
    {
        return BridgedPositions[(int)x, (int)z];
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
