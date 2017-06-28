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
    public Vector2 winPosition1;
    public Vector2 winPosition2;
    private Vector2 bridgePosition;
    //private bool foundPath = false;
    private List<Vector2> coinPositionsToCheck = new List<Vector2>();

    /*private int bridgePositionX;
    private int bridgePositionZ;
    private int coin1positionX;
    private int coin1positionZ;
    private int coin2positionX;
    private int coin2positionZ;*/

    //public bool foundWin1;
    //public bool foundWin2;

    public void AddBridge(Vector2 position)
    {
        BridgePositions[(int)position.x, (int)position.y] = true;
    }
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
        winPosition1.x = ((int)position1.x*2)+1;
        winPosition1.y = ((int)position1.z*2)+1;
        BridgePositions[((int)position2.x * 2) + 1, ((int)position2.z * 2) + 1] = true;
        winPosition2.x = ((int)position2.x*2)+1;
        winPosition2.y = ((int)position2.z*2)+1;
        //Debug.Log(winPosition1.x);
        //Debug.Log(winPosition1.y);
        //Debug.Log(winPosition2.x);
        //Debug.Log(winPosition2.y);

    }
    public void AddBridgeCheckWinConditions(Bridge bridge)
    {
        /*//transfering the values
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
        */
        bridgePosition.x = (bridge.transform.position.x * 2) + 1;
        bridgePosition.y = (bridge.transform.position.z * 2) + 1;
        AddBridge(bridgePosition);
        
        CheckWinConditions();

    }
    private void CheckWinConditions()
    {
        //Debug.Log(winPosition1.x);
        //Debug.Log(winPosition1.y);
        Debug.Log(coinPositionsToCheck.Count);
        coinPositionsToCheck.Add(winPosition1);
        Debug.Log(coinPositionsToCheck.Count);
        //Debug.Log(coinPositionsToCheck);
        /*for (int x = 1; x < 8; x++)
        {
            for (int z = 1; z < 8; z++)
            {
                Debug.Log(BridgePositions[x, z]);
                 
            }
        }*/


        //while (coinPositionsToCheck.Count > 0)
        //{
        //Debug.Log(coinPositionsToCheck[0].x);
        //Debug.Log(coinPositionsToCheck[0].y);
        CheckCoinPosition(winPosition1);
        //}
        //Debug.Log(coinPositionsToCheck.Count);
        //Debug.Log(coinPositionsToCheck);
        coinPositionsToCheck.Clear();
        //Debug.Log(coinPositionsToCheck.Count);
    }
    private void CheckCoinPosition(Vector2 position)
    {
        FindBridges(position);
    }

    //function to as the board if there is a bridge in x,z position.
    public bool IsBridged(Vector2 bridgePosition)
    {
        //Debug.Log(bridgePosition.x);
        //Debug.Log(bridgePosition.y);
        return BridgePositions[(int)bridgePosition.x, (int)bridgePosition.y];
    }
    private void FindBridges(Vector2 coinPosition)
    {
        //top
        if (IsBridged(coinPosition + new Vector2(0,1)))
        {
            if ((coinPosition + new Vector2(0, 2)) == winPosition2)
            {
                GameWon();
            } else
            {
                coinPositionsToCheck.Add(coinPosition + new Vector2(0, 2));
            }
        }
        //right
        if (IsBridged(coinPosition + new Vector2(1, 0)))
        {
            if ((coinPosition + new Vector2(2, 0)) == winPosition2)
            {
                GameWon();
            }
            else
            {
                coinPositionsToCheck.Add(coinPosition + new Vector2(2,0));
            }
        }
        //bot
        if (IsBridged(coinPosition + new Vector2(0, -1)))
        {
            if ((coinPosition + new Vector2(0, -2)) == winPosition2)
            {
                GameWon();
            }
            else
            {
                coinPositionsToCheck.Add(coinPosition + new Vector2(0, -2));
            }
        }
        //left
        if (IsBridged(coinPosition + new Vector2(-1, 0)))
        {
            if ((coinPosition + new Vector2(-2, 0)) == winPosition2)
            {
                GameWon();
            }
            else
            {
                coinPositionsToCheck.Add(coinPosition + new Vector2(-2, 0));
            }
        }
        coinPositionsToCheck.Remove(coinPosition);
    }

   

    //following functions are called when bridge is placed to check winning condition!

    
    // add true to the position in the bridge-grid

    private void GameWon()
    {
        SceneManager.LoadScene("Main Menu");
    }
    void Update()
    {
        
    }
}
