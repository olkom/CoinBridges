using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

    // -------  Coin info   ---------------
    //The gameobject Coin, in the prefab folder that we use to instantiate
    public Coin CoinPrefab;
    // the different colors that coins can have
    public Color[] coinColors = { Color.green, Color.red , Color.blue, Color.yellow };
    // a holder gameObject that we use as a parent to the instantiated coins (keeps hierarchy clean) NOT WORKING?!
    //private Transform coinHolder;


    public Coin AddCoin(Vector3 position, bool isDragable)
    {

        //randomizing color for the coin about to be placed.
        Color botColor = RandomColor();
        Color topColor = RandomColor();

        //instantiate the coin prefab at the x, z coordinates that we loop through
        Coin coin = Instantiate(CoinPrefab, position, Quaternion.identity) as Coin;
        //setting top and bottom color
        coin.TopColor = topColor;
        coin.BotColor = botColor;

        //putting the new spawned coin under the boardholder parent. NOT WORKING...
        //coin.transform.SetParent(coinHolder);

        //Setting the coin to be dragable or not
        if (isDragable)
        {
            coin.isDragable = true;
        } else
        {
            coin.isDragable = false;
        }
        return coin;
    }

    //Funciton for returning a random color out of the colors in the coinColors array.
    Color RandomColor()
    {
        Color randomColor = coinColors[Random.Range(0, coinColors.Length)];
        return randomColor;
    }
}
