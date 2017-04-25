using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

    // -------  Coin info   ---------------


    //The gameobject Coin, in the prefab folder that we use to instantiate
    public Coin CoinPrefab;
    // the different colors that coins can have
    public Color[] coinColors = { Color.green, Color.red, Color.blue, Color.yellow };
    // a holder gameObject that we use as a parent to the instantiated coins (keeps hierarchy clean)
    private Transform coinHolder;


    // ------   Hand      ------
    public int CoinsInHand = 3;
    public float yoffsetHand = 0.4f;


    // ------   BOARD     ------
    // number of rows and columns with coins.
    public int columns = 4;
    public int rows = 4;
    // height offset so that the coins lay flat on the ground.
    public float yoffsetBoard = 0.2f;
    


    void LayoutCoinsOnBoard()
    {
        //setting up a holder for the objects to be instantiated.
        coinHolder = new GameObject("BoardCoins").transform;
        
        //looping through the x and z of our board
        for (float x = 0; x < columns; x++)
        {
            for (float z = 0; z < rows; z++)
            {
                
                //randomizing color for the coin about to be placed.
                Color botColor = RandomColor();
                Color topColor = RandomColor();
                
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
    }

    void LayoutCoinsInHand()
    {
        for (float x = 0; x < CoinsInHand; x++)
        {
            //randomizing color for the coin about to be placed.
            Color botColor = RandomColor();
            Color topColor = RandomColor();
            float z = -2;

            //instantiate the coin prefab at the x, z coordinates that we loop through
            Coin coin = Instantiate(CoinPrefab, new Vector3(x, yoffsetHand, z), Quaternion.identity) as Coin;
            //setting top and bottom color
            coin.TopColor = topColor;
            coin.BotColor = botColor;
           
            // naming the coin
            coin.name = "Handcoin " + x;
            // setting the coin to be dragable, since it is in the hand.
            coin.isDragable = true;
        }
    }

    //Funciton for returning a random color out of the colors in the coinColors array.
    Color RandomColor()
    {
        Color randomColor = coinColors[Random.Range(0, coinColors.Length)];
        return randomColor;
    }

    /*public Coin GetTopCoin(Vector3 pos)
    {
        
    }*/

    // the call function from the game manager
    public void LayStartingCoins()
    {
        LayoutCoinsOnBoard();
        LayoutCoinsInHand();
    }
}
