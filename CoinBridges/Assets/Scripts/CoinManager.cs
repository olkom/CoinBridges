using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

    public int columns = 4;
    public int rows = 4;
    public int startingX = 0;
    public int startingZ = 0;
    public int endingX = 3;
    public int endingZ = 3;

    //private Vector3 placementPosition;
    private Transform coinHolder;
    public Color[] coinColors = { Color.green, Color.red, Color.blue, Color.yellow };
    public Transform CoinPrefab;
    //public Coin coin;

    Color RandomColor()
    {
        Color randomColor = coinColors[Random.Range(0, coinColors.Length)];
        return randomColor;
    }

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
                Vector3 placementPosition = new Vector3(x,0.3f,z);

                //instantiate the coin prefab at the x , z coordinates that we loop through

                //Coin coin = new Coin(CoinPrefab, Color.green, Color.red, false, true, placementPosition);
                Transform coin = Instantiate(CoinPrefab, new Vector3(x, 0.3f, z), Quaternion.identity);

                //accessing the renderers of the child and parent (bottom and top)
                Renderer[] renderers = coin.GetComponentsInChildren<Renderer>();

                //accessing the bottom renderer by looking at both renderers and 
                foreach (Renderer renderer in renderers)
                {
                    if (renderer.gameObject.transform.parent != null)
                    { //child renderer -> bottomcolor

                        //this sets the starting and ending coins bottom color to black.
                        if ((x == startingX && z == startingZ) || (x == endingX && z == endingZ))
                        {
                            renderer.material.color = Color.black;
                        }
                        else
                        {
                            renderer.material.color = botColor;
                        }
                    }
                    else //parent renderer -> topColor
                    {
                        renderer.material.color = topColor;
                    }
                }
                //putting the new spawned coin under the boardholder parent.
                coin.transform.SetParent(coinHolder);
                coin.name = "coin " + x + "." + z;
                coin.GetComponent<DragDropCoin>().enabled = false;

            }
        }
    }

    public void LayStartingCoins()
    {
        LayoutCoinsOnBoard();
        
    }
}
