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
    public bool[,] CheckedCoins = new bool[9, 9];
    public Vector2 winPosition1;
    public Vector2 winPosition2;
    private Vector2 bridgePosition;
    private List<Vector2> coinPositionsToCheck = new List<Vector2>();
    public GameObject winPositionPrefab;

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
        GameObject win1 = Instantiate(winPositionPrefab, position1, Quaternion.identity);
        GameObject win2 = Instantiate(winPositionPrefab, position2, Quaternion.identity);
    }
    public void AddBridgeCheckWinConditions(Bridge bridge)
    {
        bridgePosition.x = (bridge.transform.position.x * 2) + 1;
        bridgePosition.y = (bridge.transform.position.z * 2) + 1;
        AddBridge(bridgePosition);
        CheckWinConditions();
    }
    private void CheckWinConditions()
    {
        //refresh the coins that have been searched!
        for (int x = 0; x < 9; x++)
        {
            for (int z = 0; z < 9; z++)
            {
                CheckedCoins[x, z] = false;
            }
        }
        //adds the win1 position to start looking for bridges.
        coinPositionsToCheck.Add(winPosition1);

        //loops through coinpositions
        while (coinPositionsToCheck.Count > 0)
        {
            CheckCoinPosition(coinPositionsToCheck[0]);
        }
        
        coinPositionsToCheck.Clear();
        
    }
    private void CheckCoinPosition(Vector2 position)
    {
        FindBridges(position);
    }
    public bool IsBridged(Vector2 bridgePosition)
    {
        //returns if there is a bridge.
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
            } else if (!CheckedCoins[(int)coinPosition.x,(int)coinPosition.y+2])
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
            else if (!CheckedCoins[(int)coinPosition.x +2 , (int)coinPosition.y])
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
            else if (!CheckedCoins[(int)coinPosition.x, (int)coinPosition.y -2])
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
            else if (!CheckedCoins[(int)coinPosition.x - 2, (int)coinPosition.y])
            {
                coinPositionsToCheck.Add(coinPosition + new Vector2(-2, 0));
            }
        }
        CheckedCoins[(int)coinPosition.x, (int)coinPosition.y] = true;
        coinPositionsToCheck.Remove(coinPosition);
    }
    private void GameWon()
    {
        SceneManager.LoadScene("Main Menu");
    }
 }
