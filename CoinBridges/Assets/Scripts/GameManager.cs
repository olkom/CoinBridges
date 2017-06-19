using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    
    public CoinManager coinManager;
    public BridgeManager bridgeManager;

    // ------   HAND     ------
    // number of coins in the hand of the player
    public Hand hand;
    public int NbrOfCoinsInHand = 3;

    // ------   BOARD     ------
    // number of rows and columns with coins. 
    
    //THESE VALUES ARE SET TWICE AT THE MOMENT, FIX!! (ALSO IN BOARD SCRIPT)
    public Board board;
    public int columns = 4;
    public int rows = 4;

    // Use this for initialization
    void Awake () {
        coinManager = GetComponent<CoinManager>();
        InitGame();
	}
	
    void InitGame()
    {
        LayoutCoinsOnBoard();
        LayoutCoinsInHand();
        AddBridgeToHand();
    }

	// Update is called once per frame
	void Update () {
		
	}

    void LayoutCoinsOnBoard()
    {
        //looping through the x and z of our board
        for (float x = 0; x < columns; x++)
        {
            for (float z = 0; z < rows; z++)
            {
                board.AddTopCoin(new Vector3(x, 0, z), coinManager.AddCoin(new Vector3(x, 0.2f, z), false));
            }
        }
        board.AddWinningPositions(new Vector3(0, 0, 0), new Vector3(1, 0, 1));
    }
    void LayoutCoinsInHand()
    {
        for (float x = 0; x < NbrOfCoinsInHand; x++)
        {
            float z = -2;
            hand.addCoinToHandArray(x, coinManager.AddCoin(new Vector3(x+2, hand.yoffsetHand, z), true));
        }
    }

    void AddBridgeToHand()
    {
        bridgeManager.addBridge(new Vector3(0.5f, hand.yoffsetHand, -1.5f), true, true);
    }
}
