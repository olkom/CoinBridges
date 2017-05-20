using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //public BoardManager boardScript;
    public CoinManager coinManager;

    //public Hand playerHand;
    public Hand handPrefab;
    public int NbrOfCoinsInHand = 3;

    // ------   BOARD     ------
    // number of rows and columns with coins.
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
                coinManager.AddCoin(new Vector3(x, 0.2f, z), false);
                board.SetTopCoin(new Vector3(x, 0, z));
                //board.board[(int)x][(int)z] = 1;
                //TopCoinHeight[(int)x][(int)z] = 0; 

            }
            
        }
    }
    void LayoutCoinsInHand()
    {
        Hand hand1 = Instantiate(handPrefab, new Vector3(2, -0.25f, -2), Quaternion.identity) as Hand;
        hand1.NbrOfCoins = NbrOfCoinsInHand;
        //coinHolder = new GameObject("BoardCoins").transform;

        for (float x = 0; x < hand1.NbrOfCoins; x++)
        {
            float z = -2;
            coinManager.AddCoin(new Vector3(x, hand1.yoffsetHand, z), true);
        }
    }
}
