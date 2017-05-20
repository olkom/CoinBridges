using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public int columns = 4;
    public int rows = 4;
    // height offset so that the coins lay flat on the ground.
    public float yoffsetBoard = 0.2f;
    public int[,] TopCoins = new int[4,4];

    //private Transform coinHolder;
    public void SetTopCoin(Vector3 position)
    {
        TopCoins[(int)position.x,(int)position.z] = (int)position.y;
    }
    public int GetTopCoin(int x, int z)
    {
        return TopCoins[x,z];
    }
        // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*void SetupCoinsOnBoard()
    {
        //setting up a holder for the objects to be instantiated.
        coinHolder = new GameObject("BoardCoins").transform;

        //looping through the x and z of our board
        for (float x = 0; x < columns; x++)
        {
            for (float z = 0; z < rows; z++)
            {
                
                
                //TopCoinHeight[(int)x][(int)z] = 0; 
                //randomizing color for the coin about to be placed.
                //Color botColor = RandomColor();
                //Color topColor = RandomColor();

                //instantiate the coin prefab at the x, z coordinates that we loop through
                Coin coin = Instantiate(CoinPrefab, new Vector3(x, yoffsetBoard, z), Quaternion.identity) as Coin;
                //setting top and bottom color
                coin.TopColor = topColor;
                coin.BotColor = botColor;

                //putting the new spawned coin under the boardholder parent.
                coin.transform.SetParent(coinHolder);
                // naming the coin x.z
                coin.name = "coin " + x + "." + z;
                // setting the coin to be not dragable, since it is on the board already.
                coin.isDragable = false;

            }
        }
    }*/
}
