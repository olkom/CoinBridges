using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour {

    //THESE VALUES ARE SET TWICE AT THE MOMENT, FIX!! (ALSO IN GAMEMANAGER SCRIPT)


    //----------COINS-------------
    public int columns = 4;
    public int rows = 4;
    // height offset so that the coins lay flat on the ground.
    public float yoffsetBoard = 0.2f;
    public Coin[,] BoardTopCoins = new Coin[4,4];

    //function to add a Coin to the topcoin array 
    public void AddTopCoin(Vector3 position, Coin topCoin)
    {
        BoardTopCoins[(int)position.x, (int)position.z] = topCoin;
    }
    //function to get the TopColor of the topcoin.
    public Color GetTopCoinTopColor(float x, float z)
    {
        if (((int)x > rows - 1 || (int)z > columns - 1) || ((int)x < 0 || (int)z < 0)) // this statement make sure to return black when asking for a position outside the coins on the board.
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

    // ----------- BRIDGES ---------------
    //this matrix keeps track of the bridges that are placed, this matrix is bigger than topCoins, to get space for bridges. 
    //0,0 -> 1,1 
    //1,1 -> 3,3
    //2,2 -> 5,5

    public bool[,] BridgePositions = new bool[9, 9];
    private int bridgePositionX;
    private int bridgePositionZ;
    private int coin1positionX;
    private int coin1positionZ;
    private int coin2positionX;
    private int coin2positionZ;
    public Vector2 winPosition1;
    public Vector2 winPosition2;
    public bool foundWin1;
    public bool foundWin2;
    

    public void AddWinningPositions(Vector3 position1, Vector3 position2)
    {
        for (int x=0;x<9;x++)
        {
            for (int z = 0; z < 9; z++)
            {
                BridgePositions[x, z] = false;
            }
        }
        BridgePositions[((int)position1.x * 2) + 1, ((int)position1.z * 2) + 1] = true;
        winPosition1.x = position1.x;
        winPosition1.y = position1.z;
        BridgePositions[((int)position2.x * 2) + 1, ((int)position2.z * 2) + 1] = true;
        winPosition2.x = position2.x;
        winPosition2.y = position2.z;
    }
    
    
    //function to as the board if there is a bridge in x,z position.
    public bool isBridged(int x, int z)
    {
        return BridgePositions[x, z];
    }
    private void FindBridges(int x, int z)
    {
        //top
        if (isBridged(x, z + 1))
        {
            nextCheck(x, z + 1, x, z + 2, x, z);
        }
        //right
        if (isBridged(x + 1, z))
        {
            nextCheck(x + 1, z, x + 2, z, x, z);
        }
        //bot
        if (isBridged(x, z - 1))
        {
            nextCheck(x, z - 1, x, z - 2, x, z);
        }
        //left
        if (isBridged(x - 1, z))
        {
            nextCheck(x - 1, z, x - 2, z, x, z);
        }
    }
    private bool isCoinWinCoin(int x, int z)
    {
        if (BridgePositions[x, z])
        {
            if (x == winPosition1.x && z == winPosition1.y)
            {
                foundWin1 = true;
                return true;
            }
            else if (x == winPosition2.x && z == winPosition2.y)
            {
                foundWin2 = true;
                return true;
            }
        } 
            return false;
    }
    private void GameWon()
    {
        SceneManager.LoadScene("Main Menu");
    }
    private void nextCheck(int bridgeX, int bridgeZ, int coin1X, int coin1Z, int coin2X, int coin2Z)
    {
        if (!isCoinWinCoin(coin1X, coin1Z))
        {
            FindBridges(coin1X, coin1Z);
        }
        if (!isCoinWinCoin(coin2X, coin2Z))
        {
            FindBridges(coin2X, coin2Z);
        }

        AddBridge(bridgeX, bridgeZ);
        if (foundWin1 && foundWin2)
        {
            GameWon();
        }
    }
    //following functions are called when bridge is placed to check winning condition!
    public void CheckWinConditions(Bridge bridge)
    {
        //transfering the values
        bridgePositionX = ((int)(bridge.transform.position.x * 2) + 1);
        bridgePositionZ = ((int)(bridge.transform.position.z * 2) + 1);
        coin1positionX = ((int)(bridge.bridgeCoins[0].transform.position.x * 2) + 1);
        coin1positionZ = ((int)(bridge.bridgeCoins[0].transform.position.z * 2) + 1);
        coin2positionX = ((int)(bridge.bridgeCoins[1].transform.position.x * 2) + 1);
        coin2positionZ = ((int)(bridge.bridgeCoins[1].transform.position.z * 2) + 1);
        Debug.Log(bridgePositionX);
        Debug.Log(bridgePositionZ);
        Debug.Log(coin1positionX);
        Debug.Log(coin1positionZ);
        Debug.Log(coin2positionX);
        Debug.Log(coin2positionZ);

        nextCheck(bridgePositionX, bridgePositionZ, coin1positionX, coin1positionZ, coin2positionX, coin2positionZ);

        


        
    }
    // add true to the position in the bridge-grid
    public void AddBridge(int x, int z)
    {
        BridgePositions[x, z] = true;
    }
    void Update()
    {
        
    }
}
